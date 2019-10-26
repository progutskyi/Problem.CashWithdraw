namespace Problem.CashWithdraw.Web.Services
{
    public interface IAccountService
    {
        int[] Withdraw(int amount);
    }
}