using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Public.API
{
    [TestClass]
    public partial class IUnityContainer_Interface
    {
        protected IUnityContainer Container;
        protected Type TypeFrom = typeof(IDictionary);
        protected Type TypeTo   = typeof(Hashtable);
        protected const string Name = "name";
        protected InjectionConstructor Constructor = new InjectionConstructor();
        protected ContainerControlledLifetimeManager Manager = new ContainerControlledLifetimeManager();

        [TestInitialize]
        public virtual void TestInitialize() => Container = new UnityContainer();

        [TestMethod]
        public void Baseline()
        {
            Assert.IsNotNull(Container);
            Assert.IsInstanceOfType(Container, typeof(IUnityContainer));
        }

        [TestMethod]
        public void RegisterType()
        {
            // Act 
            Container.RegisterType(TypeFrom, TypeTo, Name, Manager, Constructor);

            // Validate
            var registration = Container.Registrations.Last();

            Assert.AreEqual(TypeFrom, registration.RegisteredType);
            Assert.AreEqual(TypeTo,   registration.MappedToType);
            Assert.AreEqual(Name,     registration.Name);
        }


        [TestMethod]
        public void RegisterInstance()
        {
            // Act 
            Container.RegisterInstance(TypeFrom, Name, new Hashtable(), Manager);

            // Validate
            var registration = Container.Registrations.Last();

            Assert.AreEqual(TypeFrom, registration.RegisteredType);
            Assert.AreEqual(Name, registration.Name);
        }

        /*
    public interface IUnityContainer : IDisposable
    {

        /// <summary>
        /// Resolve an instance of the requested type with the given name from the container.
        /// </summary>
        /// <param name="t"><see cref="Type"/> of object to get from the container.</param>
        /// <param name="name">Name of the object to retrieve.</param>
        /// <param name="resolverOverrides">Any overrides for the resolve call.</param>
        /// <returns>The retrieved object.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
        object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides);

        /// <summary>
        /// Return instances of all registered types requested.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is useful if you've registered multiple types with the same
        /// <see cref="Type"/> but different names.
        /// </para>
        /// <para>
        /// Be aware that this method does NOT return an instance for the default (unnamed) registration.
        /// </para>
        /// </remarks>
        /// <param name="t">The type requested.</param>
        /// <param name="resolverOverrides">Any overrides for the resolve calls.</param>
        /// <returns>Set of objects of type <paramref name="t"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
        IEnumerable<object> ResolveAll(Type t, params ResolverOverride[] resolverOverrides);

        /// <summary>
        /// Run an existing object through the container and perform injection on it.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is useful when you don't control the construction of an
        /// instance (ASP.NET pages or objects created via XAML, for instance)
        /// but you still want properties and other injection performed.
        /// </para></remarks>
        /// <param name="t"><see cref="Type"/> of object to perform injection on.</param>
        /// <param name="existing">Instance to build up.</param>
        /// <param name="name">name to use when looking up the TypeMappings and other configurations.</param>
        /// <param name="resolverOverrides">Any overrides for the resolve calls.</param>
        /// <returns>The resulting object. By default, this will be <paramref name="existing"/>, but
        /// container extensions may add things like automatic proxy creation which would
        /// cause this to return a different object (but still type compatible with <paramref name="t"/>).</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "BuildUp is correct.")]
        object BuildUp(Type t, object existing, string name, params ResolverOverride[] resolverOverrides);

        /// <summary>
        /// Run an existing object through the container, and clean it up.
        /// </summary>
        /// <param name="o">The object to tear down.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o", Justification = "Parameter name is valid in context")]
        void Teardown(object o);

        /// <summary>
        /// Add an extension object to the container.
        /// </summary>
        /// <param name="extension"><see cref="UnityContainerExtension"/> to add.</param>
        /// <returns>The <see cref="UnityContainer"/> object that this method was called on (this in C#, Me in Visual Basic).</returns>
        IUnityContainer AddExtension(UnityContainerExtension extension);

        /// <summary>
        /// Resolve access to a configuration interface exposed by an extension.
        /// </summary>
        /// <remarks>Extensions can expose configuration interfaces as well as adding
        /// strategies and policies to the container. This method walks the list of
        /// added extensions and returns the first one that implements the requested type.
        /// </remarks>
        /// <param name="configurationInterface"><see cref="Type"/> of configuration interface required.</param>
        /// <returns>The requested extension's configuration interface, or null if not found.</returns>
        object Configure(Type configurationInterface);

        /// <summary>
        /// Remove all installed extensions from this container.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method removes all extensions from the container, including the default ones
        /// that implement the out-of-the-box behavior. After this method, if you want to use
        /// the container again you will need to either read the default extensions or replace
        /// them with your own.
        /// </para>
        /// <para>
        /// The registered instances and singletons that have already been set up in this container
        /// do not get removed.
        /// </para>
        /// </remarks>
        /// <returns>The <see cref="UnityContainer"/> object that this method was called on (this in C#, Me in Visual Basic).</returns>
        IUnityContainer RemoveAllExtensions();

        /// <summary>
        /// The parent of this container.
        /// </summary>
        /// <value>The parent container, or null if this container doesn't have one.</value>
        IUnityContainer Parent { get; }

        /// <summary>
        /// Create a child container.
        /// </summary>
        /// <remarks>
        /// A child container shares the parent's configuration, but can be configured with different
        /// settings or lifetime.</remarks>
        /// <returns>The new child container.</returns>
        IUnityContainer CreateChildContainer();

        /// <summary>
        /// Get a sequence of <see cref="ContainerRegistration"/> that describe the current state
        /// of the container.
        /// </summary>
        IEnumerable<ContainerRegistration> Registrations { get; }
    }
         */
    }
}
