using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Problem.CashWithdraw.Web.Exceptions;
using Problem.CashWithdraw.Web.Models;
using Problem.CashWithdraw.Web.Services;

namespace Problem.CashWithdraw.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPut]
        [Route("withdraw")]
        public IActionResult WithdrawMoney(int amount)
        {
            try
            {
                var notes = this.accountService.Withdraw(amount);

                return this.Ok(notes.Select(NoteViewModel.FromNote));
            }
            catch (ArgumentException)
            {
                return this.BadRequest($"Withdraw amount {amount} should be greater than zero");
            }
            catch (NoteUnavailableException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return this.StatusCode((int)HttpStatusCode.InternalServerError, "Unknown error occured");
            }
        }
    }
}