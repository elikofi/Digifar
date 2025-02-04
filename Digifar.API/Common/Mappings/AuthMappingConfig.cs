using Digifar.Application.Authentication.Common;
using Digifar.Application.Authentication.UserManagement.Commands.Register;
using Digifar.Contracts.Authentication;
using Mapster;

namespace Digifar.API.Common.Mappings
{
    public class AuthMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterUserRequest, RegisterUserCommand>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest, src => src.User);
        }
    }
}
