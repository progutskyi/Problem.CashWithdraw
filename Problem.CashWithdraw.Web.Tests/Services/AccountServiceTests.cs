﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Problem.CashWithdraw.Web.Exceptions;
using Problem.CashWithdraw.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem.CashWithdraw.Web.Tests.Services
{
    [TestClass]
    public class AccountServiceTests
    {
        private readonly AccountService accountService = new AccountService();

        [TestMethod]
        public void ShouldReturnEmptySetForNull()
        {
            // Arrange
            int? NullAmount = null;

            // Act
            var actual = this.accountService.Withdraw(NullAmount);

            // Assert
            Assert.IsFalse(actual.Any());
        }

        [TestMethod]
        [DataRow(int.MinValue)]
        [DataRow(-1249)]
        [DataRow(0)]
        public void ShouldThrowInvalidArgumentException(int invalidArgument)
        {
            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => this.accountService.Withdraw(invalidArgument));

            // Assert
            Assert.AreEqual("Amount should be greated than zero", exception.Message);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(11)]
        [DataRow(22)]
        [DataRow(33)]
        [DataRow(44)]
        [DataRow(55)]
        [DataRow(66)]
        [DataRow(77)]
        [DataRow(88)]
        [DataRow(99)]
        [DataRow(157)]
        public void ShouldThrowNoteUnavailableExcetionWhenAmountCantBeReturnedInAvailableNotes(int amount)
        {
            // Act
            var exception = Assert.ThrowsException<NoteUnavailableException>(() => this.accountService.Withdraw(amount));

            // Assert
            Assert.AreEqual($"Don't have notes to withdraw amount {amount}. Available notes are: 100,50,20,10", exception.Message);
        }

        [TestMethod]
        [DataRow(10, new[] { 10 })]
        [DataRow(20, new[] { 20 })]
        [DataRow(30, new[] { 20, 10 })]
        [DataRow(40, new[] { 20, 20 })]
        [DataRow(50, new[] { 50 })]
        [DataRow(60, new[] { 50, 10 })]
        [DataRow(70, new[] { 50, 20 })]
        [DataRow(80, new[] { 50, 20, 10 })]
        [DataRow(90, new[] { 50, 20, 20 })]
        [DataRow(100, new[] { 100 })]
        [DataRow(160, new[] { 100, 50, 10 })]
        [DataRow(190, new [] { 100, 50, 20, 20 })]
        [DataRow(200, new[] { 100, 100 })]
        [DataRow(280, new[] { 100, 100, 50, 20, 10 })]
        [DataRow(490, new[] { 100, 100, 100, 100, 50, 20, 20 })]
        public void ShouldReturnMinimumNotesForAmoutThatBrokesDownIntoNotes(int amount, IEnumerable<int> expectedNotes)
        {
            // Act
            var notes = this.accountService.Withdraw(amount);

            // Assert
            CollectionAssert.AreEqual(expectedNotes.ToList(), notes.Select(v => v.Value).ToList());
        }
    }
}
 