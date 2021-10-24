using System;
using DDDLogic;

namespace DDDExample.Logic
{
    public class Slot : AggregateRoot
    {
        public virtual SnackPile SnackPile { get; set; }
        public virtual SnackMachine SnackMachine { get; private set; }
        public virtual int Position { get; private set; }

        public Slot()
        {
        }

        public Slot(SnackMachine snackMachine, int position)
            : this()
        {
            SnackMachine = snackMachine;            
            Position = position;
            SnackPile = new SnackPile(0, 0m, null);
        }
    }
}
