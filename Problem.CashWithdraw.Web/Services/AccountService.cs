using Problem.CashWithdraw.Domain;
using Problem.CashWithdraw.Web.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem.CashWithdraw.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly List<Note> availableNotes = new List<Note>();

        public AccountService()
        {
            this.availableNotes = new List<Note>
            {
                new Note { Value = 100 },
                new Note { Value = 50 },
                new Note { Value = 20 },
                new Note { Value = 10 },
            };
        }
        public IEnumerable<Note> Withdraw(int? amount)
        {
            if (!amount.HasValue)
            {
                return Enumerable.Empty<Note>();
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Amount should be greated than zero");
            }

            return this.WithdrawNotes(amount);
        }

        private IEnumerable<Note> WithdrawNotes(int? amount)
        {
            var returnNotes = new List<Note>();

            int remainingAmount = amount.Value;
            int divisionRemainder = -1;
            Note note;

            while ((note = this.GetFirstNoteThatIsLessOrEqualToAmout(remainingAmount)) != null
                && divisionRemainder != 0)
            {
                var divisionResult = Math.DivRem(remainingAmount, note.Value, out divisionRemainder);

                for (int i = 0; i < divisionResult; i++)
                {
                    returnNotes.Add(note);
                }

                remainingAmount = divisionRemainder;
            }

            if (remainingAmount != 0)
            {
                throw new NoteUnavailableException(amount.Value, this.availableNotes);
            }

            return returnNotes;
        }

        private Note GetFirstNoteThatIsLessOrEqualToAmout(int amount) => 
            this.availableNotes.FirstOrDefault(n => n.Value <= amount);
    }
}
