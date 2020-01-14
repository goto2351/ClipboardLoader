using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ClipboardAutoloader
{
    public partial class Form1 : Form
    {
        int LineNumber; //現在の行番号
        List<string> text = new List<string>();//テキストファイルの内容を格納
        Boolean isEOF; //現在の行がファイルの終わりか?
        int LastLine = 0;//最後の行番号
        string NextLine;
        //キー入力監視
        private OrderdKeyWatcher watcher;

        public Form1()
        {
            InitializeComponent();
            StatusLabel.Text = "テキストファイルが読み込まれていません";

        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilepathBox.Text = openFileDialog.FileName;
                
            }
        }


        //ファイルを読みこんでリストに格納するメソッド
        private void ReadText(string FileName)
        {
            StreamReader reader = new StreamReader(FileName, Encoding.GetEncoding("Shift_JIS"));

            //ファイルを最後まで読み込んで1行ずつ格納する
            string line;
            while((line = reader.ReadLine()) != null)
            {
                //空白の行は飛ばす
                if(line != "")
                {
                    text.Add(line);
                    LastLine += 1;
                }
            }
        }

        //次の行をクリップボードにセットするメソッド
        private void SetNextLine()
        {

            //次の行をクリップボードに書き込む
            Clipboard.SetText(NextLine);

            LineNumber += 1;
            //EOF判定
            if (LineNumber == LastLine)
            {
                isEOF = true;
            }

            //EOFならステータス変更、途中なら次の行を読み込み
            if (isEOF == true)
            {
                NextButton.Enabled = false;
                StatusLabel.Text = "ファイルの最後です";
            }
            else
            {
                //次の行を変数に格納
                NextLine = text[LineNumber];
                StatusLabel.Text = "次の行: " + NextLine;
            }

        }

        //前の行をクリップボードにセットするメソッド
        private void SetPrevLine()
        {
            //1行目にいるときは何もしない
            if(LineNumber > 1)
            {
                //1行前に戻す
                LineNumber -= 1;

                //行を読み込んでクリップボードに書き込み
                string PrevLine = text[LineNumber - 1];
                Clipboard.SetText(PrevLine);

                //ステータスバーを更新
                NextLine = text[LineNumber];
                StatusLabel.Text = "次の行: " + NextLine;
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            ReadText(FilepathBox.Text);
            LineNumber = 0;
            NextLine = text[LineNumber];
            StartButton.Enabled = true;
            StatusLabel.Text = "テキストファイルの読み込みが完了しました";
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //最初の行のセット
            SetNextLine();

            //ボタンを有効化
            BackButton.Enabled = true;
            NextButton.Enabled = true;

            //キー入力監視クラス
            watcher = new OrderdKeyWatcher(50, 10, (int)Keys.Right);
            watcher.KeyPushed += new EventHandler<KeyWatcherEventArgs>(this.watcher_KeyPushed);
            watcher.Watch();

            
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            SetNextLine();
        }

        //→キーが押されたときに次の行を読み込む
        delegate void SetFocusDelegate();
        private void watcher_KeyPushed(object sender,KeyWatcherEventArgs e)
        {
            //今が最後の行でないか?
            if(isEOF != true)
            {
                Invoke(new SetFocusDelegate(SetNextLine));
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            SetPrevLine();
        }
    }
}