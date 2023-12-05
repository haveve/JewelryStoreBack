using CourseWorkDB.Graphql.Mutation.User;
using CourseWorkDB.Model;
using CourseWorkDB.ViewModel.User;
using Dapper;
using Microsoft.AspNetCore.Identity;
using TimeTracker.Repositories;
using TimeTracker.Services;

namespace CourseWorkDB.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<IEnumerable<LoginUser>> GetUsers();
        Task<User?> GetUser(string tel, string password, int iteration);
        Task<int> DisableUser(int id);
        Task<ChangePermission> ChangePermission(ChangePermission changePermission);
    }
}
