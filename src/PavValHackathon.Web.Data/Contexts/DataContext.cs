using Microsoft.EntityFrameworkCore;

namespace PavValHackathon.Web.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}