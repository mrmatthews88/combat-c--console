using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combat
{
    class Display
    {
        public bool topBar = false;
        protected const string BORDER_BOTTOM_LEFT_CORNER = "\u255A";
        protected const string BORDER_BOTTOM_RIGHT_CORNER = "\u255D";
        protected const string BORDER_BOTTOM = "\u2550";
        protected const string BORDER_HORIZONTAL = "\u2550";
        protected const string BORDER_TOP_LEFT_CORNER = "\u2554";
        protected const string BORDER_LEFT_JOINT = "\u2560";
        protected const string BORDER_RIGHT_JOINT = "\u2563";
        protected const string BORDER_TOP_RIGHT_CORNER = "\u2557";
        protected const string BORDER_SIDE = "\u2551";

        protected int x;
        protected int y;
        protected int width;
        protected int height;
        protected bool border = false;
        public bool visible = true;

        protected int borderPadding() => border ? 1 : 0;

        public ConsoleColor borderColor = ConsoleColor.White;
        public ConsoleColor ForeColor = ConsoleColor.White;

        public Display(int _x, int _y, int _width, int _height, bool _border = false)
        {
            this.x = _x;
            this.y = _y;
            this.width = _width;
            this.height = _height;
            this.border = _border;
            Console.CursorVisible = false;
        }
        protected void SetCursorPosition(int _x, int _y)
        {
            try
            {
                Console.SetCursorPosition(x + _x + borderPadding(), y + _y + borderPadding());
            }
            catch
            {
                Console.SetCursorPosition(1,1);
            }
        }
        protected virtual void DrawBorder()
        {
            if (!border) return;
            Console.ForegroundColor = borderColor;
            Console.SetCursorPosition(x, y);
            for (int row = 0; row < height; row++)
            {
                string thisLine = "";
                for (int col = 0; col < width; col++)
                {


                    if (row == 0)
                    {
                        if (col == 0) thisLine +=BORDER_TOP_LEFT_CORNER;
                        else if (col == width - 1) thisLine+= BORDER_TOP_RIGHT_CORNER;
                        else thisLine+=BORDER_HORIZONTAL;
                    }
                    else if (row == height - 1)
                    {
                        if (col == 0) thisLine += BORDER_BOTTOM_LEFT_CORNER;
                        else if (col == width - 1) thisLine += BORDER_BOTTOM_RIGHT_CORNER;
                        else thisLine += BORDER_HORIZONTAL;
                    }
                    else if (col == 0 || col == width - 1)
                    {
                        thisLine += BORDER_SIDE;
                    }
                    else thisLine += (' ');
                }
                Console.Write(thisLine);
                try
                {
                    Console.SetCursorPosition(x, Console.CursorTop + 1);
                }
                catch
                {
                    Console.SetCursorPosition(1,1);
                }
            }
            Console.ResetColor();
        }
        public void WriteLine(string _string)
        {
            Console.CursorLeft = x + borderPadding();
            Console.Write(_string);
            Console.SetCursorPosition(x + borderPadding(), Console.CursorTop + 1);
        }
        public void Write(string _string)
        {
            Console.Write(_string);

        }
        public void Write(char _char) => Console.Write(_char);
        public void WriteTopBar(string _string)
        {
            SetCursorPosition(0, 0);
            Console.Write(_string);
        }
        public void refresh()
        {
            Clear();
            DrawBorder();
            DrawTopBar();
        }

        public void DrawTopBar()
        {
            if (!topBar) return;

            Console.ForegroundColor = borderColor;
            Console.SetCursorPosition(x, y + 2);
            for (int col = 0; col < width; col++)
            {
                if (col == 0) Write(BORDER_LEFT_JOINT);
                else if (col == width - 1) Write(BORDER_RIGHT_JOINT);
                else Write(BORDER_HORIZONTAL);
            }
            SetCursorPosition(0, 0);
        }

        public void Clear()
        {
            Console.ResetColor();
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (border)
                    {
                        if (row > 0 && row < height-1 && col > 0 && col < width-1)
                        {

                            Console.SetCursorPosition(col + x, row + y);
                            Console.Write(' ');
                        }
                    }
                    else
                    {
                        if (row >= 0 && row < height && col >= 0 && col < width)
                        {
                            Console.SetCursorPosition(col+x, row+y);
                            Console.Write(' ');
                        }
                    }
                }
            }
            DrawTopBar();

            SetCursorPosition(0, 0);
        }
    }
}
