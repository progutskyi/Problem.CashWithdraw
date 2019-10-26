using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Problem.CashWithdraw.Web.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Problem.CashWithdraw.Web.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private readonly AccountController accountController = new AccountController();

        [TestMethod]
        public void ShouldReturnErrorForInvalidArgumentException()
        {
            // Arrange

            // Act
            var actualResult = this.accountController.WithdrawMoney(-5) as BadRequestResult;

            // Assert
            Assert.AreEqual(400, actualResult.StatusCode);
        }
    }
}
