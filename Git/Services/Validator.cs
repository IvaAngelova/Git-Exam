using Git.Models.Users;
using System.Collections.Generic;

using static Git.Data.DataConstants;

namespace Git.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateUserRegistration(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UsernameMinLength
                || model.Username.Length > UsernameMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid! It must be between {UsernameMinLength} and {UsernameMaxLength}.");
            }

            if (model.Password.Length < PassowrdMinLength
                || model.Username.Length > PassowrdMaxLength)
            {
                errors.Add($"The provided password is not valid! It must be between {PassowrdMinLength} and {PassowrdMaxLength}.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Parssword and its confirmation are different.");
            }

            return errors;
        }
    }
}
