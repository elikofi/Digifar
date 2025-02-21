

using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace Digifar.Application.Dashboard.WalletManagement.Commands
{
    public class CreateUserWalletCommandHandler(IWalletRepository walletRepository,
        IMapper mapper) : IRequestHandler<CreateUserWalletCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateUserWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = mapper.Map<Wallet>(request);

            return await walletRepository.AddWalletAsync(wallet);
        }
    }
}
