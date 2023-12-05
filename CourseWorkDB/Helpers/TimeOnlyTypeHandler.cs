using Dapper;
using System.Data;

namespace CourseWork_DB.Helpers
{

    public class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
    {
        public override TimeOnly Parse(object value)
        {
            var data = (TimeSpan)value;
            return new TimeOnly(data.Hours,data.Minutes,data.Seconds);
        }

        public override void SetValue(IDbDataParameter parameter, TimeOnly value)
        {
            parameter.DbType = DbType.Time;
            parameter.Value = value;
        }
    }
}
