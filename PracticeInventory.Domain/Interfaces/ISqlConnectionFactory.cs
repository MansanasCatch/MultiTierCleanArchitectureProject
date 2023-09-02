using Microsoft.Data.SqlClient;

namespace PracticeInventory.Domain.Interfaces;

public interface ISqlConnectionFactory
{
    SqlConnection Create();
}
