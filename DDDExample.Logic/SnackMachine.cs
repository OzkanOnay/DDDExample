using System;
using System.Collections.Generic;
using System.Linq;
using DDDExample.Logic;

namespace DDDLogic
{
    public class SnackMachine : AggregateRoot
    {
        public virtual Money MoneyInside { get; private set; }
        public virtual decimal MoneyInTransaction { get; private set; }
        protected virtual ICollection<Slot> Slots { get; private set; }



        public SnackMachine()
        {
            MoneyInside = Money.None;
            MoneyInTransaction = 0m;

            Slots = new List<Slot>
            {
                new Slot(this, 1),
                new Slot(this, 2),
                new Slot(this, 3)
            };
        }

        public virtual void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = { Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar };
            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money.Amount;
            MoneyInside += money;
        }

        public virtual void ReturnMoney()
        {
            Money moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
            MoneyInside -= moneyToReturn;
            MoneyInTransaction = 0m;
        }

        public virtual void BuySnack(int position)
        {
            Slot slot = GetSlot(position);
            if (slot.SnackPile.Price > MoneyInTransaction)
                throw new InvalidOperationException();


            slot.SnackPile = slot.SnackPile.SubstractOne();

            Money change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);

            if (change.Amount < MoneyInTransaction - slot.SnackPile.Price)
                throw new InvalidOperationException();

            MoneyInside -= change;
            MoneyInTransaction = 0m;
        }

        public virtual void LoadSnacks(int position, SnackPile snackPile)
        {
            Slot slot = GetSlot(position);
            slot.SnackPile = snackPile;
        }

        private Slot GetSlot(int position)
        {
            return Slots.Single(x => x.Position == position);
        }

        public virtual SnackPile GetSnackPileInSlot(int position)
        {
            return GetSlot(position).SnackPile;
        }

        public virtual void LoadMoney(Money money)
        {
            MoneyInside += money;
        }
    }
}