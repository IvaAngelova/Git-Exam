using System.Collections.Generic;

using Git.Models.Users;
using Git.Models.Commits;
using Git.Models.Repositories;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserFormModel model);

        ICollection<string> ValidateRepositoryCreation(CreateRepositoryForModel model);

        ICollection<string> ValidateCommitCreation(CreateCommitForModel model);
    }
}
