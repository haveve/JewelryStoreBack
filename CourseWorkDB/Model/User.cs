using System.ComponentModel;

namespace CourseWorkDB.Model
{
    public class User
    {
        [Description("id")]
        public int Id { get; set; }
        [Description("full_name")]
        public string FullName { get; set; }
        [Description("telephone_number")]
        public string TelephoneNumber {get;set;}
        [Description("password")]
        public string Password { get; set;}
        [Description("salt")]
        public string Salt { get; set;}
        public UserPermission Permissions { get; set;}
    }
}
