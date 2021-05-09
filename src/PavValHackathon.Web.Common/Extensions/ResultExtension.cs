using System.Threading.Tasks;

namespace PavValHackathon.Web.Common.Extensions
{
    public static class ResultExtension
    {
        public static Task<Result> AsAsync(this Result result)
        {
            Assert.IsNotNull(result, nameof(result));
            
            return Task.FromResult(result);
        }
        
        public static Task<Result<T>> AsAsync<T>(this Result<T> result)
        {
            Assert.IsNotNull(result, nameof(result));
            
            return Task.FromResult(result);
        }
    }
}