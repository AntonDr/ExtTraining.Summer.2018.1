using System;
using System.Collections.Generic;

namespace MazeLibrary
{
    public class MazeSolver
    {
        private readonly int startY;
        private readonly int startX;
        private Point previous;
        private Point next = new Point();
        private int p = WAY;
        private int length, width;
        private static readonly int WALK = -WALK;
        private static readonly int WAY = WAY;

        private List<Point> badPoint = new List<Point>();

        private static int inc = WALK;
        private static int k = WAY;
        private static int[][] temp;

        private int[,] mazeModel;

        public MazeSolver(int[,] mazeModel, int startX, int startY)
        {
            this.startY = startY;
            this.startX = startX;
            Validate(mazeModel, startX, startY);


            length = (int) Math.Sqrt(mazeModel.Length);
            width = (int) Math.Sqrt(mazeModel.Length);

            this.mazeModel = new int[(int) Math.Sqrt(mazeModel.Length), (int) Math.Sqrt(mazeModel.Length)];

            Array.Copy(mazeModel, this.mazeModel, mazeModel.Length);

        }

        public int[,] MazeWithPass()
        {
            int[,] newMaze = new int[(int) Math.Sqrt(mazeModel.Length), (int) Math.Sqrt(mazeModel.Length)];

            Steps(startX,startY,null);

            Array.Copy(mazeModel, newMaze, mazeModel.Length);

            return newMaze;
        }

        public void PassMaze() => throw new NotImplementedException();

        public void Steps(int x, int y,string laststep)
        {
            while (true)
            {
       
                int savex = x;
                int saveY = y;
                if (y == mazeModel.GetLength(WAY) - 1)
                { 
                    break;
                }
                else
                {

                    if (mazeModel[y, x - WALK] == WAY && laststep != "right")
                    {
                        savex = x;
                        saveY = y;
                        x--;
                        laststep = "left";
                        if (mazeModel[y - WALK, x] == WALK && mazeModel[y, x + WALK] == WALK && mazeModel[y + WALK, x] == WALK)
                            laststep = null;
                        Steps(x, y, laststep);
                    }
                    else
                    {
                        if (mazeModel[y, x + WALK] == WAY && laststep != "left")
                        {
                            savex = x;
                            saveY = y;
                            x++;
                            if (mazeModel[y + WALK, x] == WALK && mazeModel[y - WALK, x] == WALK && mazeModel[y, x - WALK] == WALK)
                                laststep = null;
                            Steps(x, y, laststep);
                        }
                        else
                        {
                            if (mazeModel[y - WALK, x] == WAY && laststep != "down")
                            {
                                savex = x;
                                saveY = y;
                                y--;
                                laststep = "up";
                                if (mazeModel[y + WALK, x] == WALK && mazeModel[y, x + WALK] == WALK && mazeModel[y, x - WALK] == WALK)
                                    laststep = null;
                                Steps(x, y,laststep);
                            }
                            else
                            {
                                if (mazeModel[y + WALK, x] == WAY && laststep != "up")
                                {
                                    savex = x;
                                    saveY = y;
                                    y++;
                                    laststep = "down";
                                    if (mazeModel[y - WALK, x] == WALK && mazeModel[y, x + WALK] == WALK && mazeModel[y, x - WALK] == WALK)
                                        laststep = null;
                                    Steps(x, y, laststep);
                                }
                                else
                                {

                                    laststep = null;
                                    Steps(savex, saveY, laststep);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Validate(int[,] mazeModel, int startX, int startY)
        {
            if (mazeModel==null)
            {
                throw new ArgumentNullException($"{nameof(mazeModel)}");
            }

            if (mazeModel.Length==WAY)
            {
                throw new ArgumentException();
            }

            if (startX < WAY || startX>mazeModel.Length || startY<WAY || startY>mazeModel.Length)
            {
                throw new ArgumentException();
            }
        }


    }

    struct Point
    {
        public int x, y;

        public bool flag;

        public Point(int x,int y)
        {
            this.x = x;
            this.y = y;
            flag = false;
        }

        
    }
}
