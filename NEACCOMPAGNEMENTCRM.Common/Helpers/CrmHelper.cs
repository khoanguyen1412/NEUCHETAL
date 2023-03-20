// -----------------------------------------------------------------------
// <copyright file="CrmHelper.cs" company="ELCA">
//      Copyright (c) ELCA Informatique SA Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace NEACCOMPAGNEMENTCRM.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using Microsoft.Crm.Sdk.Messages;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Client;
    using Microsoft.Xrm.Sdk.Messages;
    using Microsoft.Xrm.Sdk.Metadata;
    using Microsoft.Xrm.Sdk.Query;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// The CRM Helper class.
    /// </summary>
    public static class CrmHelper
    {
        public static IOrganizationService CreateCrmWebService(string crmUrl)
        {
            try
            {
                OrganizationServiceProxy serviceProxy;
                IOrganizationService service;

                Uri org = new Uri(crmUrl);
                ClientCredentials credentials = new ClientCredentials();
                credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

                using (serviceProxy = new OrganizationServiceProxy(org, null, credentials, null))
                {
                    serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());
                    //serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Address = new EndpointAddress(org);
                    serviceProxy.Timeout = new TimeSpan(0, 20, 0);
                    service = (IOrganizationService)serviceProxy;
                    return service;
                }
            }
            catch (Exception e)
            {
                throw new Exception(Constants.ErrorMessage_CannotCreateCrmWebService + e.Message);
            }
        }
    }
}