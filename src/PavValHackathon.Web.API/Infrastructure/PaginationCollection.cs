using System;
using System.Collections.Generic;

namespace PavValHackathon.Web.API.Infrastructure
{
    public abstract class PaginationCollection
    {
        public int Count { get; set; }
        public string? NextHref { get; set; }
        public string? PrevHref { get; set; }
        public string FirstHref { get; set; } = null!;
        public string LastHref { get; set; } = null!;
    }

    public class PaginationCollection<T> : PaginationCollection
    {
        public PaginationCollection()
        {
        }

        public PaginationCollection(ICollection<T> collection)
        {
            Items = collection;
        }
        
        public ICollection<T> Items { get; set; } = Array.Empty<T>();
    }
}