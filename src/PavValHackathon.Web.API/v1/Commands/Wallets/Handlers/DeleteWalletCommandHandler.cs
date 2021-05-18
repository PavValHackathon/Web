using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PavValHackathon.Web.API.Infrastructure;
using PavValHackathon.Web.API.Infrastructure.Extensions;
using PavValHackathon.Web.Common;
using PavValHackathon.Web.Common.Cqrs.Commands;
using PavValHackathon.Web.Data.Repositories.Custom;
using Void = PavValHackathon.Web.Common.Void;

namespace PavValHackathon.Web.API.v1.Commands.Wallets.Handlers
{
    public class DeleteWalletCommandHandler : ICommandHandler<DeleteWalletCommand, Void>
    {
        private readonly IUserContext _userContext;
        private readonly IWalletRepository _walletRepository;

        public DeleteWalletCommandHandler(IUserContext userContext, IWalletRepository walletRepository)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        }

        public async Task<Result<Void>> HandleAsync(DeleteWalletCommand command, CancellationToken cancellationToken = default)
        {
            Assert.IsNotNull(command, nameof(command));
            cancellationToken.ThrowIfCancellationRequested();

            var userId = _userContext.UserId;
            
            //TODO: delete all transactions!!!
            
            if (await _walletRepository.ExistAsync(command.Id, userId, cancellationToken) == false)
                return this.NotFound("Wallet not found.");

            await _walletRepository.DeleteAsync(command.Id, cancellationToken);
            
            return this.Ok();
        }
    }
}