using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class Repository
    {
        [Key]
        [Required]
        public string Id { get; set; }
          = Guid.NewGuid().ToString();

        [Required]
        [MinLength(DataConstants.RepositoryNameMinLength)]
        [MaxLength(DataConstants.RepositoryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
            = DateTime.UtcNow;
        
        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        IEnumerable<Commit> Commits { get; set; }
            = new List<Commit>();
    }
}
