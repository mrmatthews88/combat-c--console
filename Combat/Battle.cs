using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NameGenerator;
using People;
namespace Combat
{
    class Battle
    {
        Person player;
        Person npc;
        static Random random = new Random(DateTime.Now.Second);
        Stats NpcStats = new Stats(10, 15, 110, 8, true);
        Stats PlayerStats = new Stats(10, 4, 110, 8, true);
        public Battle setPerson(Person person) { player = person; return this; }
        public void setNpc(Person person) { npc = person; }
        public void setRandomNpc()
        {
            List<Person> people = new List<Person>();
            people.Add(new Wizard());
            people.Add(new Person());
            people.Add(new Knight());
            npc = people[random.Next(0, people.Count - 1)];
        }

        public int Start()
        {
            if (npc == null) setRandomNpc();
            if (player == null) player = Program.player;
            NpcStats.person = npc;
            PlayerStats.person = player;
            PlayerStats.refresh();
            NpcStats.refresh();
            while (player.health > 0 && npc.health > 0)
            {
                NpcStats.Draw();
                PlayerStats.Draw();
                SelectAction();
                npc.AutoAction();
                player.action(npc);
                npc.action(player);
            }

            BattleActions.Clear();
            BattleActions.WriteLine(player.health > 0 ? "You Won" : "You have perished.");
            BattleActions.WriteLine(player.health > 0 ? "You Won" : "You have perished.");
            BattleActions.WriteLine(player.health > 0 ? "You Won" : "You have perished.");
            BattleActions.WriteLine(player.health > 0 ? "You Won" : "You have perished.");
            BattleActions.WriteLine(player.health > 0 ? "You Won" : "You have perished.");
            BattleActions.WriteLine(player.health > 0 ? "You Won" : "You have perished.");
            BattleActions.WriteLine(player.health > 0 ? "You Won" : "You have perished.");
            Console.ReadKey(true);
            return player.health > 0 ? 1 : 0;
        }



        BattleMenu BattleActions = new BattleMenu();

        public void SelectAction()
        {
            BattleActions.Clear();

            player.nextAction = BattleActions.Choose();

            if (!player.testAP())
            {
                BattleActions.Clear();
                BattleActions.WriteLine("Not Enough AP");
                Console.ReadKey();
                SelectAction();
            }

        }

    }
}
