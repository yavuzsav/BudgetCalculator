using System.Threading;
using System.Threading.Tasks;
using BudgetCalculator.Business.Constants;
using BudgetCalculator.Core.Utilities.Results;
using BudgetCalculator.Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BudgetCalculator.Business.Handlers.Auth.Commands
{
    public class ResetPasswordCommand : IRequest<IResult>
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IResult>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;

            public ResetPasswordCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<IResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                {
                    return new ErrorResult(Messages.UserNotFound);
                }

                var canSingIn = await _userManager.CheckPasswordAsync(user, request.OldPassword);

                if (!canSingIn)
                {
                    return new ErrorResult(Messages.UserInformationIncorrect);
                }

                var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, passwordResetToken, request.NewPassword);

                if (result.Succeeded)
                {
                    return new SuccessResult(Messages.PasswordChangeSuccess);
                }

                return new ErrorResult(Messages.PasswordChangeFail);
            }
        }
    }
}