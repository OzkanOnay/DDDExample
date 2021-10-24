using System;
using System.Collections.Generic;

namespace DDDLogic
{
    public class Money : ValueObject<Money>
    {
        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);

        public int OneCentCount { get; }
        public int TenCentCount { get;  }
        public int QuarterCount { get;  }
        public int OneDollarCount { get;  }
        public int FiveDollarCount { get;  }
        public int TwentyDollarCount { get; }

        public decimal Amount =>
            OneCentCount * 0.01m +
                    TenCentCount * 0.10m +
                    QuarterCount * 0.25m +
                    OneDollarCount +
                    FiveDollarCount * 5 +
                    TwentyDollarCount * 20;

        internal Money Allocate(decimal amount)
        {
            int twentyDollarCount = Math.Min((int)(amount / 20), TwentyDollarCount);
            amount = amount - twentyDollarCount * 20;

            int fiveDollarCount = Math.Min((int)(amount / 5), FiveDollarCount);
            amount = amount - fiveDollarCount * 5;

            int oneDollarCount = Math.Min((int)(amount), OneDollarCount);
            amount = amount - oneDollarCount;

            int quarterCount = Math.Min((int)(amount / 0.25m), QuarterCount);
            amount = amount - quarterCount * 0.25m;

            int tenCentCount = Math.Min((int)(amount / 0.1m), TenCentCount);
            amount = amount - tenCentCount * 0.1m;

            int oneCentCount = Math.Min((int)(amount / 0.01m), OneCentCount);
            amount = amount - oneCentCount * 0.01m;

            return new Money(oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);
        }

        public Money(
            int oneCentCount,
            int tenCentCount,
            int quarterCentCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount)
        {
            if (oneCentCount < 0)
                throw new InvalidOperationException();
            if (tenCentCount < 0)
                throw new InvalidOperationException();
            if (quarterCentCount < 0)
                throw new InvalidOperationException();
            if (oneDollarCount < 0)
                throw new InvalidOperationException();
            if (fiveDollarCount < 0)
                throw new InvalidOperationException();
            if (twentyDollarCount < 0)
                throw new InvalidOperationException();

            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCentCount;            
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new Money(
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount);

            return sum;
        }

        public static Money operator -(Money money1, Money money2)
        {

            Money substract = new Money(
                money1.OneCentCount - money2.OneCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.QuarterCount - money2.QuarterCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount,
                money1.TwentyDollarCount - money2.TwentyDollarCount);
            return substract;
        }

        public static Money operator *(Money money1, int multiplier)
        {
            Money sum = new Money(
                money1.OneCentCount * multiplier,
                money1.TenCentCount * multiplier,
                money1.QuarterCount * multiplier,
                money1.OneDollarCount * multiplier,
                money1.FiveDollarCount * multiplier,
                money1.TwentyDollarCount * multiplier);

            return sum;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return OneCentCount;
            yield return TenCentCount;
            yield return QuarterCount;
            yield return OneDollarCount;
            yield return FiveDollarCount;
            yield return TwentyDollarCount;
        }

    }
}
