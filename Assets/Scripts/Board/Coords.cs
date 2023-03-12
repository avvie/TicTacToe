namespace Board
{
    public struct Coords
    {
        public int x { get; }
        public int y { get; }

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString() => $"Coord: {x},{y}";
    }
}