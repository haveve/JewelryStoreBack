using Dapper;
using System.Data;
using System.Text;

namespace TimeTracker.Services
{
    public static class DapperBulk
    {
        public static async Task<int> BulkInsertAsync<T>(this IDbConnection connection, IEnumerable<T> data, Func<T, string> toSpecificString, string values, string toTabel)
        {
            string intialString = @$"INSERT INTO {toTabel} {values} VALUES";
            StringBuilder query = new(intialString);

            int count = 0;
            int iteration = 0;

            foreach (var el in data)
            {
                query.Append(toSpecificString(el) + ',');

                if ((iteration + 1) % 1000 == 0)
                {
                    count += await connection.ExecuteAsync(query.ToString().TrimEnd(',')).ConfigureAwait(false);
                    query.Clear();
                    query.Append(intialString);
                }

                iteration++;
            }

            if (count % 999 != 0 || count == 0)
                count += await connection.ExecuteAsync(query.ToString().TrimEnd(',')).ConfigureAwait(false);

            return count;
        }
    }
}
