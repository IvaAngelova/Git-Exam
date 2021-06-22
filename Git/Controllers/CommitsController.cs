using MyWebServer.Controllers;

using Git.Data;
using Git.Services;
using MyWebServer.Http;
using System.Linq;
using Git.Models.Commits;
using Git.Data.Models;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly IValidator validator;
        private readonly GitDbContext contex;

        public CommitsController(IValidator validator, GitDbContext contex)
        {
            this.validator = validator;
            this.contex = contex;
        }

        [Authorize]
        public HttpResponse All()
        {
            var commits = contex
                .Commits
                .Where(c => c.CreatorId == this.User.Id)
                .Select(c => new CommitListingViewModel
                {
                    Id = c.Id,
                    Repository = c.Repository.Name,
                    Description = c.Description,
                    CreatedOn = c.CreatedOn.ToLocalTime().ToString("F")
                })
                .ToList();

            return View(commits);
        }

        [Authorize]
        public HttpResponse Create(string id)
        {
            var repository = contex
                .Repositories
                .Where(r => r.Id == id)
                .Select(r => new CommitToRepositoryForModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .FirstOrDefault();

            if (repository == null)
            {
                return BadRequest();
            }

            return View(repository);
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateCommitForModel model)
        {
            var modelErrors = this.validator.ValidateCommitCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var commit = new Commit
            {
                RepositoryId = model.Id,
                Description = model.Description,
                CreatorId = User.Id
            };

            contex.Commits.Add(commit);

            contex.SaveChanges();

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse Delete(string id)
        {
            var commit = this.contex
                .Commits
                .FirstOrDefault(c => c.Id == id);

            this.contex.Commits.Remove(commit);

            this.contex.SaveChanges();

            return Redirect("/Commits/All");
        }
    }
}
