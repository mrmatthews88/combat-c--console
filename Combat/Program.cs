using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using People;

namespace Combat
{


    class Program
    {
        #region Console Sizing
        [DllImport("kernel32.dll", ExactSpelling = true)]

        private static extern IntPtr GetConsoleWindow();

        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int HIDE = 0;

        private const int MAXIMIZE = 3;

        private const int MINIMIZE = 6;

        private const int RESTORE = 9;
        #endregion 

        static Stats PlayerStats = new Stats(50, 1, 110, 6, true);
        static Stats NpcStats = new Stats(1, 7, 110, 6, true);

        static bool inBattle = false;
        public static ConsoleColor defaultColor = Console.ForegroundColor;
        public static void Clear()
        {


            //Console.ReadKey();
            //Console.Clear();
            // clearFrame(Console.WindowWidth - 2, Console.WindowHeight - 2);

            if (inBattle)
            {
                PlayerStats.Draw();
                NpcStats.Draw();
                Console.SetCursorPosition(1, 20);
            }



        }

        static void Maximise()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
        }
        public static void WriteLine(string _string)
        {
            Console.CursorLeft = 1;
            Console.Write(_string);
            Console.SetCursorPosition(1, Console.CursorTop + 1);
        }

        static void gameOver()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"  ______                                            ______                                 __ ");
            Console.WriteLine(@" /      \                                          /      \                               /  |");
            Console.WriteLine(@"/$$$$$$  |  ______   _____  ____    ______        /$$$$$$  | __     __  ______    ______  $$ |");
            Console.WriteLine(@"$$ | _$$/  /      \ /     \/    \  /      \       $$ |  $$ |/  \   /  |/      \  /      \ $$ |");
            Console.WriteLine(@"$$ |/    | $$$$$$  |$$$$$$ $$$$  |/$$$$$$  |      $$ |  $$ |$$  \ /$$//$$$$$$  |/$$$$$$  |$$ |");
            Console.WriteLine(@"$$ |$$$$ | /    $$ |$$ | $$ | $$ |$$    $$ |      $$ |  $$ | $$  /$$/ $$    $$ |$$ |  $$/ $$/ ");
            Console.WriteLine(@"$$ \__$$ |/$$$$$$$ |$$ | $$ | $$ |$$$$$$$$/       $$ \__$$ |  $$ $$/  $$$$$$$$/ $$ |       __ ");
            Console.WriteLine(@"$$    $$/ $$    $$ |$$ | $$ | $$ |$$       |      $$    $$/    $$$/   $$       |$$ |      /  |");
            Console.WriteLine(@" $$$$$$/   $$$$$$$/ $$/  $$/  $$/  $$$$$$$/        $$$$$$/      $/     $$$$$$$/ $$/       $$/ ");
        }

        static void win()
        {
            Console.WriteLine(@" __      __                         __       __  __            __ ");
            Console.WriteLine(@"/  \    /  |                       /  |  _  /  |/  |          /  |");
            Console.WriteLine(@"$$  \  /$$/______   __    __       $$ | / \ $$ |$$/  _______  $$ |");
            Console.WriteLine(@" $$  \/$$//      \ /  |  /  |      $$ |/$  \$$ |/  |/       \ $$ |");
            Console.WriteLine(@"  $$  $$//$$$$$$  |$$ |  $$ |      $$ /$$$  $$ |$$ |$$$$$$$  |$$ |");
            Console.WriteLine(@"   $$$$/ $$ |  $$ |$$ |  $$ |      $$ $$/$$ $$ |$$ |$$ |  $$ |$$/ ");
            Console.WriteLine(@"    $$ | $$ \__$$ |$$ \__$$ |      $$$$/  $$$$ |$$ |$$ |  $$ | __ ");
            Console.WriteLine(@"    $$ | $$    $$/ $$    $$/       $$$/    $$$ |$$ |$$ |  $$ |/  |");
            Console.WriteLine(@"    $$/   $$$$$$/   $$$$$$/        $$/      $$/ $$/ $$/   $$/ $$/ ");
        }

        public static Person player = new Wizard("player");
        static Person npc = new Person("npc");
        static Display GameFrame;
        static void Main(string[] args)
        {
            Maximise();
            GameFrame = new Display(0, 0, Console.WindowWidth-1, Console.WindowHeight-1, true);
            GameFrame.topBar = true;
            GameFrame.borderColor = ConsoleColor.DarkYellow;
            GameFrame.refresh();

            MenuPlayerClass PlayerMenu = new MenuPlayerClass(1,4,100,100);
            player = PlayerMenu.Choose();
            int Wins = 0;
            
            while (player.health > 0)
            {
                GameFrame.Clear();
                if (Wins > 0)
                {
                    GameFrame.WriteTopBar($"Wins:: {Wins}");
                    player.Heal(10f);
                }
                Wins += new Battle().Start();
                
                player.multiplier += player.multiplier / 2;
                player.AP += 10;
                
            }


        }



    }
    
    class Action
    {
        public const int TYPE_ATTACK = 0;
        public const int TYPE_DEFENSE = 1;
        public Action(string name, float modifier, int type, int AP) { this.name = name; this.modifier = modifier; this.type = type; this.AP = AP; }
        public int type;
        public string name;
        public float modifier = 0f;
        public int AP = 1;
    }





}
