namespace NEACCOMPAGNEMENTCRM.Plugins
{
    using Microsoft.Xrm.Sdk;
    using System;
    using NEACCOMPAGNEMENTCRM.Common.Common;

    public class LocalPluginContext : XrmServerContext
    {
        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "LocalPluginContext")]
        internal IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// IPluginExecutionContext contains information that describes the run-time environment in which the plug-in executes, information related to the execution pipeline, and entity business information.
        /// </summary>
        internal IPluginExecutionContext PluginExecutionContext { get; private set; }

        /// <summary>
        /// Synchronous registered plug-ins can post the execution context to the Microsoft Azure Service Bus. <br/> 
        /// It is through this notification service that synchronous plug-ins can send brokered messages to the Microsoft Azure Service Bus.
        /// </summary>
        internal IServiceEndpointNotificationService NotificationService { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalPluginContext" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <exception cref="System.ArgumentNullException">serviceProvider</exception>
        public LocalPluginContext(IServiceProvider serviceProvider) : base()
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            // Set service provider
            this.ServiceProvider = serviceProvider;
            this.Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal void Initialize()
        {
            // Obtain the execution context service from the service provider.
            this.PluginExecutionContext = (IPluginExecutionContext)this.ServiceProvider.GetService(typeof(IPluginExecutionContext));
            this.ExecutionContext = this.PluginExecutionContext;

            // Obtain the tracing service from the service provider.
            this.TracingService = (ITracingService)this.ServiceProvider.GetService(typeof(ITracingService));

            // Get the notification service from the service provider.
            this.NotificationService = (IServiceEndpointNotificationService)this.ServiceProvider.GetService(typeof(IServiceEndpointNotificationService));

            // Obtain the Organization Service factory service from the service provider
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)this.ServiceProvider.GetService(typeof(IOrganizationServiceFactory));

            // Use the factory to generate the Organization Service.
            this.OrganizationService = factory.CreateOrganizationService(this.PluginExecutionContext.UserId);
            this.OrganizationServiceAdmin = factory.CreateOrganizationService(null);
        }
    }
}

