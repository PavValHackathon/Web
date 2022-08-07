using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PavValHackathon.Web.API.v1.Commands.Transactions;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.API.v1.Contracts.Requests;
using PavValHackathon.Web.API.v1.Queries.Transactions;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Common.Cqrs.Queries;
using PavValHackathon.Web.Common.Mapping;
using Swashbuckle.AspNetCore.Annotations;

namespace PavValHackathon.Web.API.v1.Controllers
{
    [ApiController]
    [Route("transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQueryExecutor _queryExecutor;
        private readonly ICommandExecutor _commandExecutor;

        public TransactionController(IMapper mapper, ICommandExecutor commandExecutor, IQueryExecutor queryExecutor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
            _commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
        }

        [HttpGet("{id:int}")]
        [SwaggerResponse((int) HttpStatusCode.OK, type: typeof(TransactionDocument))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, type: typeof(ProblemDetails))]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var query = new GetTransactionQuery(id);
            var queryResult = await _queryExecutor.ExecuteAsync<GetTransactionQuery, TransactionDocument>(query);

            return Ok(queryResult);
        }

        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.Created, type: typeof(TransactionDocument))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, type: typeof(ProblemDetails))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTransactionRequestDocument document)
        {
            var command = _mapper.MapFrom<CreateTransactionRequestDocument, CreateTransactionCommand>(document);
            var commendResult = await _commandExecutor.ExecuteAsync<CreateTransactionCommand, int>(command);

            if (commendResult.IsFailed)
                return BadRequest(commendResult);

            var query = new GetTransactionQuery(commendResult.Value);
            var queryResult = await _queryExecutor.ExecuteAsync<GetTransactionQuery, TransactionDocument>(query);

            // ReSharper disable once Mvc.ActionNotResolved
            return CreatedAtAction(nameof(GetAsync), new {id = queryResult.Value!.Id}, queryResult);
        }
    }
}