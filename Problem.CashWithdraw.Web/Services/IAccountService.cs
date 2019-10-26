using Problem.CashWithdraw.Domain;
using System.Collections.Generic;

namespace Problem.CashWithdraw.Web.Services
{
    public interface IAccountService
    {
        IEnumerable<Note> Withdraw(int amount);
    }
}