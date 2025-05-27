using Learnly.APIs.DTOs;

namespace Learnly.APIs.Helpers
{
    public class Pagination<T>
    {
        public Pagination(int pageSize, int pageIndex, int count, IReadOnlyList<T> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
            Count = count;
        }

        public int PageIndex { get; set; } 
        
        public int PageSize { get; set; }

        public int Count { get; set; } // total count

        public IReadOnlyList<T> Data { get; set; }
    }
}
