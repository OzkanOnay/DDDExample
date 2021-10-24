using System;
using Xunit;
using DDDLogic;
using FluentAssertions;
using DDDExample.Logic;
using System.Linq;
using DDDExample.Logic.ATM;

namespace DDDExample.Test
{
    public class AtmSpecs
    {
        [Fact]
        public void Take_money_exchanges_money_with_commision()
        {
            var atm = new Atm();
            atm.LoadMoney(Money.Dollar);

            atm.TakeMoney(1m);

            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(1.01m);
        }

        [Fact]
        public void Commision_is_at_least_one_cent()
        {
            var atm = new Atm();
            atm.LoadMoney(Money.Cent);

            atm.TakeMoney(0.01m);

            atm.MoneyCharged.Should().Be(0.02m);
        }

        [Fact]
        public void Commision_is_rounded_up_to_next_cent()
        {
            var atm = new Atm();
            atm.LoadMoney(Money.Dollar + Money.TenCent);

            atm.TakeMoney(1.1m);

            atm.MoneyCharged.Should().Be(1.12m);
        }
    }
}
