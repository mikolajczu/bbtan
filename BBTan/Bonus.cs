using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTan
{
    public enum Name
    {
        ADDBALL,
        DESTROYVERTICAL,
        DESTROYHORIZONTAL
    }

    class Bonus : IBlock
    {
        public Name name;
        int hp;
        int positionX;
        int positionY;
        Color color;
        const int width = 30;
        const int height = 30;

        public int Hp { get { return hp; } set { hp = value; } }
        public int PositionX { get { return positionX; } set { positionX = value; } }
        public int PositionY { get { return positionY; } set { positionY = value; } }

        public Bonus(int positionX, int positionY, int red, int blue, int green, int r)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            color = Color.FromArgb(red, green, blue);
            hp = 1;
            if (r == 0)
                name = Name.ADDBALL;
            else if (r == 1)
                name = Name.DESTROYVERTICAL;
            else
                name = Name.DESTROYHORIZONTAL;
        }

        public void Show(Graphics e)
        {
            Font font = new Font("Arial", 17);
            e.FillEllipse(new SolidBrush(color), positionX + width / 2, positionY + height / 2, width, height);
            e.FillEllipse(new SolidBrush(Color.Black), positionX + width / 2 + 4, positionY + height / 2 + 4, width - 8, height - 8);
            if (name == Name.ADDBALL)
                e.DrawString("+", new Font("Arial", 22), new SolidBrush(Color.White), positionX + width / 2 + 2, positionY + height / 2 - 1);
            else if (name == Name.DESTROYVERTICAL)
                e.DrawString("|", font, new SolidBrush(Color.White), positionX + width / 2 + 9, positionY + height / 2);
            else
                e.DrawString("_", font, new SolidBrush(Color.White), positionX + width / 2 + 5, positionY + height / 2 - 8);
        }
    }
}
