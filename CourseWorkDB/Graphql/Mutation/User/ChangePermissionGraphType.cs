using CourseWorkDB.ViewModel.User;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.User
{
    public class ChangePermissionGraphType:ObjectGraphType<ChangePermission>
    {
        public ChangePermissionGraphType() 
        {
            Field(el => el.Id);
            Field(el => el.Permission, type : typeof(UserPermissionGraphType));
        }
    }
}
