using System;
using System.Collections.Generic;
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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Snack;
            yield return Quantity;
            yield return Price;
        }
    }
}
