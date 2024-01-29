using System.Text.Json;

namespace AcmeWeb
{
    /// <summary>
    /// Provides helpers for session operations
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Serialize an object to JSON, then store in the session under key
        /// </summary>
        /// <param name="session">this session</param>
        /// <param name="key">the lookup key</param>
        /// <param name="obj">The object to serialize</param>
        public static void SetJsonObject<T>(this ISession session, string key, T obj)
        {
            var jtext = JsonSerializer.Serialize(obj);
            session.SetString(key, jtext);
        }
        /// <summary>
        /// Deserializes an object previously stored in the session
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="session"></param>
        /// <param name="key">The lookup key</param>
        /// <returns>the deserialized object</returns>
        public static T? GetJsonObject<T>(this ISession session, string key)
        {
            return JsonSerializer.Deserialize<T>(session.GetString(key) ?? "");
        }
    }
}
