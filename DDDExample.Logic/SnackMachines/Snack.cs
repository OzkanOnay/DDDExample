using System;
using DDDLogic;

namespace DDDExample.Logic
{
    public class Snack : Entity
    {
        public virtual string Name { get; private set; }

        protected Snack() {
        }

        public Snack(string name) : this()
        {
            Name = name;
        }
    }
}
