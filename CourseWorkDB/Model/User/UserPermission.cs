using System.ComponentModel;

namespace CourseWorkDB.Model.User
{
    public class UserPermission
    {
        [Description("product_manage")]
        public bool ProductManage { get; set; }
        [Description("user_manage")]
        public bool UserManage { get; set; }
    }
}
