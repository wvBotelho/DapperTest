using System;
using System.Data;

namespace TesteDapperRepository.Infraestrutura
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection GetConnection { get; }
    }
}
