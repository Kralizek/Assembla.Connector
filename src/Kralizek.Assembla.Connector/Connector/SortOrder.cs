namespace Kralizek.Assembla.Connector
{
    public struct SortOrder
    {
        public string Direction { get; }

        private SortOrder(string direction)
        {
            Direction = direction;
        }

        public static readonly SortOrder Ascending = new SortOrder("asc");
        public static readonly SortOrder Descending = new SortOrder("desc");
    }
}