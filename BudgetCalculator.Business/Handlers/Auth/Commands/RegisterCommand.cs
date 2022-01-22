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
    public class RegisterCommand : IRequest<IDataResult<RegisterDtoWithToken>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IDataResult<RegisterDtoWithToken>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;

            public RegisterCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<IDataResult<RegisterDtoWithToken>> Handle(RegisterCommand request,
                CancellationToken cancellationToken)
            {
                var user = new AppUser()
                {
                    Email = request.Email,
                    Name = request.Name,
                    Surname = request.Surname,
                    UserName = request.UserName
                };

                var result = await _userManager.CreateAsync(user, request.Password);


                // TODO: create token

                if (result.Succeeded)
                    return new SuccessDataResult<RegisterDtoWithToken>(new RegisterDtoWithToken()
                    {
                        Email = request.Email,
                        Name = request.Name,
                        Surname = request.Surname,
                        UserName = request.UserName
                    });

                return new ErrorDataResult<RegisterDtoWithToken>(Messages.RegistrationFail);
            }
        }
    }
}