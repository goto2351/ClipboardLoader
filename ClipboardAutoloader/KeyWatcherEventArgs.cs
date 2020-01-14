using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardAutoloader
{
    public class KeyWatcherEventArgs
    {
        private int[] _pressedKeyCode;

        public int[] PressedKeyCode
        {
            get
            {
                return _pressedKeyCode;
            }
        }

        public KeyWatcherEventArgs(params int[] pressedKeyCode)
        {
            _pressedKeyCode = pressedKeyCode;
        }
    }
}
