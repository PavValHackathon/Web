using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.v1.Commands.Wallets;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.API.v1.Contracts.Requests;
using PavValHackathon.Web.API.v1.Queries.Wallets;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Common.Cqrs.Queries;
using PavValHackathon.Web.Common.Mapping;
using Swashbuckle.AspNetCore.Annotations;
using Void = PavValHackathon.Web.Common.Void;

namespace PavValHackathon.Web.API.v1.Controllers
{
    [ApiController]
    [Route("wallet")]
    public class WalletController : ControllerBase
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMapper _mapper;

        public WalletController(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IMapper mapper)
        {
            _commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(WalletDocument))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (id <= 0)
                return BadRequest(Result.Failed((int) HttpStatusCode.BadRequest, "Id can not be less that 1."));

            var query = new GetWalletQuery(id);
            var result = await _queryExecutor.ExecuteAsync<GetWalletQuery, WalletDocument>(query);

            return Ok(result);
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(PaginationCollection<WalletDocument>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        public async Task<IActionResult> ListAsync(
            [FromQuery] int top = 100,
            [FromQuery] int skip = 0)
        {
            var query = new ListWalletQuery
            {
                Top = top,
                Skip = skip
            };
            var result = await _queryExecutor.ExecuteAsync<ListWalletQuery, PaginationCollection<WalletDocument>>(query);

            return Ok(result);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(WalletDocument))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateWalletRequestDocument document)
        {
            var command = _mapper.MapFrom<CreateWalletRequestDocument, CreateWalletCommand>(document);
            var result = await _commandExecutor.ExecuteAsync<CreateWalletCommand, int>(command);
            
            if (result.IsFailed)
                return BadRequest(result);

            var query = new GetWalletQuery(result.Value);
            var queryResult = await _queryExecutor.ExecuteAsync<GetWalletQuery, WalletDocument>(query);

            if (queryResult.IsFailed)
                return BadRequest(result);

            // ReSharper disable once Mvc.ActionNotResolved
            return CreatedAtAction(nameof(GetAsync), new { id = queryResult.Value!.Id }, queryResult);
        }

        [HttpPatch("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        public async Task<IActionResult> EditAsync([FromRoute] int id, [FromBody] EditWalletRequestDocument document)
        {
            if (id <= 0)
                return BadRequest(Result.Failed((int) HttpStatusCode.BadRequest, "Id can not be less that 1."));

            var command = new EditWalletCommand(id);
            _mapper.MapFrom(command, document);
            var commandResult = await _commandExecutor.ExecuteAsync<EditWalletCommand, Void>(command);

            if (commandResult.IsFailed)
                return BadRequest(commandResult);
            
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest(Result.Failed((int)HttpStatusCode.BadGateway, "Id can not be less that 1."));

            var command = new DeleteWalletCommand(id);
            var commandResult = await _commandExecutor.ExecuteAsync<DeleteWalletCommand, Void>(command);

            if (commandResult.IsFailed)
                return BadRequest(commandResult);

            return NoContent();
        }
    }
}