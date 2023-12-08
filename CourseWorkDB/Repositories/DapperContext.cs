using  Microsoft.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using Dapper;
using WebSocketGraphql.Services;
using System;
using CourseWorkDB.Model;
using CourseWorkDB.ViewModel.Size;
using CourseWork_DB.Helpers;

namespace TimeTracker.Repositories
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SQLConnection");


            Mapper.SetMapper(typeof(Category));
            Mapper.SetMapper(typeof(Creator));
            Mapper.SetMapper(typeof(Product));
            Mapper.SetMapper(typeof(Size));
            Mapper.SetMapper(typeof(SizeInfo));
            Mapper.SetMapper(typeof(StoneInfo));
            Mapper.SetMapper(typeof(Discount));
            Mapper.SetMapper(typeof(User));
            Mapper.SetMapper(typeof(UserPermission));
            Mapper.SetMapper(typeof(History));
            Mapper.SetMapper(typeof(SelectedProduct));


            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
            SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());

            SqlMapper.AddTypeHandler(new DateTimeHandler());

        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

    }
    public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
    {
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.Value = value;
        }

        public override DateTime Parse(object value)
        {
            return DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
        }
    }
}
    