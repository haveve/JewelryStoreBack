using CourseWorkDB.Model;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.User
{
    public class UserPermissionInputGraphType : InputObjectGraphType<UserPermission>
    {
        public UserPermissionInputGraphType()
        {
            Field(el => el.UserManage);
            Field(el => el.ProductManage);
        }
    }
}
