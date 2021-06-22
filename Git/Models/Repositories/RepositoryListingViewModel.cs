namespace Git.Models.Repositories
{
    public class RepositoryListingViewModel
    {
        public string Id { get; init; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public string CreatedOn { get; set; }

        public int Commits { get; set; }
    }
}
