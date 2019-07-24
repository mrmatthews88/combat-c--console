using System;
using System.Collections.Generic;
using System.Linq;

namespace Combat
{


    class Program
    {

        static bool inBattle = false;
        public static ConsoleColor defaultColor = Console.ForegroundColor;
        public static void Clear()
        {


            Console.Clear();

            if (inBattle)
            {
                drawStats("YOU", player);
                Console.WriteLine("");
                Console.WriteLine("");
                drawStats("BAD GUY", npc);
            }



        }

        static void drawStats(string label, Person person)
        {
            //Draw Stats
            Console.ForegroundColor = defaultColor;
            Console.WriteLine($"-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine($"          {label}             ");
            //Console.WriteLine($"|  Health:{player.health} AP: {player.AP}");
            Console.Write("HP::");
            Console.ForegroundColor = System.ConsoleColor.Red;
            for (int i = 0; i < 100; i++)
            {
                if (i < (int)person.healthPercent())
                {
                    Console.Write("\u2588");
                }
                else
                {
                    Console.Write("_");
                }
            }
            Console.ForegroundColor = defaultColor;
            Console.Write("\nAP::");
            Console.ForegroundColor = System.ConsoleColor.Green;
            for (int i = 0; i < 100; i++)
            {
                if (i < (int)person.APPercent())
                {
                    Console.Write("\u2588");
                }
                else
                {
                    Console.Write("_");
                }
            }
            Console.ForegroundColor = defaultColor;
            Console.WriteLine($"\n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
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

        static Person player = new Wizard("player");
        static Person npc = new Person("npc");
        static void Main(string[] args)
        {

            var CharacterSelect = new Menu();
            Console.Clear();
            Console.WriteLine("Choose your Class");
            CharacterSelect.Options.AddRange(new[] { "Mage", "knight" });

            if (CharacterSelect.show() == "Mage")
            {
                player = new Wizard("Player");
            }
            else
            {
                player = new Knight("Player");
            }
            inBattle = true;


            while (player.health > 0 && npc.health > 0)
            {
                Console.WriteLine($"{player.name}: {player.health.ToString()} [AP: {player.AP}]            {npc.name}: {npc.health.ToString()} [AP: {npc.AP}]");

                player.selectAction();
                npc.AutoAction();
                Program.Clear();
                player.action(npc);
                npc.action(player);
                Console.ReadKey();
            }

            Program.Clear();
            if (player.health > 0)
            {
                win();
            }
            else
            {
                gameOver();
            }



            Console.ReadKey();

        }



    }

    class Menu
    {
        public List<string> Options = new List<string>();
        int position = 0;
        public string choice = "";

        public string show()
        {
            if (position < 0) position = Options.Count + position;
            position = position % Options.Count;
            Program.Clear();
            for (int i = 0; i < Options.Count; i++)
            {
                if (position == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Options[i]} <<<");
                }
                else
                {
                    Console.ForegroundColor = Program.defaultColor;
                    Console.WriteLine(Options[i]);
                }
            }

            var button = Console.ReadKey(true);

            if (button.Key == ConsoleKey.Enter) { choice = Options[position]; return choice; }
            if (button.Key == ConsoleKey.UpArrow) { position -= 1; }
            if (button.Key == ConsoleKey.DownArrow) { position += 1; }
            return show();

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

    class Person
    {

        public static Dictionary<string, Action> ActionList = new Dictionary<string, Action>(){
            {"Thump", new Action("Thump", 20f, Action.TYPE_ATTACK, 2)},
            {"Slap", new Action("Slap", 5f, Action.TYPE_ATTACK, 1)},
            {"Flare", new Action("Flare", 20f, Action.TYPE_ATTACK, 3)},
            {"Fireball", new Action("Fireball", 60f, Action.TYPE_ATTACK, 20)},
            {"Slash", new Action("Slash", 28f, Action.TYPE_ATTACK, 4)},
            {"Thrust", new Action("Thrust", 60f, Action.TYPE_ATTACK, 20)},
            {"Defend", new Action("Defend", 19f, Action.TYPE_DEFENSE, -2)}
        };
        public float healthPercent() => (health / maxHealth) * 100;
        public float APPercent() => ((float)AP / (float)maxAP) * (float)100;
        public float maxHealth = 20;
        public int maxAP = 20;
        public float health = 20;
        public float baseDamage = 2;
        public float defense = 5;
        public float multiplier = 1;
        public int AP = 10;
        public Action nextAction = null;
        public string name = "";
        public Dictionary<string, Action> Actions = new Dictionary<string, Action>();

        public Person(string name) { this.name = name; basicAttacks(); }

        public float getDefense()
        {
            return nextAction.type == Action.TYPE_ATTACK ? defense : defense + nextAction.modifier;
        }

        public virtual void basicAttacks()
        {
            Actions.Add("Slap", Person.ActionList["Slap"]);
            Actions.Add("Thump", Person.ActionList["Thump"]);
            Actions.Add("Defend", Person.ActionList["Defend"]);
        }

        public void selectAction()
        {

            Console.WriteLine("Please Select An Action");

            var Menu = new Menu();

            foreach (KeyValuePair<string, Action> item in Actions)
            {
                if (item.Value.AP <= AP) Menu.Options.Add($"{item.Key}");
            }
            nextAction = null;
            Program.Clear();
            Menu.show();

            nextAction = Actions[Menu.choice];
            if (!testAP())
            {
                Program.Clear();
                Console.WriteLine("Not Enough AP");
                Console.ReadKey();
                selectAction();
            }

        }

        public void AutoAction()
        {
            int random = (new Random()).Next(0, Actions.Count);
            nextAction = Actions[Actions.Keys.ToList()[random]];
            if (!testAP())
            {
                AutoAction();
            }
        }

        public bool testAP()
        {
            return (nextAction.AP <= AP);
        }

        public void action(Person Opponent)
        {
            if (nextAction.type == Action.TYPE_ATTACK)
            {
                float random = (float)(new Random()).Next(85, 100) / 100f;
                float damage = (nextAction.modifier / Opponent.getDefense()) * baseDamage * multiplier * random;
                Opponent.health -= damage;

                Console.WriteLine($"{name} hits {Opponent.name} with {nextAction.name} for {damage} damage");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"{name} Defends");
                Console.WriteLine();
            }
            AP -= nextAction.AP;
            if (AP < 0) AP = 0;
            if (AP > maxAP) AP = maxAP;

        }

    }

    class Wizard : Person
    {
        public Wizard(string name) : base(name) { }
        public override void basicAttacks()
        {
            Actions.Add("Slap", Person.ActionList["Slap"]);
            Actions.Add("Flare", Person.ActionList["Flare"]);
            Actions.Add("Fireball", Person.ActionList["Fireball"]);
            Actions.Add("Defend", Person.ActionList["Defend"]);
        }
    }


    class Knight : Person
    {
        public Knight(string name) : base(name) { }
        public override void basicAttacks()
        {
            Actions.Add("Slap", Person.ActionList["Slap"]);
            Actions.Add("Slash", Person.ActionList["Slash"]);
            Actions.Add("Thrust", Person.ActionList["Thrust"]);
            Actions.Add("Defend", Person.ActionList["Defend"]);
        }
    }

}
