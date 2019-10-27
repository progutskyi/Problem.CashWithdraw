using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Problem.CashWithdraw.Domain;
using Problem.CashWithdraw.Web.Controllers.Api;
using Problem.CashWithdraw.Web.Exceptions;
using Problem.CashWithdraw.Web.Models;
using Problem.CashWithdraw.Web.Services;
using Problem.CashWithdraw.Web.Tests.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        public void ShouldReturnBadRequestWhenAccountServiceThrowsArgumentException()
        {
            // Arrange
            const int InvalidAmount = -1;
            this.accountService.Setup(a => a.Withdraw(InvalidAmount))
                .Throws(new ArgumentException());

            // Act
            var actualResult = this.accountController.WithdrawMoney(InvalidAmount) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(actualResult);

            var response = actualResult.Value as ErrorModel;
            Assert.AreEqual(400, actualResult.StatusCode);
            Assert.AreEqual($"Withdraw amount {InvalidAmount} should be greater than zero", response.Error);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenAccountServiceThrowsNoteUnavailableException()
        {
            // Arrange
            const int NoteUnavailableAmount = 135;
            this.accountService.Setup(a => a.Withdraw(NoteUnavailableAmount))
                .Throws(new NoteUnavailableException(NoteUnavailableAmount, Enumerable.Empty<Note>()));

            // Act
            var actualResult = this.accountController.WithdrawMoney(NoteUnavailableAmount) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(actualResult);

            var response = actualResult.Value as ErrorModel;
            Assert.AreEqual(400, actualResult.StatusCode);
            Assert.AreEqual($"Don't have notes to withdraw amount {NoteUnavailableAmount}. Available notes are: ", response.Error); 
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
            var actualResult = this.accountController.WithdrawMoney(Amount) as OkObjectResult;
            var notesViewModelCollection = actualResult.Value as IEnumerable<NoteViewModel>;

            // Assert
            Assert.AreEqual(200, actualResult.StatusCode);
            Assert.IsNotNull(notesViewModelCollection);
            CollectionAssert.AreEqual(notesViewModelCollection.Select(n => n.Value).ToList(), withdrawNotes.Select(w => w.Value).ToList());
        }
    }
}
