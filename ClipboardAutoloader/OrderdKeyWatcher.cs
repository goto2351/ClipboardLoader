using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ClipboardAutoloader
{
    //http://sheepdogjam.cocolog-nifty.com/blog/2013/12/4-cbb2.html より引用

    public class OrderdKeyWatcher
    {
        /// <summary>
        /// キーの状態を保持するprivate class
        /// </summary>
        private class KeyStateValue
        {
            public DateTime Time { get; private set; }
            public IntPtr KeyCode { get; private set; }
            public bool IsTerminal { get; private set; }
            public KeyStateValue Next { get; set; }

            // 継続用
            public KeyStateValue(IntPtr pointer)
            {
                KeyCode = pointer;
                Time = DateTime.MinValue;
                IsTerminal = false;
            }

            // 終端用
            public KeyStateValue()
            {
                IsTerminal = true;
            }

            // 時刻の記録
            public void Record()
            {
                Time = DateTime.Now;
            }
        }

        /// <summary>
        /// KeyStateValueのリスト 
        /// </summary>
        private class KeyStateValueList : List<KeyStateValue>
        {
            public KeyStateValue First { get; private set; }
            public KeyStateValue Last { get; private set; }

            public KeyStateValueList(params int[] keyCode)
            {
                var temp = new KeyStateValue(); // ダミー

                keyCode.ToList<int>().ForEach(x =>
                {
                    var value = new KeyStateValue(new IntPtr(x));
                    temp.Next = value;
                    Add(value);
                    temp = value;
                });

                // 先頭の設定
                First = this[0];
                Last = temp;

                // 終端の追加
                temp.Next = new KeyStateValue();
            }
        }

        // Lockオブジェクト
        private object _lock = new object();

        // 確認するキーのリスト
        private int[] _keyCodeArray;
        private KeyStateValueList _keyStateValueList;

        // 確認間隔
        private int _interval;

        // 閾値
        private int _threshold;
        private TimeSpan _thresholdTimeSpan;

        // 別スレッド管理用のTask
        private Task _task;

        // 動作フラグ
        private bool stopFlag = false;

        // GetAsyncKeyStateの押下確認用ビットマスク
        private const Int64 mask64 = (Int64)0x0001;

        // イベント
        public event EventHandler<KeyWatcherEventArgs> KeyPushed;

        // アンマネージド キーが押下されたかどうか確認する
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetAsyncKeyState(IntPtr nVirtKey);

        /// <summary>
        /// 指定されたキーが押下されるまでバックグラウンドで待ち、押下された場合にイベントを発生させる
        /// </summary>
        /// <param name="interval">確認間隔 ミリ秒</param>
        /// <param name="threshold">全体での入力時の閾値 ミリ秒</param>
        /// <param name="callBackMethod">イベント</param>
        /// <param name="keyCode">確認するキーコード</param>
        public OrderdKeyWatcher(int interval, int threshold, params int[] keyCode)
        {
            _keyCodeArray = keyCode;
            _keyStateValueList = new KeyStateValueList(_keyCodeArray);

            _interval = interval;
            _threshold = threshold;
            _thresholdTimeSpan = new TimeSpan(0, 0, 0, 0, _threshold);
            //_callBackMethod = callBackMethod;
        }

        /// <summary>
        /// 指定されたキー状態を観察します
        /// </summary>
        public void Watch()
        {
            lock (_lock)
            {
                // 多重起動防止
                if (_task != null)
                    if (_task.Status == TaskStatus.Running)
                        return;

                // 停止フラグ
                stopFlag = true;
            }

            // 監視開始
            _task = new Task(WatchKeys);
            _task.Start();
        }

        /// <summary>
        /// 観察を中断します
        /// </summary>
        public void Abort()
        {
            lock (_lock)
            {
                stopFlag = false;
            }
        }

        /// <summary>
        /// 指定されたキーの観察
        /// </summary>
        private void WatchKeys()
        {
            int intervalSum = 0;
            KeyStateValue target = _keyStateValueList.First;

            while (stopFlag)
            {
                if (WatchKey(target.KeyCode))
                {
                    target.Record();
                    target = target.Next;

                    // 押下判定
                    if (target.IsTerminal) // 最後のキーが押下されたか?
                    {
                        // 全入力の時間が閾値以下である事
                        if ((_keyStateValueList.Last.Time - _keyStateValueList.First.Time) < _thresholdTimeSpan)
                        {
                            // 見つかったらコールバック
                            KeyPushed(this, new KeyWatcherEventArgs(_keyCodeArray));
                        }
                        intervalSum = 0;
                        target = _keyStateValueList.First;
                    }
                }
                else
                {
                    // 見つからない場合はウェイトする
                    Thread.Sleep(_interval);
                    intervalSum += _interval;
                }

                // 一定期間内に見つからない場合は最初に戻る
                if (intervalSum >= _threshold)
                {
                    intervalSum = 0;
                    target = _keyStateValueList.First;
                }
            }
        }

        /// <summary>
        /// 指定されたキーが押下されたか確認
        /// </summary>
        /// <param name="code">キーコード</param>
        /// <returns>キーの状態、押下された場合true</returns>
        private bool WatchKey(IntPtr code)
        {
            return ((GetAsyncKeyState(code).ToInt64() & mask64) != 0);
        }
    }

}
