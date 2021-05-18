namespace PavValHackathon.Web.API.Infrastructure.Extensions
{
    internal static class PaginationCollectionExtensions
    {
        private static readonly string SkipParameterName = nameof(PaginationFilter.Skip).ToLower();
        private static readonly string TopParameterName = nameof(PaginationFilter.Top).ToLower();

        public static void SetPrevHref(this PaginationCollection collection, string path, PaginationFilter paginationFilter)
        {
            var skip = paginationFilter.Skip - paginationFilter.Top;

            collection.PrevHref = skip > 0
                ? BuildUri(path, skip, paginationFilter.Top)
                : null;
        }

        public static void SetNexHref(this PaginationCollection collection, string path, PaginationFilter paginationFilter)
        {
            var skip = paginationFilter.Skip + paginationFilter.Top;

            collection.NextHref = skip < collection.Count
                ? BuildUri(path, skip, paginationFilter.Top)
                : null;
        }

        public static void SetLastHref(this PaginationCollection collection, string path, PaginationFilter paginationFilter)
        {
            var skip = collection.Count - paginationFilter.Top > 0
                ? collection.Count - paginationFilter.Top
                : 0;

            collection.LastHref = BuildUri(path, skip, paginationFilter.Top);
        }

        public static void SetFirstHref(this PaginationCollection collection, string path, PaginationFilter paginationFilter)
        {
            collection.FirstHref = BuildUri(path, 0, paginationFilter.Top);
        }

        private static string BuildUri(string baseUri, int skip, int top)
        {
            return $"{baseUri}?{SkipParameterName}={skip}&{TopParameterName}={top}";
        }
    }
}