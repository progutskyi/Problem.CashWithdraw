using Problem.CashWithdraw.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Problem.CashWithdraw.Web.Extentions
{
    public static class NotesExtensions
    {
        public static string GetNotesValueString(this IEnumerable<Note> notes)
        {
            return string.Join(',', notes.Select(n => n.Value));
        }
    }
}
