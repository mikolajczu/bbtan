using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTan
{
    class Square : IBlock
    {
        int hp;
        int width = 60;
        int height = 60;
        int positionX;
        int positionY;
        Color color;
        public int Hp { get { return hp; } set { hp = value; } }
        public int PositionX { get { return positionX; } set { positionX = value; } }
        public int PositionY { get { return positionY; } set { positionY = value; } }

        public Square(int positionX, int positionY, int hp, int red, int blue, int green)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            color = Color.FromArgb(red, green, blue);
            this.hp = hp;
        }

        public void Show(Graphics e)
        {
            Font font = new Font("Arial", 18);
            e.FillRectangle(new SolidBrush(color), positionX, positionY, width, height);
            e.FillRectangle(new SolidBrush(Color.Black), positionX + 5, positionY + 5, width - 10, height - 10);
            e.DrawString(hp.ToString(), font, new SolidBrush(Color.White), positionX + 16, positionY + 16);
        }
    }
}
