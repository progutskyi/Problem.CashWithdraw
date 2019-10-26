using Microsoft.VisualStudio.TestTools.UnitTesting;
using Problem.CashWithdraw.Web.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Problem.CashWithdraw.Web.Tests.Services
{
    [TestClass]
    public class AccountServiceTests
    {
        private readonly AccountService accountService = new AccountService();

        [TestMethod]
        public void ShouldWithdraw()
        {
            this.accountService.Withdraw(10);
        }
    }
}
