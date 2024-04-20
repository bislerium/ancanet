using System.Diagnostics.CodeAnalysis;

namespace ancanet.server.SignalR.Contracts
{
    public interface IConnectionMapper<T>
    {
        int Count { get; }
        bool Add(T key, string connectionId);
        IEnumerable<string> GetConnections(T key);
        bool TryRemove(T key, string connectionId, [NotNullWhen(true)] out bool? isConnectionRemaining);
    }
}
