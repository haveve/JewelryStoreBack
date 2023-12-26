using System.ComponentModel;

namespace CourseWorkDB.Model.DetailsInfo.Size
{
    public class SizeInfo
    {
        public Size Size { get; set; }
        [Description("cost")]
        public decimal Cost { get; set; }
        [Description("count")]
        public int Count { get; set; }
        [Description("weight_gram")]
        public decimal WeightGram { get; set; }
    }
}
