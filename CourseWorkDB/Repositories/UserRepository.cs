using CourseWorkDB.Model;
using CourseWorkDB.ViewModel.User;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.Text;
using TimeTracker.Repositories;
using TimeTracker.Services;

namespace CourseWorkDB.Repositories
{
    public class UserRepository : IUserRepository
    {
        DapperContext _dapperContext;

        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<User> AddUser(User user)
        {
            string query = @"BEGIN TRANSACTION;
            declare @ID table (ID int)
            INSERT INTO Users (full_name,telephone_number,password,salt) OUTPUT inserted.id into @ID Values(@FullName,@TelephoneNumber,@Password,@Salt)
            DECLARE @InsertedID int = (SELECT TOP 1 ID FROM @ID);
            INSERT INTO UserPermission(user_id,product_manage,user_manage) VALUES(@InsertedID,@ProductManage,@UserManage)
            SELECT @InsertedID
            COMMIT;";

            using var connection = _dapperContext.CreateConnection();
            user.Id = await connection.QuerySingleAsync<int>(query, new
            {
                FullName = user.FullName,
                TelephoneNumber = user.TelephoneNumber,
                Password = user.Password,
                Salt = user.Salt,
                ProductManage = user.Permissions.ProductManage,
                UserManage = user.Permissions.UserManage
            });

            return user;
        }

        public async Task<User?> GetUser(string tel, string password, int iteration)
        {
            string query = @"SELECT u.id,u.full_name,u.telephone_number,u.password,u.salt,up.product_manage,up.user_manage FROM Users as u
            JOIN UserPermission  as up
            ON u.telephone_number = @tel AND u.id = up.user_id AND u.disabled is distinct from 1";

            using var connection = _dapperContext.CreateConnection();

            var user = (await connection.QueryAsync<User?, UserPermission, User?>(query, (user, perm) =>
            {
                if (user is not null)
                {
                    user.Permissions = perm;
                }
                return user;

            }, new { tel }, splitOn: "product_manage")).SingleOrDefault();

            if (user is null || !password.ComparePasswords(user.Password, true, user.Salt, iteration))
            {
                return null;
            }

            return user;
        }

        public async Task<int> DisableUser(int id)
        {
            string query = "UPDATE Users SET disabled = 1 WHERE id = @id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { id });
            return id;
        }

        public async Task<IEnumerable<UserSelfData>> GetUsers(UserSort userSort)
        {
            StringBuilder query = new StringBuilder("Select full_name,telephone_number,id From Users");

            if(userSort.TelephoneNumber is not null)
            {
                query.Append("WHERE telephone_number = @TelephoneNumber");
            }

            if(userSort.FullName is not null)
            {
                query.AppendFormat(" {0} full_name Like '%{1}%'", userSort.TelephoneNumber is null?
                    "WHERE":"AND",userSort.FullName);   
            }

            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<UserSelfData>(query.ToString());
        }

        public async Task<ChangePermission> ChangePermission(ChangePermission changePermission)
        {
            string query = "UPDATE UserPermission SET user_manage = @UserManage, product_manage = @ProductManage where user_id = @Id";
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { changePermission.Id, changePermission.Permission.UserManage, changePermission.Permission.ProductManage });
            return changePermission;
        }

    }
}
