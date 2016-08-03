namespace OpenIDConnect.Inspector
{
    /// <summary>
    /// Dependency resolver interface.
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// Gets an instance of the specified service.
        /// </summary>
        T GetService<T>() where T : class;
    }
}
