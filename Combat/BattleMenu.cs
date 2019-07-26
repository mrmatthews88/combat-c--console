using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combat
{
    class BattleMenu : Display
    {

        public BattleMenu() : base(2, 28, 20, 20)
        {
            foreach (Action action in Program.player.Actions.Values)
            {
                choices.Add(action);
            }
        }
        int position = 0;
        List<Action> choices = new List<Action>();
        public void drawOption(Action action, int activeOn)
        {
            Console.ResetColor();
            bool active = position == activeOn;
            if (active) Console.ForegroundColor = ConsoleColor.Green;
            WriteLine(action.name + (active ? " <<<" : "    "));
        }
        public Action Choose()
        {
            Console.ResetColor();
            SetCursorPosition(0, 0);
            WriteLine("Select an Action");
            WriteLine("");
            if (position < 0) position = choices.Count + position;
            position = position % choices.Count;
            
            for (int i = 0; i < choices.Count; i++)
            {
                drawOption(choices[i], i);
            }

            var button = Console.ReadKey(true);
            Console.SetCursorPosition(50, Console.LargestWindowHeight - 2);
            Console.Write(button.Key);

            if (button.Key == ConsoleKey.Enter) { return choices[position]; }
            if (button.Key == ConsoleKey.UpArrow) { position -= 1; }
            if (button.Key == ConsoleKey.DownArrow) { position += 1; }
            return Choose();
        }
    }

    /*

     */
    /*

    */
}
