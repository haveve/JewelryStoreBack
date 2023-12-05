using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.User
{
    public class UserPermissionGraphType:ObjectGraphType<UserPermission>
    {
        public UserPermissionGraphType()
        {
            Field(el => el.UserManage);
            Field(el => el.ProductManage);
        }
    }
}
