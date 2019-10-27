using Problem.CashWithdraw.Domain;
using System.Collections.Generic;

namespace Problem.CashWithdraw.Web.Tests.Builders
{
    public class NoteCollectionBuilder
    {
        private readonly List<Note> notes = new List<Note>();

        public NoteCollectionBuilder AddNoteWithValue(int value)
        {
            this.notes.Add(new Note { Value = value });
            return this;
        }

        public IEnumerable<Note> Build()
        {
            return this.notes;
        }
    }
}
