using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.v1.Commands.Buckets;
using PavValHackathon.Web.API.v1.Contracts;
using PavValHackathon.Web.API.v1.Contracts.Requests;
using PavValHackathon.Web.API.v1.Queries.Buckets;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Common.Cqrs.Queries;
using PavValHackathon.Web.Common.Mapping;
using Swashbuckle.AspNetCore.Annotations;
using Void = PavValHackathon.Web.Common.Void;

namespace PavValHackathon.Web.API.v1.Controllers
{
    [ApiController]
    [Route("bucket")]
    public class BucketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQueryExecutor _queryExecutor;
        private readonly ICommandExecutor _commandExecutor;

        public BucketController(IMapper mapper,
            IQueryExecutor queryExecutor,
            ICommandExecutor commandExecutor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
            _commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
        }

        [HttpGet("{id:int}")]
        [SwaggerResponse((int) HttpStatusCode.OK, type: typeof(BucketDocument))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, type: typeof(ProblemDetails))]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (id <= 0)
                return BadRequest(Result.Failed((int) HttpStatusCode.BadRequest, "Id can not be less that 1."));

            var query = new GetBucketQuery(id);
            var result = await _queryExecutor.ExecuteAsync<GetBucketQuery, BucketDocument>(query);

            return Ok(result);
        }

        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.OK, type: typeof(PaginationCollection<BucketDocument>))]
        public async Task<IActionResult> ListAsync(
            [FromQuery] int skip = 0,
            [FromQuery] int top = 100)
        {
            var query = new ListBucketQuery
            {
                Skip = skip,
                Top = top
            };

            var result =
                await _queryExecutor.ExecuteAsync<ListBucketQuery, PaginationCollection<BucketDocument>>(query);

            return Ok(result);
        }

        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.Created, type: typeof(BucketDocument))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, type: typeof(ProblemDetails))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBucketRequestDocument document)
        {
            var command = _mapper.MapFrom<CreateBucketRequestDocument, CreateBucketCommand>(document);
            var commandResult = await _commandExecutor.ExecuteAsync<CreateBucketCommand, int>(command);

            if (commandResult.IsFailed)
                return BadRequest(commandResult);

            var query = new GetBucketQuery(commandResult.Value);
            var queryResult = await _queryExecutor.ExecuteAsync<GetBucketQuery, BucketDocument>(query);

            if (queryResult.IsFailed)
                return BadRequest(queryResult);

            // ReSharper disable once Mvc.ActionNotResolved
            return CreatedAtAction(nameof(GetAsync), new {id = queryResult.Value!.Id}, queryResult);
        }

        [HttpDelete("{id:int}")]
        [SwaggerResponse((int) HttpStatusCode.NoContent)]
        [SwaggerResponse((int) HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, type: typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest(Result.Failed((int) HttpStatusCode.BadRequest, "Id can not be less that 1."));

            var command = new DeleteBucketCommand(id);
            var commandResult = await _commandExecutor.ExecuteAsync<DeleteBucketCommand, Void>(command);

            return commandResult.IsFailed
                ? BadRequest(commandResult)
                : NoContent();
        }

        [HttpPatch("{id:int}")]
        [SwaggerResponse((int) HttpStatusCode.NoContent)]
        [SwaggerResponse((int) HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, type: typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] EditBucketRequestDocument document)
        {
            if (id <= 0)
                return BadRequest(Result.Failed((int) HttpStatusCode.BadRequest, "Id can not be less that 1."));

            var command = new EditBucketCommand(id);
            _mapper.MapFrom(command, document);

            var commandResult = await _commandExecutor.ExecuteAsync<EditBucketCommand, Void>(command);

            return commandResult.IsFailed
                ? BadRequest(commandResult)
                : NoContent();
        }
    }
}