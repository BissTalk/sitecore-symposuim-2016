﻿using Sitecore.Pipelines;
using SymDemo.DependencyInjection;

namespace SymDemo.Pipeline.DependencyInjection
{
    /// <summary>
    /// A Sitecore pipeline processor designed for the application initialize to bootstrap the 
    /// </summary>
    public class StartUpUnityContainerManagerInitialization
    {
        private const string IOC_BOOTSTRAPPER_PIPELINE_NAME = "IocBootstrap";


        /// <summary>
        ///     Initialized the container in the Begin Pipeline
        /// </summary>
        public void Process(PipelineArgs args)
        {
            var containerManager = UnityContainerManager.UnityContainerManager.Instance;
            var container = containerManager?.Get();
            var newArgs = new IocContainerArgs(container);
            CorePipeline.Run(IOC_BOOTSTRAPPER_PIPELINE_NAME, newArgs, false);
            CopyOutputAgrsDataToInputArgs(args, newArgs);
        }

        private static void CopyOutputAgrsDataToInputArgs(PipelineArgs inputArgs, IocContainerArgs outputArgs)
        {
            inputArgs.CustomData.AddRange(outputArgs.CustomData);
        }
    }
}