using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combat;
using NameGenerator;

namespace People
{
    class Person
    {


        public static Dictionary<string, Combat.Action> ActionList = new Dictionary<string, Combat.Action>(){
            {"Thump", new Combat.Action("Thump", 20f, Combat.Action.TYPE_ATTACK, 2)},
            {"Slap", new Combat.Action("Slap", 5f, Combat.Action.TYPE_ATTACK, 1)},
            {"Flare", new Combat.Action("Flare", 20f, Combat.Action.TYPE_ATTACK, 3)},
            {"Fireball", new Combat.Action("Fireball", 60f, Combat.Action.TYPE_ATTACK, 20)},
            {"Slash", new Combat.Action("Slash", 28f, Combat.Action.TYPE_ATTACK, 4)},
            {"Thrust", new Combat.Action("Thrust", 60f, Combat.Action.TYPE_ATTACK, 20)},
            {"Defend", new Combat.Action("Defend", 19f, Combat.Action.TYPE_DEFENSE, -2)}
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
        public Combat.Action nextAction = null;
        public string name = "";
        public Dictionary<string, Combat.Action> Actions = new Dictionary<string, Combat.Action>();

        public Person(string name) { this.name = name; basicAttacks(); }
        public Person() { this.name = (Names.RandomRace()).RandomGenerate(); basicAttacks(); }

        public float getDefense()
        {
            return nextAction.type == Combat.Action.TYPE_ATTACK ? defense : defense + nextAction.modifier;
        }

        public virtual void basicAttacks()
        {
            Actions.Add("Slap", Person.ActionList["Slap"]);
            Actions.Add("Thump", Person.ActionList["Thump"]);
            Actions.Add("Defend", Person.ActionList["Defend"]);
        }


        public void AutoAction()
        {
            int random = Program.random.Next(0, Actions.Count);
            try
            {
                string action = Actions.Keys.ToList()[random];
                nextAction = Actions[action];
            }catch
            {
                nextAction = new Combat.Action("Did Bugger All for some reason?", 19f, Combat.Action.TYPE_DEFENSE, 0);
            }
            if (!testAP())
            {
                AutoAction();
            }
        }

        public bool testAP()
        {
            return (nextAction.AP <= AP);
        }

        public void Heal(float hp)
        {
            health += hp;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public void action(Person Opponent)
        {
            if (nextAction.type == Combat.Action.TYPE_ATTACK)
            {
                float random = (float)Program.random.Next(85, 100) / 100f;
                float damage = (nextAction.modifier / Opponent.getDefense()) * baseDamage * multiplier * random;
                Opponent.health -= damage;

                //Console.WriteLine($"{name} hits {Opponent.name} with {nextAction.name} for {damage} damage");
                //Console.WriteLine();
            }
            else
            {
                //Console.WriteLine($"{name} Defends");
                //Console.WriteLine();
            }
            AP -= nextAction.AP;
            if (AP < 0) AP = 0;
            if (AP > maxAP) AP = maxAP;

        }

    }

}
namespace NameGenerator
{
    class Names
    {
        public static Names RandomRace()
        {
            List<Names> Races = new List<Names>();
            Races.Add(new Human());
            if (Races.Count > 1) return Races[Program.random.Next(0, Races.Count - 1)];
            return Races[0];
        }
        
        public bool Gender;
        protected string[] FirstNamesMale;
        protected string[] FirstNamesFemale;
        protected string[] Surnames;
        public void RandomGender()
        {
            Gender = Program.random.Next(0, 1) == 0 ? false : true;
        }
        public string RandomName()
        {
            string[] forenames = Gender ? FirstNamesMale : FirstNamesFemale;
            string Forename = forenames[Program.random.Next(0, forenames.Length - 1)];
            string Surname = Surnames[Program.random.Next(0, Surnames.Length - 1)];
            return $"{Forename} {Surname}";
        }
        public string RandomGenerate()
        {
            RandomGender();
            return RandomName();
        }

    }

    class Human : Names
    {
        public Human()
        {
            FirstNamesMale = new[] {
                "Anlow", "Arando", "Bram", "Cale", "Dalkon", "Daylen", "Dodd", "Dungarth", "Dyrk", "Eandro", "Falken", "Feck", "Fenton", "Gryphero", "Hagar", "Jeras", "Krynt", "Lavant", "Leyten", "Madian", "Malfier", "Markus", "Meklan", "Namen", "Navaren", "Nerle", "Nilus", "Ningyan", "Norris", "Quentin", "Semil", "Sevenson", "Steveren", "Talfen", "Tamond", "Taran", "Tavon", "Tegan", "Vanan", "Vincent"
            };
            FirstNamesFemale = new[] {
                "Azura", "Brey", "Hallan", "Kasaki", "Lorelei", "Mirabel", "Pharana", "Remora", "Rosalyn", "Sachil", "Saidi", "Tanika", "Tura", "Tylsa", "Vencia", "Xandrilla"
            };
            Surnames = new[] {
                "Arkalis", "Armanci", "Bilger", "Blackstrand", "Brightwater", "Carnavon", "Caskajaro", "Coldshore", "Coyle", "Cresthill", "Cuttlescar", "Daargen", "Dalicarlia", "Danamark", "Donoghan", "Drumwind", "Dunhall", "Ereghast", "Falck", "Fallenbridge", "Faringray", "Fletcher", "Fryft", "Goldrudder", "Grantham", "Graylock", "Gullscream", "Hindergrass", "Iscalon", "Kreel", "Kroft", "Lamoth", "Leerstrom", "Lynchfield", "Moonridge", "Netheridge", "Oakenheart", "Pyncion", "Ratley", "Redraven", "Revenmar", "Roxley", "Sell", "Seratolva", "Shanks", "Shattermast", "Shaulfer", "Silvergraft", "Stavenger", "Stormchapel", "Strong", "Swiller", "Talandro ", "Targana", "Towerfall", "Umbermoor", "Van Devries", "Van Gandt", "Van Hyden", "Varcona", "Varzand", "Voortham", "Vrye", "Webb", "Welfer", "Wilxes", "Wintermere", "Wygarthe", "Zatchet", "Zethergyll"
            };
        }
    }
}
