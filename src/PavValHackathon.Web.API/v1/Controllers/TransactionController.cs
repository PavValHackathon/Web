using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PavValHackathon.Web.API.v1.Commands.Transactions;
using PavValHackathon.Web.API.v1.Contracts.Requests;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Common.Mapping;

namespace PavValHackathon.Web.API.v1.Controllers
{
    [ApiController]
    [Route("transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandExecutor _commandExecutor;

        public TransactionController(IMapper mapper, ICommandExecutor commandExecutor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTransactionRequestDocument document)
        {
            var command = _mapper.MapFrom<CreateTransactionRequestDocument, CreateTransactionCommand>(document);
            var commendResult = await _commandExecutor.ExecuteAsync<CreateTransactionCommand, int>(command);
            
            //TODO: add getTransactionQuery
            return Ok();
        }
    }
}