namespace PavValHackathon.Web.API.Infrastructure
{
    public class PaginationFilter
    {
        public int Skip { get; set; } = 0;
        public int Top { get; set; } = 100;
    }
}