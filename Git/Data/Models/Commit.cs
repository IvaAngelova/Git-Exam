using System;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class Commit
    {
        [Key]
        [Required]
        public string Id { get; set; }
         = Guid.NewGuid().ToString();

        [Required]
        [MinLength(DataConstants.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
            = DateTime.UtcNow;

        [Required]
        public string CreatorId { get; set; }

        public User Creator { get; set; }

        [Required]
        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}
