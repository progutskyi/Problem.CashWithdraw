using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult WithdrawMoney(int amount)
        {
            throw new NotImplementedException();
        }
    }
}