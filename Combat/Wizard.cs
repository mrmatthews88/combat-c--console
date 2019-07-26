using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combat;
namespace People
{
    class Wizard : Person
    {
        public Wizard(string name) : base(name) { }
        public Wizard() : base() { }
        public override void basicAttacks()
        {
            Actions.Add("Slap", Person.ActionList["Slap"]);
            Actions.Add("Flare", Person.ActionList["Flare"]);
            Actions.Add("Fireball", Person.ActionList["Fireball"]);
            Actions.Add("Defend", Person.ActionList["Defend"]);
        }
    }
}
