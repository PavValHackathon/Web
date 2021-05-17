namespace PavValHackathon.Web.Data.Domain
{
    public class Currency : IDomainEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}