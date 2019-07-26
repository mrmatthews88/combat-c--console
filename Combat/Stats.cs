using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;
namespace Combat
{
    class Stats : Display
    {
        public Stats(int _x, int _y, int _width, int _height, bool _border = true) : base(_x, _y, _width, _height, _border){ topBar = true; }
        public Person person;
        
        void DrawBar(int x, int y, int percent, ConsoleColor color = ConsoleColor.White, string type = "")
        {
            Console.SetCursorPosition(this.x + x + borderPadding(), this.y + y + borderPadding());
            Console.ForegroundColor = color;
            Write($"{type}::");
            for (int i = 0; i < 100; i++)
            {
                string _char = (i < percent) ? "\u2588" : "<";
                Write(_char);
            }
            Console.ResetColor();
        }

        public void Draw()
        {
            WriteName();
            WriteStats();

            //health
            DrawBar(0, 2, (int)person.healthPercent(), ConsoleColor.Red, "HP");
            // AP
            DrawBar(0, 3, (int)person.APPercent(), ConsoleColor.Green, "AP");
        }

        void WriteName()
        {
            Console.ResetColor();
            SetCursorPosition(0, 0);
            Write(person.name);
        }

        void WriteStats()
        {

            Console.ResetColor();
            SetCursorPosition(person.name.Length+5, 0);
            Write($"HP ({person.health})");
        }

        protected override void DrawBorder()
        {
            
            base.DrawBorder();
            WriteName();
            WriteStats();
        }


    }
}
