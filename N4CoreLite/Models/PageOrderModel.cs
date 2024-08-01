using N4CoreLite.Extensions;

namespace N4CoreLite.Models
{
    public class PageOrderModel
    {
        public int PageNumber { get; set; }
        public string RecordsPerPageCount { get; set; }
        public int TotalRecordsCount { get; set; }
        public List<string> RecordsPerPageCounts { get; set; }

        public List<int> PageNumbers
        {
            get
            {
                var pageNumbers = new List<int>();
                int recordsPerPageCount;
                if (TotalRecordsCount > 0 && int.TryParse(RecordsPerPageCount, out recordsPerPageCount))
                {
                    int numberOfPages = Convert.ToInt32(Math.Ceiling(TotalRecordsCount / Convert.ToDecimal(recordsPerPageCount)));
                    for (int page = 1; page <= numberOfPages; page++)
                    {
                        pageNumbers.Add(page);
                    }
                }
                else
                {
                    pageNumbers.Add(1);
                }
                return pageNumbers;
            }
        }

        public string OrderExpression { get; set; }
        public Dictionary<string, string> OrderExpressionDictionary { get; private set; }

        private List<string> _orderExpressionsForEntityProperties;

        /// <summary>
        /// Must be assigned by related entity property names.
        /// Turkish characters will be replaced with relevant English characters.
        /// </summary>
        public List<string> OrderExpressionsForEntityProperties
        {
            get
            {
                return _orderExpressionsForEntityProperties;
            }
            set
            {
                _orderExpressionsForEntityProperties = value;
                OrderExpressionDictionary = new Dictionary<string, string>();
                foreach (var orderExpression in _orderExpressionsForEntityProperties)
                {
                    OrderExpressionDictionary.Add(orderExpression.ChangeTurkishCharactersToEnglish().Replace(" ", ""), orderExpression);
                    OrderExpressionDictionary.Add(orderExpression.ChangeTurkishCharactersToEnglish().Replace(" ", "") + "Desc", orderExpression + " Azalan");
                }
                if (OrderExpressionDictionary.Any())
                {
                    OrderExpression = string.IsNullOrWhiteSpace(OrderExpression) ? OrderExpressionDictionary.First().Key : OrderExpression;
                }
            }
        }

        public PageOrderModel()
        {
            PageNumber = 1;
            RecordsPerPageCount = "10";
            RecordsPerPageCounts = new List<string>() { "5", "10", "25", "50", "100", "Tümü" };
            OrderExpression = string.Empty;
            _orderExpressionsForEntityProperties = new List<string>();
            OrderExpressionDictionary = new Dictionary<string, string>();
        }
    }
}
