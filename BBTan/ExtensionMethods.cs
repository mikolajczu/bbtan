using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTan
{
    public static class ExtensionMethods
    {
        public static void Move(this IBlock[,] blocks, int lvl)
        {
            for (int i = 0; i < lvl; i++)
                for (int j = 0; j < blocks.GetLength(1); j++)
                    blocks[i, j].PositionY += 65;
        }

        public static bool IsEnd(this IBlock[,] blocks, int height)
        {
            for (int i = 0; i < blocks.GetLength(0); i++)
                for (int j = 0; j < blocks.GetLength(1); j++)
                    if (blocks[i, j].PositionY >= height - 50)
                    {
                        if (blocks[i, j] is Bonus)
                            continue;
                        else
                            return true;
                    }
            return false;
        }

        public static bool AreBack(this List<Ball> balls, int height)
        {
            for(int i = 0; i < balls.Count; i++)
                if (balls[i].PositionY < height)
                    return false;
            return true;
        }
    }
}
