using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTan
{
    public interface IBlock
    {
        int Hp { get; set; }
        int PositionX { get; set; }
        int PositionY { get; set; }
        void Show(Graphics e);
    }
}
