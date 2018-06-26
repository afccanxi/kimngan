using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuaHangXayDung
{
    class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
