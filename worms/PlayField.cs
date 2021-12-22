using System;

namespace worms
{
    internal class PlayField
    {
        public Point[,] matrix { get; set; }

        public int size { get; set; }

        public PlayField(int size)
        {
            this.size = size + 1;
            matrix = new Point[this.size, this.size];
        }

        public void Init()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    matrix[x, y] = new Point(x, y, FieldState.EMPTY);
                }
            }
        }

        public bool IsOutOfRange(int x, int y)
        {
            int bound = size / 2;
            if (Math.Abs(x) > bound || Math.Abs(y) > bound)
            {
                return true;
            }
            return false;
        }

        public FieldState GetFieldState(int x, int y)
        {
            int bound = size / 2;
            if (Math.Abs(x) > bound || Math.Abs(y) > bound)
            {
                throw new Exception("Coord is out of range");
            }
            return matrix[x + bound, y + bound].state;
        }

        public void SetFieldState(int x, int y, FieldState state)
        {
            int bound = size / 2;
            if (Math.Abs(x) > bound || Math.Abs(y) > bound)
            {
                throw new Exception("Coord is out of range");
            }
            matrix[x + bound, y + bound].state = state;

        }


    }
}