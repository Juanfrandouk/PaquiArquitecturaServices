using Dapper;
using Paquigroup.Ecommerce.Domain.Entity;
using Paquigroup.Ecommerce.Domain.Interface;
using Paquigroup.Ecommerce.Transversal.Common;
using System.Data;


namespace Paquigroup.Ecommerce.Infrastructura.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

        }
        public Users Authenticate(string username, string password)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@Password", password);
                var result = connection.QuerySingle<Users>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result;

            }

        }
    }
}
