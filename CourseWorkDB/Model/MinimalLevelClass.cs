using System.ComponentModel;

namespace CourseWorkDB.Model
{
    public class MinimalLevelClass
    {
        [Description("id")]
        public int Id { get; set; }
        [Description("name")]
        public string Name { get; set; }
    }
}
