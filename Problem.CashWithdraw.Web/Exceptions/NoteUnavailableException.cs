using Problem.CashWithdraw.Domain;
using Problem.CashWithdraw.Web.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Problem.CashWithdraw.Web.Exceptions
{
    public class NoteUnavailableException : Exception
    {
        public NoteUnavailableException(int amount, IEnumerable<Note> availableNotes)
            : base($"Dont have notes to withdraw amount {amount}. Available notes are: {availableNotes.GetNotesValueString()}")
        {
        }
    }
}
