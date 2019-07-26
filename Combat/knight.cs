using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combat;
namespace People
{
    class Knight : Person
    {
        public Knight(string name) : base(name) { }
        public Knight() : base() { }
        public override void basicAttacks()
        {
            Actions.Add("Slap", Person.ActionList["Slap"]);
            Actions.Add("Slash", Person.ActionList["Slash"]);
            Actions.Add("Thrust", Person.ActionList["Thrust"]);
            Actions.Add("Defend", Person.ActionList["Defend"]);
        }
    }
}
