namespace OpenIDConnect.Inspector.Tests.Common
{
    using System;

    /// <summary>
    /// Dependency resolver for the test runtime.
    /// </summary>
    public class TestDependencyResolver : IDependencyResolver
    {
        #region IDependencyResolver Members

        /// <summary>
        /// Gets an instance of the service with the specified type.
        /// </summary>
        public T GetService<T>() where T : class
        {
            if (typeof(T) == typeof(Fiddler.ISAZProvider))
            {
                return this.InstantiateSAZProvider() as T;
            }

            return default(T);
        }

        /// <summary>
        /// Instantiates an instance of Fiddler.XceedProvider class via reflection, since the type is marked as internal.
        /// </summary>
        private Fiddler.ISAZProvider InstantiateSAZProvider()
        {
            var sazProviderType = Type.GetType("Fiddler.XceedProvider,Fiddler");
            return Activator.CreateInstance(sazProviderType) as Fiddler.ISAZProvider;
        }

        #endregion
    }
}
