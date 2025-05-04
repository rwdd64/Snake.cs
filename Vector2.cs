using System.Data.Common;

namespace Snake
{
    struct Vector2
    {
        public static readonly Vector2 ZERO = new Vector2(0, 0);
        public static readonly Vector2 RIGHT = new Vector2(1, 0);
        public static readonly Vector2 LEFT = new Vector2(-1, 0);
        public static readonly Vector2 UP = new Vector2(0, -1);
        public static readonly Vector2 DOWN = new Vector2(0, 1);

        public int x, y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 Inverse()
        {
            return new Vector2(-this.x, -this.y);
        }

        public static bool Equals(Vector2 a, Vector2 b)
        {
            return (a.x == b.x && a.y == b.y);
        }

        public bool Equals(Vector2 other)
        {
            return (this.x == other.x && this.y == other.y);
        }

        public Vector2 Add(Vector2 other)
        {
            return new Vector2(this.x + other.x, this.y + other.y);
        }

        public static Vector2 Add(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 Sub(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 Mul(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 Mul(Vector2 a, int scalar)
        {
            return new Vector2(a.x * scalar, a.y * scalar);
        }
    }
}
