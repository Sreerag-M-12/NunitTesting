using System.Security.Principal;

namespace BanKAccountNunitTest
{
    public class Tests
    {
        BankAccount _bankAccount;
        BankAccount _toAccount;

        [SetUp]
        public void Setup()
        {
            _bankAccount = new BankAccount();
            _toAccount = new BankAccount();
        }

        [TestCase(1000, 4000)]
        public void ValidDeposit(double amount, double expectedResult)
        {
            _bankAccount.Deposit(amount);
            double actualResult = _bankAccount.GetBalance();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(-1000, "Deposit amount must be positive")]
        public void InValidDeposit(double amount, string expectedResult)
        {
            var ex = Assert.Throws<ArgumentException>(() => _bankAccount.Deposit(amount));
            Assert.AreEqual(expectedResult, ex.Message);
        }

        [TestCase(1000, 2000)]
        [TestCase(3000, 0)]
        public void ValidWithdraw(double amount, double expectedResult)
        {
            _bankAccount.Withdraw(amount);
            double actualResult = _bankAccount.GetBalance();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(-1000, "WithDraw amount must be positive")]
        public void InValidWithdrawNegativeAmount(double amount, string expectedResult)
        {
            var ex = Assert.Throws<ArgumentException>(() => _bankAccount.Withdraw(amount));
            Assert.AreEqual(expectedResult, ex.Message);
        }


        [TestCase(4000, "Insufficient funds")]
        public void InValidWithdrawInsufficientFunds(double amount, string expectedResult)
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _bankAccount.Withdraw(amount));
            Assert.AreEqual(expectedResult, ex.Message);
        }


        [TestCase(1000, 2000, 4000)]
        public void ValidTransfer(double amount, double expectedBalanceFrom, double expectedBalanceTo)
        {
            _bankAccount.Transfer(_toAccount, amount);
            double actualBalanceFrom = _bankAccount.GetBalance();
            double actualBalanceTo = _toAccount.GetBalance();

            Assert.AreEqual(expectedBalanceFrom, actualBalanceFrom);
            Assert.AreEqual(expectedBalanceTo, actualBalanceTo);
        }


        [TestCase(1000, "Value cannot be null. (Parameter 'toAccount')")]
        public void InValidTransferNullToAccount(double amount, string expectedResult)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _bankAccount.Transfer(null, amount));
            Assert.AreEqual(expectedResult, ex.Message);
        }


    }
}