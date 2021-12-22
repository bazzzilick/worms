namespace worms
{
    class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public FieldState state { get; set; }

        public Point()
        {
            x = 0;
            y = 0;
            state = FieldState.EMPTY;
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(int x, int y, FieldState state)
        {
            this.x = x;
            this.y = y;
            this.state = state;
        }

    }
}