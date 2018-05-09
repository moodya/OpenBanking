using System;
using System.Threading.Tasks;
using System.Web.Http;
using Banking.Api.Logging;
using Banking.Api.Requests;
using Banking.Contract;
using MediatR;
using Seterlund.CodeGuard;
using ValidationException = Banking.Api.Validation.ValidationException;

namespace Banking.Api.Controllers
{
    public class BankingController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public BankingController(
            IMediator mediator,
            ILogger logger)
        {
            Guard.That(() => mediator).IsNotNull();
            Guard.That(() => logger).IsNotNull();

            _mediator = mediator;
            _logger = logger;
        }

        [Route("banking/users")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUsersAsync()
        {
            try
            {
                var users = await _mediator.SendAsync(new UserRequest());
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.Log(e, "Failed to get all users");
                return InternalServerError();
            }
        }

        [Route("banking/user")]
        [HttpPost]
        public async Task<IHttpActionResult> AddAccountAsync([FromBody] Account account)
        {
            try
            {
                var users = await _mediator.SendAsync(new AddAccountRequest(account));
                return Ok(users);
            }
            catch (ValidationException e)
            {
                _logger.Log(e, "User bank account data failed validation");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.Log(e, "Failed to add user bank account");
                return InternalServerError();
            }
        }

        [Authorize]
        [Route("banking/bank/{bankName}/user/{accountNumber}/transactions")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAccountTransactionsAsync([FromUri] string bankName, [FromUri] string accountNumber)
        {
            try
            {
                var accountTransactions = await _mediator.SendAsync(new AccountTransactionsRequest(bankName, accountNumber));
                return Ok(accountTransactions);
            }
            catch (ValidationException e)
            {
                _logger.Log(e, "User bank account data failed validation");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.Log(e, "Failed to get account transactions");
                return InternalServerError();
            }
        }
    }
}
