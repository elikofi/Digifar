using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using MapsterMapper;
using MediatR;

namespace Digifar.Application.Dashboard.WalletManagement.Commands
{
    public class DeleteUserWalletCommandHandler(IWalletRepository walletRepository) : IRequestHandler<DeleteUserWalletCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteUserWalletCommand request, CancellationToken cancellationToken)
        {
            return await walletRepository.DeleteWalletAsync(request.WalletId);
        }
    }
}
