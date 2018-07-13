using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MazeLibrary
{
    /// <summary>
    /// Class for work with maze
    /// </summary>
    public class MazeSolver
    {
        private readonly int startY;
        private readonly int startX;
        private readonly int length;
        private readonly int width;
        private readonly int[,] mazeModel;
        
        public MazeSolver(int[,] mazeModel, int startX, int startY)
        {
            this.startY = startY;
            this.startX = startX;
            Validate(mazeModel, startX, startY);


            length = (int) Math.Sqrt(mazeModel.Length);
            width = (int) Math.Sqrt(mazeModel.Length);
            

            this.mazeModel = new int[length,width];

            Array.Copy(mazeModel, this.mazeModel, mazeModel.Length);

        }

        public int[,] MazeWithPass()
        {
            return (int[,])mazeModel.Clone();
        }

        public List<Point> PointsForExit()
        {
            var pList = new List<Point>();

            int max = 0;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (mazeModel[i,j]>max)
                    {
                        max = mazeModel[i,j];
                    }
                }
            }

            for (int i = 1; i <= max; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int k = 0; k < width; k++)
                    {
                        if (i==mazeModel[j,k])
                        {
                            pList.Add(new Point(j,k));
                            
                        }
                    }
                }
            }

            return pList;


        }

        public void PassMaze() => FindAWay();

        private void FindAWay()
        {
            int n = length;
            int m = width;

            bool[,] mapBools = new bool[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    mapBools[i, j] = (mazeModel[i, j] == 0);
                }
            }

            int inc = 1;

            NextStep(mapBools,startX, startY, inc);
        }

        private bool NextStep(bool [,] map,int x, int y, int inc)
        {

            if (!(x == startX && y == startY))
            {
                if (x < 0 || y < 0)
                {
                    return true;
                }

                if (x >= length || y >= width)
                {
                    return true;
                }

                if (!map[x, y])
                {
                    return false;
                }
            }

            map[x, y] = false;

            var up = NextStep(map,x + 1, y, inc+1);
            var down = NextStep(map,x - 1, y, inc+1);
            var right = NextStep(map,x, y + 1, inc + 1);
            var left = NextStep(map,x, y - 1, inc + 1);

            if (up || down || right || left)
            { 
                mazeModel[x, y] = inc;
               
                return true;
            }

            return false;
        }

        private void Validate(int[,] mazeModel, int startX, int startY)
        {
            if (mazeModel==null)
            {
                throw new ArgumentNullException($"{nameof(mazeModel)}");
            }

            if (mazeModel.Length==0)
            {
                throw new ArgumentException();
            }

            if (startX < 0 || startX>mazeModel.Length || startY<0 || startY>mazeModel.Length)
            {
                throw new ArgumentException();
            }
        }
    }

    public struct Point
    {
        private int x, y;

        public Point(int x,int y) : this()
        {
            X = x;
            Y = y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
