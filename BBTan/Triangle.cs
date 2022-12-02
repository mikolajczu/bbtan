using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTan
{
    class Triangle : IBlock
    {
        int hp;
        int positionX;
        int positionY;
        int which;
        Color color;
        Point[] point = new Point[3];
        Point[] point2 = new Point[3];
        const int width = 60;
        const int height = 60;
        public int Which { get { return which; } set { which = value; } }
        public int Hp { get { return hp; } set { hp = value; } }
        public int PositionX { get { return positionX; } set { positionX = value; } }
        public int PositionY
        {
            get { return positionY; }
            set
            {
                positionY = value - positionY;
                point[0].Y += positionY;
                point[1].Y += positionY;
                point[2].Y += positionY;
                point2[0].Y += positionY;
                point2[1].Y += positionY;
                point2[2].Y += positionY;
                positionY = value;
            }
        }

        public Triangle(int positionX, int positionY, int hp, int red, int blue, int green, int r)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            color = Color.FromArgb(red, green, blue);
            this.hp = hp;
            which = r;
            if (r == 0)
            {
                point[0] = new Point(positionX, positionY);
                point[1] = new Point(positionX, positionY + height);
                point[2] = new Point(positionX + width, positionY + height);
                point2[0] = new Point(positionX + 5, positionY + 13);
                point2[1] = new Point(positionX + 5, positionY + height - 5);
                point2[2] = new Point(positionX + width - 13, positionY + height - 5);
            }
            else if (r == 1)
            {
                point[0] = new Point(positionX, positionY + height);
                point[1] = new Point(positionX + width, positionY + height);
                point[2] = new Point(positionX + width, positionY);
                point2[0] = new Point(positionX + 13, positionY + height - 5);
                point2[1] = new Point(positionX + width - 5, positionY + height - 5);
                point2[2] = new Point(positionX + width - 5, positionY + 13);
            }
            else if (r == 2)
            {
                point[0] = new Point(positionX, positionY);
                point[1] = new Point(positionX + width, positionY);
                point[2] = new Point(positionX + width, positionY + height);
                point2[0] = new Point(positionX + 13, positionY + 5);
                point2[1] = new Point(positionX + width - 5, positionY + 5);
                point2[2] = new Point(positionX + width - 5, positionY + height - 13);
            }
            else
            {
                point[0] = new Point(positionX, positionY);
                point[1] = new Point(positionX, positionY + height);
                point[2] = new Point(positionX + width, positionY);
                point2[0] = new Point(positionX + 5, positionY + 5);
                point2[1] = new Point(positionX + 5, positionY + height - 13);
                point2[2] = new Point(positionX + width - 13, positionY + 5);
            }
        }

        public void Show(Graphics e)
        {
            Font font = new Font("Arial", 18);
            e.FillPolygon(new SolidBrush(color), point);
            e.FillPolygon(new SolidBrush(Color.Black), point2);
            if(which == 0)
                e.DrawString(hp.ToString(), font, new SolidBrush(Color.White), positionX + 6, positionY + 28);
            else if(which == 1)
                e.DrawString(hp.ToString(), font, new SolidBrush(Color.White), positionX + 30, positionY + 28);
            else if (which == 2)
                e.DrawString(hp.ToString(), font, new SolidBrush(Color.White), positionX + 30, positionY + 6);
            else
                e.DrawString(hp.ToString(), font, new SolidBrush(Color.White), positionX + 6, positionY + 6);
        }
    }
}
