using System.Linq;

using Git.Data;
using Git.Services;
using Git.Data.Models;
using Git.Models.Users;

using MyWebServer.Http;
using MyWebServer.Controllers;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly IValidator validator;
        private readonly GitDbContext contex;

        public UsersController(IPasswordHasher passwordHasher, IValidator validator, GitDbContext contex)
        {
            this.passwordHasher = passwordHasher;
            this.validator = validator;
            this.contex = contex;
        }

        public HttpResponse Register() 
            => View();

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelErrors = this.validator.ValidateUserRegistration(model);

            if (this.contex.Users.Any(u => u.Username == model.Username))
            {
                modelErrors.Add($"User with '{model.Username}' username already exists.");
            }

            if (this.contex.Users.Any(u => u.Email == model.Email))
            {
                modelErrors.Add($"User with '{model.Email}' e-mail already exists.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var user = new User
            {
                Username = model.Username,
                Password = this.passwordHasher.HashPassword(model.Password),
                Email = model.Email
            };

            contex.Users.Add(user);

            contex.SaveChanges();

            return Redirect("/Users/Login");

        }

        public HttpResponse Login() 
            => View();

        [HttpPost]
        public HttpResponse Login(LoginUserFormModel model)
        {
            var hashedPassword = this.passwordHasher.HashPassword(model.Password);

            var userId = this.contex
                .Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error("Username and password combination is not valid.");
            }

            this.SignIn(userId);

            return Redirect("/Repositories/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
