using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BBTan
{
    class BBTan
    {
        Thread th;
        List<Ball> balls = new List<Ball>();
        IBlock[,] blocks = new IBlock[11, 6];
        public int lvl = 0;
        bool isThereLastBall = false;
        Ball lastBall;
        int eX, eY;
        int width, height;

        public BBTan(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Start()
        {
            balls.Clear();
            lvl = 0;
            balls.Add(new Ball((width / 2) - 10, height - 20));
            for (int i = 0; i < blocks.GetLength(0); i++)
                for (int j = 0; j < blocks.GetLength(1); j++)
                    blocks[i, j] = new Square(-1000, -1000, 0, 0, 0, 0);
            addLevel();
        }

        private void addLevel()
        {
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                int red = r.Next(255);
                int green = r.Next(255);
                int blue = r.Next(255);
                int which = r.Next(4);
                int rn = r.Next(4);
                if (which == 0)
                    blocks[lvl % 11, i] = new Square(65 * i + 5, 5, lvl + 2, red, green, blue);
                else if (which == 1)
                    blocks[lvl % 11, i] = new Triangle(65 * i + 5, 5, lvl + 2, red, green, blue, rn);
                else
                    blocks[lvl % 11, i] = new Bonus(65 * i + 5, 5, red, green, blue, rn);

            }
            lvl++;
        }

        private int hit(Ball ball)
        {
            int i = 0;
            for (int j = 0; j < blocks.GetLength(0); j++)
            {
                for (int k = 0; k < blocks.GetLength(1); k++)
                {
                    if (blocks[j, k] == null)
                        return i;
                    for (int b = 0; b < balls.Count; b++)
                    {
                        if (ball.PositionX + 20 >= blocks[j, k].PositionX && ball.PositionX < blocks[j, k].PositionX && ball.PositionY >= blocks[j, k].PositionY - 10 && ball.PositionY + 20 <= blocks[j, k].PositionY + 70)
                            i = 2;
                        else if (ball.PositionX <= blocks[j, k].PositionX + 60 && ball.PositionX + 20 > blocks[j, k].PositionX + 60 && ball.PositionY >= blocks[j, k].PositionY - 10 && ball.PositionY + 20 <= blocks[j, k].PositionY + 70)
                            i = 2;
                        else if (ball.PositionX >= blocks[j, k].PositionX - 10 && ball.PositionX + 20 <= blocks[j, k].PositionX + 70 && ball.PositionY + 20 >= blocks[j, k].PositionY && ball.PositionY < blocks[j, k].PositionY)
                            i = 1;
                        else if (ball.PositionX >= blocks[j, k].PositionX - 10 && ball.PositionX + 20 <= blocks[j, k].PositionX + 70 && ball.PositionY <= blocks[j, k].PositionY + 60 && ball.PositionY + 20 > blocks[j, k].PositionY + 60)
                            i = 1;
                        else if (ball.AccelerationX > 0 && ball.AccelerationY > 0 && ball.PositionX + 20 >= blocks[j, k].PositionX && ball.PositionX + 20 <= blocks[j, k].PositionX + 10 && ball.PositionY + 20 <= blocks[j, k].PositionY + 10 && ball.PositionY + 20 >= blocks[j, k].PositionY)
                            i = 3;
                        else if (ball.AccelerationX < 0 && ball.AccelerationY > 0 && ball.PositionX <= blocks[j, k].PositionX + 60 && ball.PositionX >= blocks[j, k].PositionX + 50 && ball.PositionY + 20 <= blocks[j, k].PositionY + 10 && ball.PositionY + 20 >= blocks[j, k].PositionY)
                            i = 3;
                        else if (ball.AccelerationX < 0 && ball.AccelerationY < 0 && ball.PositionX <= blocks[j, k].PositionX + 60 && ball.PositionX >= blocks[j, k].PositionX + 50 && ball.PositionY <= blocks[j, k].PositionY + 70 && ball.PositionY >= blocks[j, k].PositionY + 50)
                            i = 3;
                        else if (ball.AccelerationX > 0 && ball.AccelerationY < 0 && ball.PositionX + 20 >= blocks[j, k].PositionX && ball.PositionX + 20 <= blocks[j, k].PositionX + 10 && ball.PositionY <= blocks[j, k].PositionY + 70 && ball.PositionY >= blocks[j, k].PositionY + 50)
                            i = 3;
                        if (i != 0)
                        {
                            blocks[j, k].Hp--;
                            if (blocks[j, k].Hp == 0)
                            {
                                blocks[j, k].PositionX = -100;
                                blocks[j, k].PositionY = -100;
                            }
                            if (blocks[j, k] is Bonus bonus)
                            {
                                if (bonus.name == Name.DESTROYVERTICAL)
                                {
                                    for (int l = 0; l < blocks.GetLength(0); l++)
                                    {
                                        if (blocks[l, k] is Bonus)
                                            continue;
                                        else
                                        {
                                            blocks[l, k].Hp--;
                                            if (blocks[l, k].Hp <= 0)
                                            {
                                                blocks[l, k].PositionX = -100;
                                                blocks[l, k].PositionY = -100;
                                            }
                                        }
                                    }
                                }
                                else if (bonus.name == Name.DESTROYHORIZONTAL)
                                {
                                    for (int l = 0; l < blocks.GetLength(1); l++)
                                    {
                                        if (blocks[j, l] is Bonus)
                                            continue;
                                        else
                                        {
                                            blocks[j, l].Hp--;
                                            if (blocks[j, l].Hp <= 0)
                                            {
                                                blocks[j, l].PositionX = -100;
                                                blocks[j, l].PositionY = -100;
                                            }
                                        }
                                    }
                                }
                                else
                                    balls.Add(new Ball((width / 2) - 10, height - 20));
                                return 0;
                            }
                            else if(blocks[j,k] is Triangle triangle)
                            {
                                if (triangle.Which == 0)
                                {
                                    if (ball.AccelerationY > 0)
                                    {
                                        ball.whichT = 0;
                                        return 4;
                                    }
                                }
                                else if (triangle.Which == 1)
                                {
                                    if (ball.AccelerationY > 0)
                                    {
                                        ball.whichT = 1;
                                        return 4;
                                    }
                                }
                                else if (triangle.Which == 2)
                                {
                                    if (ball.AccelerationY < 0)
                                    {
                                        ball.whichT = 2;
                                        return 4;
                                    }
                                }
                                else if (triangle.Which == 3)
                                {
                                    if (ball.AccelerationY < 0)
                                    {
                                        ball.whichT = 3;
                                        return 4;
                                    }
                                }

                            }
                            return i;
                        }
                    }
                }
            }
            return i;
        }

        public void BallShot()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Shot(width, height, eX - 10, eY + 10);
                Thread.Sleep(100);
            }
        }

        public void Show(Graphics e)
        {
            for (int i = 0; i < balls.Count; i++)
                balls[i].Show(e);
            for (int i = 0; i < blocks.GetLength(0); i++)
                for (int j = 0; j < blocks.GetLength(1); j++)
                    if (blocks[i, j].Hp > 0)
                        blocks[i, j].Show(e);
        }

        public bool Move()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Move(width, height, hit(balls[i]));
                if (isThereLastBall == false && balls[i].PositionY >= height - 20)
                {
                    isThereLastBall = true;
                    lastBall = balls[i];
                }
            }
            if (balls.AreBack(height - 20))
            {
                for (int j = 0; j < balls.Count; j++)
                {
                    balls[j].PositionY = height - 20;
                    balls[j].PositionX = lastBall.PositionX;
                }
                blocks.Move(blocks.GetLength(0));
                addLevel();
                return true;
            }
            return false;
        }

        public bool End()
        {
            if (blocks.IsEnd(height))
                return true;
            return false;
        }

        public void Shot(int x, int y)
        {
            if (balls.AreBack(height - 20))
            {
                eX = x;
                eY = y;
                th = new Thread(BallShot);
                th.Start();
                lastBall = null;
                isThereLastBall = false;
            }
        }
    }
}
