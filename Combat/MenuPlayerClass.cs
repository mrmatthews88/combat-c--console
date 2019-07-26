using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combat
{
    class MenuPlayerClass : Display
    {
        List<string> Title = new List<string>();
        List<string> Knight = new List<string>();
        List<string> Wizard = new List<string>();
        public MenuPlayerClass(int _x, int _y, int _width, int _height, bool _border = false) : base(_x, _y, _width, _height, _border) {
            setKnight();
            setWizard();
            setTitle();
            choices.Add(new Knight("Player"));
            choices.Add(new Wizard("Player"));
        }
        int position = 0;
        List<Person> choices = new List<Person>();
        public void drawOption(List<string> ASCII, int activeOn)
        {
            Console.ResetColor();
            if (position == activeOn) Console.ForegroundColor = ConsoleColor.Green;
            foreach (var line in ASCII)
            {
                WriteLine(line);
            }
        }
        public Person Choose()
        {
            

            if (position < 0) position = 2 + position;
            position = position % 2;
            Console.SetCursorPosition(x, y);
            drawOption(Title, -1);
            Console.CursorTop += 4;
            drawOption(Knight, 0);
            Console.CursorTop += 4;
            drawOption(Wizard, 1);

            var button = Console.ReadKey(true);
            Console.SetCursorPosition(50, Console.LargestWindowHeight-2);
            Console.Write(button.Key);

            if (button.Key == ConsoleKey.Enter) {  return choices[position]; }
            if (button.Key == ConsoleKey.UpArrow) { position -= 1; }
            if (button.Key == ConsoleKey.DownArrow) { position += 1; }
            return Choose();
        }
        void setTitle()
        {
            Title.Add(" .::::::..,::::::   :::    .,::::::   .,-:::::::::::::::::      .,-:::::   :::      :::.     .::::::.  .::::::. ");
            Title.Add(";;;`    `;;;;''''   ;;;    ;;;;'''' ,;;;'````';;;;;;;;''''    ,;;;'````'   ;;;      ;;`;;   ;;;`    ` ;;;`    ` ");
            Title.Add("'[==/[[[[,[[cccc    [[[     [[cccc  [[[            [[         [[[          [[[     ,[[ '[[, '[==/[[[[,'[==/[[[[,");
            Title.Add("  '''    $$$\"\"\"\"    $$'     $$\"\"\"\"  $$$            $$         $$$          $$'    c$$$cc$$$c  '''    $  '''    $");
            Title.Add(" 88b    dP888oo,__ o88oo,.__888oo,__`88bo,__,o,    88,        `88bo,__,o, o88oo,.__888   888,88b    dP 88b    dP");
            Title.Add("  \"YMmMY\" \"\"\"\"YUMMM\"\"\"\"YUMMM\"\"\"\"YUMMM \"YUMMMMMP\"   MMM          \"YUMMMMMP\"\"\"\"\"YUMMMYMM   \"\"`  \"YMmMY\"   \"YMmMY\" ");
        }
        void setKnight()
        {
            Knight.Add(" :::  .   :::.    :::.:::  .,-:::::/    ::   .: ::::::::::::");
            Knight.Add(" ;;; .;;,.`;;;;,  `;;;;;;,;;-'````'    ,;;   ;;,;;;;;;;;''''");
            Knight.Add(" [[[[[/'    [[[[[. '[[[[[[[[   [[[[[[/,[[[,,,[[[     [[     ");
            Knight.Add("_$$$$,      $$$ \"Y$c$$$$$\"$$c.    \"$$ \"$$$\"\"\"$$$     $$     ");
            Knight.Add("\"888\"88o,   888    Y88888 `Y8bo,,,o88o 888   \"88o    88, ");
            Knight.Add(" MMM \"MMP\"  MMM     YMMMM   `'YMUP\"YMM MMM    YMM MMM    ");
        }
        void setWizard()
        {
            Wizard.Add(".::    .   .::::::::::::::: :::.    :::::::.. :::::::-.  ");
            Wizard.Add("';;,  ;;  ;;;' ;;;'`````;;; ;;`;;   ;;;;``;;;; ;;,   `';,");
            Wizard.Add("'[[, [[, [['  [[[    .n[[',[[ '[[,  [[[,/[[[' `[[     [[");
            Wizard.Add("  Y$c$$$c$P   $$$  ,$$P\" c$$$cc$$$c $$$$$$c    $$,    $$");
            Wizard.Add("   \"88\"888    888,888bo,_ 888   888,888b \"88bo, 888_, o8P'");
            Wizard.Add("    \"M \"M\"    MMM `\"\" * UMM YMM   \"\"` MMMM   \"W\" MMMMP\"`   ");
        }
    }

    /*

     */
    /*

    */
}
