using System.ComponentModel;

namespace CourseWorkDB.Model
{
    public class StoneInfo
    {
        public StoneColor Color { get; set; }
        public StoneType Type { get; set; }
        public StoneShape Shape { get; set; }
        [Description("weight_carat")]
        public decimal WeightCarat {  get; set; }
        [Description("count")]
        public int Count { get; set; }
    }
}
