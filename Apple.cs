using System;

namespace Snake
{
    class Apple
    {
        static public Vector2 pos = Vector2.ZERO;
        static public bool exists = false;

        static public void Spawn()
        {
            var rand = new Random();

            int x = rand.Next() % Program.VIEW_WIDTH;
            int y = rand.Next() % Program.VIEW_HEIGHT;

            pos.x = x;
            pos.y = y;

            exists = true;
        }
    }
}
