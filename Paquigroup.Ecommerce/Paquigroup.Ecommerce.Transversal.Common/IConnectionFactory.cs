using System.Data;


namespace Paquigroup.Ecommerce.Transversal.Common
{
    public interface IConnectionFactory
    {

        IDbConnection GetConnection { get; }

    }
}
