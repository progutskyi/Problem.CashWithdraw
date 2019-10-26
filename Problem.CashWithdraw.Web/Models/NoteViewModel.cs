using Problem.CashWithdraw.Domain;

namespace Problem.CashWithdraw.Web.Models
{
    public class NoteViewModel
    {
        public int Value { get; set; }

        public static NoteViewModel FromNote(Note note)
        {
            return new NoteViewModel { Value = note.Value };
        }
    }
}
