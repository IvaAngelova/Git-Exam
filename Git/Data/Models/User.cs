using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Id { get; set; }
            = Guid.NewGuid().ToString();

        [Required]
        [MinLength(DataConstants.UsernameMinLength)]
        [MaxLength(DataConstants.UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(DataConstants.PassowrdMinLength)]
        public string Password { get; set; }

        IEnumerable<Repository> Repositories { get; set; }
            = new List<Repository>();

        IEnumerable<Commit> Commits { get; set; }
            = new List<Commit>();
    }
}
