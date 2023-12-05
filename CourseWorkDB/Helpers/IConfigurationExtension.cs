using Dapper;
using System.Data;
using System.Runtime.CompilerServices;

namespace CourseWork_DB.Helpers
{
    public static class IConfigurationExtension
    { 
        public static int GetIteration(this IConfiguration config)
        {
            return Convert.ToInt32(config["Hash:iteration"]);
        }
    }
}
