namespace SeoChecker.SEORankChecker.Models
{
    public class SearchResult
    {
        private string _url { get; init; }
        private List<int> _positions { get; set; } = [];

        public SearchResult(string url)
        {
            _url = url;
        }

        public IReadOnlyCollection<int> Positions => _positions.AsReadOnly();
        public string Url => _url;
        public string Html { get; set; }
        public void AddPosition(int position)
        {
            _positions.Add(position);
        }

        public bool IsUrlFound => Positions.Count > 0;
    }
}
