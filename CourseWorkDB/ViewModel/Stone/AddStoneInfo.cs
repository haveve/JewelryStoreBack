using System.ComponentModel;

namespace CourseWorkDB.ViewModel.Size
{
    public class AddStoneInfo
    {
        public int ProductId { get; set; }
        public int StoneTypeId { get; set; }
        public int StoneShapeId { get; set; }
        public int StoneColorId { get; set; }
        public decimal WeightCarat { get; set; }
        public int Count { get; set; }
    }
}
