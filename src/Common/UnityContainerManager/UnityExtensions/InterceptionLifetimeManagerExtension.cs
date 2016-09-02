using System.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace SymDemo.UnityContainerManager.UnityExtensions
{
    /// <summary>
    ///     Unity had a problem where the combination of a singleton and interface interception / policy injection did not work
    ///     well from a performance perspective. This is because of the order Microsoft executes it's code.
    ///     This extension will change the order and allow a singleton to work like a singleton (without regenerating the proxy
    ///     classes) which will significantly reduce overhead with unity singleton object creation.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class InterceptionLifetimeManagerExtension : UnityContainerExtension
    {
        /// <summary>
        ///     Initialization method for the unity extension.
        /// </summary>
        protected override void Initialize()
        {
            if (Context == null) throw new NoNullAllowedException("Unity container context cannot be null");
            if (Context.Strategies == null) throw new NoNullAllowedException("Unity container context strategies cannot be null");
            Context.Strategies.AddNew<InstanceInterceptionStrategy>(UnityBuildStage.PostInitialization);
            Context.Container.RegisterInstance<InjectionPolicy>(
                typeof(AttributeDrivenPolicy).AssemblyQualifiedName,
                new AttributeDrivenPolicy());
        }
    }
}