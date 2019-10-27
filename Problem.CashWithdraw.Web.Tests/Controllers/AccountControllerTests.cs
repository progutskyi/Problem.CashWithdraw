using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Problem.CashWithdraw.Domain;
using Problem.CashWithdraw.Web.Controllers.Api;
using Problem.CashWithdraw.Web.Exceptions;
using Problem.CashWithdraw.Web.Models;
using Problem.CashWithdraw.Web.Services;
using Problem.CashWithdraw.Web.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using BadRequestResult = Microsoft.AspNetCore.Mvc.BadRequestResult;

namespace Problem.CashWithdraw.Web.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> accountService = new Mock<IAccountService>();
        private readonly AccountController accountController;

        public AccountControllerTests()
        {
            this.accountController = new AccountController(this.accountService.Object);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenAccountServiceThrowsArgumentException(int invalidArgument)
        {
            // Arrange
            const int InvalidArgument = -1;
            this.accountService.Setup(a => a.Withdraw(InvalidArgument))
                .Throws(new ArgumentException());

            // Act
            var actualResult = this.accountController.WithdrawMoney(invalidArgument) as BadRequestResult;

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(400, actualResult.StatusCode);

            var error = this.accountController.ModelState.GetValueOrDefault("Value");
            Assert.AreEqual("Withdraw amount should be greater than zero", error.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenAccountServiceThrowsNoteUnavailableException()
        {
            // Arrange
            const int NoteUnavailableAmount = 135;
            this.accountService.Setup(a => a.Withdraw(NoteUnavailableAmount))
                .Throws(new NoteUnavailableException(NoteUnavailableAmount, Enumerable.Empty<Note>()));

            // Act
            var actualResult = this.accountController.WithdrawMoney(NoteUnavailableAmount) as BadRequestResult;

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(400, actualResult.StatusCode);

            var error = this.accountController.ModelState.GetValueOrDefault("Value");
            Assert.AreEqual($"Don't have notes to withdraw amount {NoteUnavailableAmount}. Available notes are: ", error.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void ShouldReturnViewWithNotesWhenAccountServiceReturnsNoteCollection()
        {
            // Arrange
            const int Amount = 80;
            var withdrawNotes = new NoteCollectionBuilder()
                .AddNoteWithValue(50)
                .AddNoteWithValue(20)
                .AddNoteWithValue(10)
                .Build();

            this.accountService.Setup(a => a.Withdraw(Amount)).Returns(withdrawNotes);

            // Act
            var actualResult = this.accountController.WithdrawMoney(Amount) as OkNegotiatedContentResult<IEnumerable<NoteViewModel>>;

            // Assert
            CollectionAssert.AreEqual(actualResult.Content.ToList(), withdrawNotes.Select(NoteViewModel.FromNote).ToList());
        }
    }
}
