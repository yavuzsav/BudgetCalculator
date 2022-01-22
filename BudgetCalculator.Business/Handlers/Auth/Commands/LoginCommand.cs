using System.Security;
using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.Entities.Concrete;
using BudgetCalculator.Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BudgetCalculator.Business.Handlers.Auth.Commands
{
    public class LoginCommand : IRequest<IDataResult<LoginWithTokenDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, IDataResult<LoginWithTokenDto>>
        {
            private readonly SignInManager<AppUser> _signInManager;

            public LoginCommandHandler(SignInManager<AppUser> signInManager)
            {
                _signInManager = signInManager;
            }

            public async Task<IDataResult<LoginWithTokenDto>> Handle(LoginCommand request,
                CancellationToken cancellationToken)
            {
                var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

                if (result.Succeeded)
                {
                    // TODO: create token

                    return new SuccessDataResult<LoginWithTokenDto>(new LoginWithTokenDto()
                        { UserName = request.UserName, });
                }

                return new ErrorDataResult<LoginWithTokenDto>(Messages.UsernamePasswordWrong);
            }
        }
    }
}