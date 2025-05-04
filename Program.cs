using System;
using System.Threading;
using System.Diagnostics;

namespace Snake
{
    class Program
    {
        public const int WAIT_TIME_MS = 200;

        public const int MAX_SCORE = 999_999;
        public const int APPLE_SCORE = 100;
        public static readonly int MAX_SCORE_DIG =
            MAX_SCORE.ToString().Length;

        public static readonly int VIEW_WIDTH = 20;
        public static readonly int VIEW_HEIGHT = 20;
        public static readonly int VIEW_X =
            Console.WindowWidth/2 - VIEW_WIDTH/2;
        public static readonly int VIEW_Y =
            Console.WindowHeight/2 - VIEW_HEIGHT/2;

        public static bool running;
        public static int score;
        public static Snake snake;
        public static Renderer renderer;

        static void HandleKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Escape:
                case ConsoleKey.Q:
                    running = false;
                    break;
                case ConsoleKey.RightArrow:
                    snake.Turn(Vector2.RIGHT);
                    break;
                case ConsoleKey.LeftArrow:
                    snake.Turn(Vector2.LEFT);
                    break;
                case ConsoleKey.UpArrow:
                    snake.Turn(Vector2.UP);
                    break;
                case ConsoleKey.DownArrow:
                    snake.Turn(Vector2.DOWN);
                    break;
            }
        }

        static void Render()
        {
            renderer.Clear();

            string score_msg = "Score: ";

            renderer.SetCursor(
                VIEW_X-score_msg.Length-MAX_SCORE_DIG,
                VIEW_Y
            );

            renderer.Write(score_msg + score);

            renderer.SetCursor(VIEW_X, VIEW_Y);
            for (int y = 0; y < VIEW_HEIGHT; ++y)
            {
                renderer.SetCursor(VIEW_X, VIEW_Y + y);
                for (int x = 0; x < VIEW_WIDTH; ++x)
                {
                    renderer.Write(".");
                }
            }

            if (Apple.exists)
            {
                renderer.SetCursor(VIEW_X + Apple.pos.x, VIEW_Y + Apple.pos.y);
                renderer.Write("@");
            }

            for (int i = 0; i < snake.nodes.Count; ++i)
            {
                renderer.SetCursor(
                    VIEW_X + snake.nodes[i].pos.x,
                    VIEW_Y + snake.nodes[i].pos.y
                );
                renderer.Write("*");
            }

            renderer.SetCursor(VIEW_X + snake.pos.x, VIEW_Y + snake.pos.y);
            if (Vector2.Equals(snake.vel, Vector2.UP))
                renderer.Write("^");
            else if (Vector2.Equals(snake.vel, Vector2.LEFT))
                renderer.Write("<");
            else if (Vector2.Equals(snake.vel, Vector2.RIGHT))
                renderer.Write(">");
            else
                renderer.Write("v");

            //renderer.FullPresent();
            renderer.Present();
        }

        static void Update()
        {
            snake.Move();

            if (!Apple.exists)
                Apple.Spawn();

            if (Vector2.Equals(snake.pos, Apple.pos))
            {
                snake.AddNode();
                if (score < MAX_SCORE)
                {
                    score = Math.Min(
                        score+APPLE_SCORE,
                        MAX_SCORE
                    );
                }
                Apple.exists = false;
            }

            for (int i = 0; i < snake.nodes.Count; ++i) {
                if (Vector2.Equals(snake.pos, snake.nodes[i].pos))
                {
                    running = false;
                    break;
                }
            }
        }

        static void EnvSet()
        {
            Console.Clear();
            Console.Title = "Snake";
            Console.CursorVisible = false;
        }

        static void EnvReset()
        {
            Console.SetCursorPosition(0, Console.WindowHeight-1);
            Console.CursorVisible = true;
        }

        static void Main(string[] args)
        {
            EnvSet();

            renderer = new Renderer(
                    Console.WindowWidth,
                    Console.WindowHeight
                );

            snake = new Snake();

            running = true;
            bool first_it = true;

            score = 0;

            var watch = new Stopwatch();

            while (running)
            {
                if (first_it)
                    first_it = false;
                else
                    Update();

                if (!running) break;

                Render();

                watch.Start();
                while (watch.Elapsed.Milliseconds < WAIT_TIME_MS)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKey key = Console.ReadKey(true).Key;
                        HandleKey(key);
                    }
                    if (!running) break;
                }
                watch.Reset();
            }

            EnvReset();
            Console.WriteLine("GAME OVER");
            Console.WriteLine($"Final score: {score}");
        }
    }
}
