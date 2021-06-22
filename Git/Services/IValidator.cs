using System.Collections.Generic;

using Git.Models.Users;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserFormModel model);
    }
}
