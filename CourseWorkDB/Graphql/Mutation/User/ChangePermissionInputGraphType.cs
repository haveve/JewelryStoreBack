using CourseWorkDB.ViewModel.User;
using GraphQL.Types;

namespace CourseWorkDB.Graphql.Mutation.User
{
    public class ChangePermissionInputGraphType : InputObjectGraphType<ChangePermission>
    {
        public ChangePermissionInputGraphType() 
        {
            Field(el => el.Id);
            Field(el => el.Permission, type : typeof(UserPermissionInputGraphType));
        }
    }
}
