using System.ComponentModel;

namespace CourseWorkDB.ViewModel.User
{
    public class UserSelfData
    {
        [Description("id")]
        public int Id { get; set; }
        [Description("full_name")]
        public string FullName { get; set; }
        [Description("telephone_number")]
        public string TelephoneNumber { get; set; }
    }
}
