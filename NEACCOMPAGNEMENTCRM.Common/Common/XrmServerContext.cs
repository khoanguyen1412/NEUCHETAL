namespace NEACCOMPAGNEMENTCRM.Common.Common
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// XrmServerContext class
    /// </summary>
    public class XrmServerContext
    {
        #region Constants
        #endregion Constants

        #region Members

        /// <summary>
        /// The Microsoft Dynamics 365 organization service.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "XrmServerContext")]
        public IOrganizationService OrganizationService { get; protected set; }

        /// <summary>
        /// DO NOT use it if you don't know what you're doing.
        /// The Microsoft Dynamics 365 organization service under SYSTEM context which by pass all security check.
        /// This doesn't work in workflow full trust mode. https://community.dynamics.com/crm/f/117/t/186310
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "XrmServerContext")]
        public IOrganizationService OrganizationServiceAdmin { get; protected set; }

        /// <summary>
        /// Provides logging run-time trace information for plug-ins. 
        /// Tracing only log to CRM in Sandbox mode which is viewable in plugin trace logs for user.
        /// </summary>
        public ITracingService TracingService { get; protected set; }

        /// <summary>
        /// ExecutionContext contains information that describes the run-time environment, information related to the execution pipeline, and entity business information.
        /// </summary>
        public IExecutionContext ExecutionContext { get; protected set; }

        /// <summary>
        /// Gets the XRM service context.
        /// </summary>
        public XrmServiceContext XrmServiceContext { get { return new XrmServiceContext(this.OrganizationService); } }

        #endregion Members

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XrmServerContext" /> class.
        /// </summary>
        public XrmServerContext()
        {
        }

        #endregion Contructors

        #region Properties
        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void Trace(string format, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(format) || this.TracingService == null)
            {
                return;
            }

            this.TracingService.Trace(format, args);
        }

        /// <summary>
        /// Retrieves the current entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnSet">The column set.</param>
        /// <returns></returns>
        public T RetrieveContextEntity<T>(ColumnSet columnSet) where T : Entity, new()
        {
            if (this.ExecutionContext != null)
            {
                return this.OrganizationService.Retrieve(
                    this.ExecutionContext.PrimaryEntityName,
                    this.ExecutionContext.PrimaryEntityId,
                    columnSet).ToEntity<T>();
            }

            return null;
        }

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods
        #endregion Private Methods

        #region Events
        #endregion Events
    }
}
