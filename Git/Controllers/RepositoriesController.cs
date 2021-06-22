using System.Linq;

using MyWebServer.Http;
using MyWebServer.Controllers;

using Git.Data;
using Git.Services;
using Git.Data.Models;
using Git.Models.Repositories;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IValidator validator;
        private readonly GitDbContext contex;

        public RepositoriesController(IValidator validator, GitDbContext contex)
        {
            this.validator = validator;
            this.contex = contex;
        }

        public HttpResponse All()
        {
            var repositoriesQuery = this.contex
                .Repositories
                .AsQueryable();

            if (this.User.IsAuthenticated)
            {
                repositoriesQuery = repositoriesQuery
                    .Where(r => r.IsPublic || r.OwnerId == this.User.Id);
            }
            else
            {
                repositoriesQuery = repositoriesQuery
                    .Where(r => r.IsPublic);
            }

            var repositores = repositoriesQuery
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new RepositoryListingViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Owner = r.Owner.Username,
                    CreatedOn = r.CreatedOn.ToLocalTime().ToString("F"),
                    Commits = r.Commits.Count()
                })
                .ToList();

            return View(repositores);
        }

        [Authorize]
        public HttpResponse Create()
        {
            if (this.User.IsAuthenticated)
            {
                return View();
            }

            return Error("Only users can create repositories.");
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateRepositoryForModel model)
        {
            var modelErrors = this.validator.ValidateRepositoryCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var repository = new Repository
            {
                Name = model.Name,
                IsPublic = model.RepositoryType == "Public",
                OwnerId = this.User.Id
            };

            contex.Repositories.Add(repository);

            contex.SaveChanges();

            return Redirect("/Repositories/All");
        }
    }
}
