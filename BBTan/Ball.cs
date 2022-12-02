using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTan
{
    public class Ball
    {
        const int x = 20; // width and height
        public int whichT; // if ball hit triangle which one is it
        float accelerationX;
        float accelerationY;
        float positionX;
        float positionY;
        public float AccelerationX { set { accelerationX = value; } get { return accelerationX; } }
        public float AccelerationY { set { accelerationY = value; } get { return accelerationY; } }
        public float PositionX { get { return positionX; } set { positionX = value; } }
        public float PositionY { get { return positionY; } set { positionY = value; } }
        public bool Enabled { get; set; }

        public Ball(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            Enabled = true;
        }

        public void Show(Graphics e)
        {
            e.FillEllipse(new SolidBrush(Color.White), positionX, positionY, x, x);
        }

        public void Move(int width, int height, int hit)
        {
            if (positionX + 10 < 0)
                positionX = 20;
            else if (positionX - 10 > width)
                positionX = width - x;
            else if (positionY <= 0)
            {
                accelerationY = -accelerationY;
            }
            if (positionX >= width - x || hit == 2 || positionX <= 0)
                accelerationX = -accelerationX;
            else if (positionY >= height - 10)
            {
                accelerationY = 0;
                accelerationX = 0;
                positionY = height - x;
                Enabled = false;
            }
            else if (hit == 1)
                accelerationY = -accelerationY;
            else if(hit == 3)
            {
                accelerationX = -accelerationX;
                accelerationY = -accelerationY;
            }
            //triangle
            else if (hit == 4)
            {
                (accelerationY, accelerationX) = (accelerationX, accelerationY);
                if (whichT == 0)
                {
                    if (accelerationX < 0)
                        accelerationX = -accelerationX;
                    if (accelerationY > 0)
                        accelerationY = -accelerationY;
                }
                else if(whichT == 1)
                {
                    if (accelerationX > 0)
                        accelerationX = -accelerationX;
                    if (accelerationY > 0)
                        accelerationY = -accelerationY;
                }
                else if (whichT == 2)
                {
                    if (accelerationX > 0)
                        accelerationX = -accelerationX;
                    if (accelerationY < 0)
                        accelerationY = -accelerationY;
                }
                else if (whichT == 3)
                {
                    if (accelerationX < 0)
                        accelerationX = -accelerationX;
                    if (accelerationY < 0)
                        accelerationY = -accelerationY;
                }
                if (accelerationY < 1 && accelerationY >= 0)
                    accelerationY += 1;
                else if (accelerationY < 0 && accelerationY > -1)
                    accelerationY -= 1;
            }
            positionX += accelerationX;
            positionY += accelerationY;
        }

        public void Shot(int width, int height, int cursorX, int cursorY)
        {
            int a = 6; // ball's acceleration
            int height1 = height - cursorY;
            int width1;
            if (positionX <= cursorX)
                width1 = (width - (int)positionX) - (width - cursorX);
            else
                width1 = width - (width - (int)positionX) - cursorX;
            if (height1 >= width1)
            {
                accelerationY = a;
                float percentage = width1 * 100 / height1;
                accelerationX = a * percentage / 100;
            }
            else
            {
                accelerationX = a;
                float percentage = height1 * 100 / width1;
                accelerationY = a * percentage / 100;
            }
            if (cursorX < positionX)
                accelerationX = -accelerationX;
            accelerationY = -accelerationY;
        }
    }
}
