using System;

namespace Snake
{
    class Renderer
    {
        int viewWidth, viewHeight;
        int cursorX, cursorY;
        char[,] buffer, prevbuffer;

        public Renderer(int viewWidth, int viewHeight)
        {
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;

            this.buffer = new char[viewHeight, viewWidth];
            this.prevbuffer = new char[viewHeight, viewWidth];
            Clear();

            this.cursorX = 0;
            this.cursorY = 0;
        }

        public void Clear()
        {
            for (int y = 0; y < viewHeight; ++y)
            {
                for (int x = 0; x < viewWidth; ++x)
                {
                    buffer[y, x] = ' ';
                }
            }
        }
        
        public void Write(string str)
        {
            for (int i = 0; i < str.Length; ++i)
            {
                Write(str[i]);
            }
        }

        public void Write(char ch)
        {
            buffer[cursorY, cursorX] = ch;

            int cursorNextX = (cursorX + 1);
            cursorY = ((cursorNextX / viewWidth) + cursorY) % viewHeight;
            cursorX = cursorNextX % viewWidth;
        }

        public void Present()
        {
            for (int y = 0; y < viewHeight; ++y)
            {
                for (int x = 0; x < viewWidth; ++x)
                {
                    if (buffer[y, x] != prevbuffer[y, x])
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(buffer[y, x]);
                    }
                }
            }

            //Console.SetCursorPosition(0, 0);

            CloneBuffer();
        }

        public void FullPresent()
        {
            for (int y = 0; y < viewHeight; ++y)
            {
                Console.SetCursorPosition(0, y);

                for (int x = 0; x < viewWidth; ++x)
                {
                    Console.Write(buffer[y, x]);
                }
            }

            Console.SetCursorPosition(0, 0);
        }

        void CloneBuffer()
        {
            prevbuffer = (char[,]) buffer.Clone();
        }

        public void SetCursor(int x, int y)
        {
            cursorX = x;
            cursorY = y;
        }

        public int GetViewWidth()
        {
            return viewWidth;
        }

        public int GetViewHeight()
        {
            return viewHeight;
        }
    }
}
