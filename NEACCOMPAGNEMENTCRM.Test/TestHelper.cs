using Microsoft.Xrm.Sdk;
using NEACCOMPAGNEMENTCRM.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACCOMPAGNEMENTCRM.Test
{
    public class TestHelper : IDisposable
    {
        #region Members

        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private bool m_Disposed = false;

        /// <summary>
        /// The instance of XrmServiceContext.
        /// </summary>
        private XrmServiceContext m_xrmContext;

        /// <summary>
        /// The instance of IOrganizationService.
        /// </summary>
        private IOrganizationService m_orgService;

        /// <summary>
        /// Gets or sets the XRM service context.
        /// </summary>
        public XrmServiceContext XrmContext
        {
            get { return m_xrmContext; }
            set { m_xrmContext = value; }
        }

        /// <summary>
        /// Gets or sets the IOrganizationService.
        /// </summary>
        public IOrganizationService OrgService
        {
            get { return m_orgService; }
            set { m_orgService = value; }
        }

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestHelper"/> class.
        /// </summary>
        public TestHelper()
        {
            m_orgService = CrmHelper.CreateCrmWebService(ConfigurationManager.AppSettings[Constants.ConfigParameter_CrmUrl]);
            m_xrmContext = new XrmServiceContext(m_orgService);
        }

        #endregion Contructors

        #region Destructor

        /// <summary>
        /// Finalizes an instance of the <see cref="TestHelper" /> class.
        /// </summary>
        /// <remarks>
        /// SDK comment: It is important to properly dispose of the service proxy instance in your application before
        /// the application terminates. The using statement makes sure that the service proxy is correctly disposed by
        /// automatically calling Dispose on the service proxy when it goes out of scope. However, for improved
        /// application performance, it is a best practice to keep the service proxy instance available in your
        /// application for the entire application session instead of disposing it and allocating it again somewhere
        /// else in the application code when needed. The reason being that creating and authenticating a service channel
        /// is an expensive operation (in time). In this case, when you are done with the service proxy instance, you must
        /// directly call the Dispose method on the proxy before the application terminates.
        /// </remarks>
        ~TestHelper()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of readability and maintainability.
            Dispose(false);
        }

        #endregion Destructor

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.m_Disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    m_xrmContext.Dispose();
                }

                m_xrmContext = null;

                // Note disposing has been done.
                m_Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to take this object off the finalization queue
            // and prevent finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

    }
}
