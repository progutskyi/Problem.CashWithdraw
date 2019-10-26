using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Problem.CashWithdraw.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPut]
        public IActionResult WithdrawMoney(int amount)
        {
            throw new NotImplementedException();
        }
    }
}