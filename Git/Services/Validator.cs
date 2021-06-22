using System.Linq;
using System.Collections.Generic;

using Git.Models.Users;
using Git.Models.Commits;
using Git.Models.Repositories;

using static Git.Data.DataConstants;

namespace Git.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCommitCreation(CreateCommitForModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < DescriptionMinLength)
            {
                errors.Add($"Description '{model.Description}' is not valid! Min Length must be {DescriptionMinLength}");
            }

            return errors;
        }

        public ICollection<string> ValidateRepositoryCreation(CreateRepositoryForModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < RepositoryNameMinLength
                || model.Name.Length > RepositoryNameMaxLength)
            {
                errors.Add($"Repository name '{model.Name}' is not valid! It must be between {RepositoryNameMinLength} and {RepositoryNameMaxLength}.");
            }

            if (model.RepositoryType != "Public" && model.RepositoryType != "Private")
            {
                errors.Add($"Repository type can be either 'Public' or 'Private'.");
            }

            return errors;
        }

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

            if (model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Parssword and its confirmation are different.");
            }

            return errors;
        }
    }
}
