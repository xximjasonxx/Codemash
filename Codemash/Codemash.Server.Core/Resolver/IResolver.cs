namespace Codemash.Server.Core.Resolver
{
    public interface IResolver<T>
    {
        /// <summary>
        /// Resolve to an instance of type T
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T Resolve(string key);
    }
}
