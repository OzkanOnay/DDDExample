using System;
using DDDLogic;

namespace DDDExample.Logic
{
    public sealed class SnackPile : ValueObject<SnackPile>
    {
        public  Snack Snack { get; }
        public  int Quantity { get; }
        public  decimal Price { get; }

        public SnackPile()
        {
        }

        public SnackPile(int quantity, decimal price, Snack snack) : this()
        {
            if (quantity < 0)
                throw new InvalidOperationException();
            if (price < 0)
                throw new InvalidOperationException();
            if(price % 0.01m < 0)
                throw new InvalidOperationException();

            Quantity = quantity;
            Price = price;
            Snack = snack;
        }

        public SnackPile SubstractOne()
        {
            return new SnackPile(Quantity - 1, Price, Snack);
        }

        protected override bool EqualsCore(SnackPile other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
