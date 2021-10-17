using System;
using Xunit;
using DDDLogic;
using FluentAssertions;
using DDDExample.Logic;
using System.Linq;

namespace DDDExample.Test
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void Return_money_emties_money_in_trasaction()
        {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Money.Dollar);

            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Should().Be(0m);
        }

        [Fact]
        public void Inserted_money_goes_to_money_in_transaction()
        {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Money.Cent);
            snackMachine.InsertMoney(Money.Dollar);


            snackMachine.MoneyInTransaction.Should().Be(1.01m);
        }

        [Fact]
        public void Cannot_insert_more_than_one_coin_or_note_at_a_time()
        {
            SnackMachine snackMachine = new SnackMachine();
            var twoCent = Money.Cent + Money.Cent;

            Action action = () => snackMachine.InsertMoney(twoCent);

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void BuySnack_trades_inserted_money_for_a_snack()
        {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(10, 1m, new Snack("Snack 1")));

            snackMachine.InsertMoney(Money.Dollar);

            snackMachine.BuySnack(1);

            snackMachine.MoneyInTransaction.Should().Be(0m);
            snackMachine.MoneyInside.Amount.Should().Be(1m);
            snackMachine.GetSnackPileInSlot(1).Quantity.Should<int>().Be(9);

        }

        [Fact]
        public void Cannot_make_purchase_when_there_is_no_snack()
        {
            SnackMachine snackMachine = new SnackMachine();
            
            Action action = () => snackMachine.BuySnack(1);

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void Cannot_make_purchase_if_not_enough_money_inserted()
        {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(1, 2m, new Snack("Snack 1")));
            snackMachine.InsertMoney(Money.Dollar);

            Action action = () => snackMachine.BuySnack(1);

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void Snack_machine_returns_money_with_highest_denomination_first()
        {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.LoadMoney(Money.Dollar);

            snackMachine.InsertMoney(Money.Quarter);
            snackMachine.InsertMoney(Money.Quarter);
            snackMachine.InsertMoney(Money.Quarter);
            snackMachine.InsertMoney(Money.Quarter);
            snackMachine.ReturnMoney();

            snackMachine.MoneyInside.QuarterCount.Should<int>().Be(4);
            snackMachine.MoneyInside.OneDollarCount.Should<int>().Be(0);
        }

        [Fact]
        public void After_purchase_change_is_returned()
        {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(1, 0.5m, new Snack("Snack 1")));
            snackMachine.LoadMoney(Money.TenCent * 10);

            snackMachine.InsertMoney(Money.Dollar);

            snackMachine.BuySnack(1);

            snackMachine.MoneyInside.Amount.Should().Be(1.5m);
            snackMachine.MoneyInTransaction.Should().Be(0m);

        }

        [Fact]
        public void Cannot_buy_snack_if_not_enough_change()
        {
            SnackMachine snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(1, 0.5m, new Snack("Snack 1")));

            snackMachine.InsertMoney(Money.Dollar);

            Action action = () => snackMachine.BuySnack(1);

            Assert.Throws<InvalidOperationException>(action);
        }
    }
}
