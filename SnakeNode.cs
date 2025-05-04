
namespace Snake
{
    class SnakeNode
    {
        public Vector2 pos;

        public SnakeNode(Vector2 pos)
        {
            this.pos = pos;
        }

        public void SetPos(Vector2 pos) {
            this.pos = pos;
        }
    }
}
