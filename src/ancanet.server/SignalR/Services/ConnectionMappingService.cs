using ancanet.server.SignalR.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace ancanet.server.SignalR.Services;

/// <summary>
/// Represents a service for mapping connections to a key of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the key.</typeparam>
public class ConnectionMappingService<T>: IConnectionMapper<T>
{
    private readonly Dictionary<T, HashSet<string>> _connections =
        [];

    /// <summary>
    /// Gets the number of key-value pairs contained in the collection.
    /// </summary>
    /// <remarks>
    /// This property returns the number of key-value pairs contained in the collection.
    /// </remarks>
    public int Count
    {
        get
        {
            return _connections.Count;
        }
    }

    /// <summary>
    /// Adds the specified connection ID to an existing pool or creates a new pool associated with the given key identifier.
    /// </summary>
    /// <param name="key">The identifier for a collection of connection IDs.</param>
    /// <param name="connectionId">The connection ID to add.</param>
    /// <returns>True if the connection is new for the key identifier; otherwise, false.</returns>
    public bool Add(T key, string connectionId)
    {
        lock (_connections)
        {
            if (!_connections.TryGetValue(key, out var connections))
            {
                connections = [];
                _connections.Add(key, connections);
                return true;
            }

            lock (connections)
            {
                connections.Add(connectionId);
                return false;
            }
        }
    }

    /// <summary>
    /// Retrieves the collection of connection IDs associated with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the key.</typeparam>
    /// <param name="key">The key used to retrieve the collection of connection IDs.</param>
    /// <returns>
    /// The collection of connection IDs associated with the key, or an empty collection if the key is not found.
    /// </returns>
    public IEnumerable<string> GetConnections(T key)
    {
        return _connections.TryGetValue(key, out var connections) ? connections : Enumerable.Empty<string>();
    }

    /// <summary>
    /// Removes the specified connection ID from the collection associated with the given key identifier. 
    /// If no connections remain, the key identifier is also removed.
    /// </summary>
    /// <param name="key">The identifier for the collection of connection IDs.</param>
    /// <param name="connectionId">The connection ID to be removed.</param>
    /// <returns>True if there are still connections remaining for the key identifier; otherwise, false.</returns>
    public bool TryRemove(T key, string connectionId, [NotNullWhen(true)] out bool? isConnectionRemaining)
    {
        lock (_connections)
        {
            isConnectionRemaining = null;
            if (!_connections.TryGetValue(key, out var connections))
            {
                return false;
            }

            lock (connections)
            {
                connections.Remove(connectionId);

                if (connections.Count == 0)
                {
                    _connections.Remove(key);
                    isConnectionRemaining = false;
                }
                isConnectionRemaining = true;
            }
            return true;
        }
    }
}

public static class ConnectionMappingInjection
{
    public static IServiceCollection AddConnectionMappingService(this IServiceCollection services)
    {
        return services.AddSingleton<IConnectionMapper<string>, ConnectionMappingService<string>>();
    }
}

