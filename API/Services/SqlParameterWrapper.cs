using Microsoft.Data.SqlClient;
namespace API.Services
{

    public static class SqlParameterWrapper
    {
        public static SqlParameter[] Create(params (string name, object value)[] parameters)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            foreach (var (name, value) in parameters)
            {
                sqlParameters.Add(new SqlParameter(name, value ?? DBNull.Value));
            }

            return sqlParameters.ToArray();
        }
    }
}
