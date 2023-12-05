using System.ComponentModel;

namespace CourseWorkDB.Model
{
    public class UserPermission
    {
        [Description("product_manage")]
        public bool ProductManage { get; set; }
        [Description("user_manage")]
        public bool UserManage { get; set; }
    }
}
