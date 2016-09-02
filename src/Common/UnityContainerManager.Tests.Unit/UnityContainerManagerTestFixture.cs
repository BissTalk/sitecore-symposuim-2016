using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SymDemo.UnityContainerManager.Tests.Unit
{
    /// <summary>
    /// Unity Container Manager
    /// </summary>
    [TestClass]
    public class UnityContainerManagerTestFixture
    {
        /// <summary>
        /// Getses the default unity container if not provided.
        /// </summary>
        [TestMethod]
        [TestCategory("Unity Container Manager tests")]
        public void GetsDefaultUnityContainerIfNotProvided()
        {
            var unityContainerManager = UnityContainerManager.Instance;
            unityContainerManager.Add(new UnityContainer());
            var result = unityContainerManager.Get();
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Adds the default unity container if not provided.
        /// </summary>
        [TestMethod]
        [TestCategory("Unity Container Manager tests")]
        public void AddDefaultUnityContainerIfNotProvided()
        {
            var unityContainerManager = UnityContainerManager.Instance;
            var result = unityContainerManager.Get();
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Addses the new unity container to manager.
        /// </summary>
        [TestMethod]
        [TestCategory("Unity Container Manager tests")]
        public void AddsNewUnityContainerToManager()
        {
            var unityContainerManager = UnityContainerManager.Instance;
            unityContainerManager.Add("test", new UnityContainer());
            var result = unityContainerManager.Get("test");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Removeses the name of the unity container from manager and adds new with the same.
        /// </summary>
        [TestMethod]
        [TestCategory("Unity Container Manager tests")]
        public void RemovesUnityContainerFromManagerAndAddsNewWithTheSameName()
        {
            var unityContainerManager = UnityContainerManager.Instance;
            unityContainerManager.Add("test", new UnityContainer());
            var result = unityContainerManager.Get("test");
            Assert.IsNotNull(result);
            unityContainerManager.Remove("test");
            result = unityContainerManager.Get("test");
            Assert.IsNotNull(result);
        }


        /// <summary>
        /// Determines whether this instance [can not remove unity container from manager if names does not exist].
        /// </summary>
        [TestMethod]
        [TestCategory("Unity Container Manager tests")]
        public void CanNotRemoveUnityContainerFromManagerIfNamesDoesNotExist()
        {
            var unityContainerManager = UnityContainerManager.Instance;
            unityContainerManager.Add("test", new UnityContainer());
            var result = unityContainerManager.Get("test");
            Assert.IsNotNull(result);
            unityContainerManager.Remove("test_missing");
            result = unityContainerManager.Get("test_missing");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Removeses all unity containers from manager.
        /// </summary>
        [TestMethod]
        [TestCategory("Unity Container Manager tests")]
        public void RemovesAllUnityContainersFromManager()
        {
            var unityContainerManager = UnityContainerManager.Instance;
            unityContainerManager.Add("test1", new UnityContainer());
            unityContainerManager.Add("test2", new UnityContainer());
            unityContainerManager.RemoveAll();
            Assert.IsNotNull(unityContainerManager.Get("test1"));
            Assert.IsNotNull(unityContainerManager.Get("test2"));
        }
    }
}
