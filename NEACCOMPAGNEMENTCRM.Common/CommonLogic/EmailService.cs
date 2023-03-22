namespace NEACCOMPAGNEMENTCRM.Common
{
    using Microsoft.Xrm.Sdk;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class EmailService
    {
        #region Constants
        #endregion

        #region Public Methods
        public static void LinkIncidentToEmailSender(IOrganizationService service, EntityReference emailRef)
        {
            using(var xrmContext = new XrmServiceContext(service))
            {
                var targetEmail = xrmContext.EmailSet.FirstOrDefault(email => email.Id == emailRef.Id);
                var incidentRef = targetEmail.RegardingObjectId;
                var emailSender = targetEmail.Sender;

                var emailSenderRef = FindEmailSender(xrmContext, emailSender);
                if (emailSenderRef == null)
                {
                    return;
                }

                var updatingIncident = new Incident();
                updatingIncident.Id = incidentRef.Id;

                switch (emailSenderRef.LogicalName)
                {
                    case Contact.EntityLogicalName:
                        updatingIncident.depne_Type = Incident_depne_Type.PopulationInstitution;
                        updatingIncident.CustomerId = emailSenderRef;
                        break;
                    case depne_professionelshp.EntityLogicalName:
                        updatingIncident.depne_Type = Incident_depne_Type.Professionnel;
                        updatingIncident.depne_Professioneldesante = emailSenderRef;
                        break;
                    case depne_responsables.EntityLogicalName:
                        updatingIncident.depne_Type = Incident_depne_Type.Responsable;
                        updatingIncident.depne_Responsabledemande = emailSenderRef;
                        break;
                    default:
                        updatingIncident.depne_Type = Incident_depne_Type.Contact;
                        updatingIncident.depne_Contactdemande = emailSenderRef;
                        break;
                }

                service.Update(updatingIncident);
            }
        }

        #endregion

        #region Private Methods
        private static EntityReference FindEmailSender(XrmServiceContext xrmContext, string emailSender)
        {
            var contactSender = xrmContext.ContactSet.FirstOrDefault(con => con.EMailAddress1 == emailSender);
            if (contactSender != null)
            {
                return contactSender.ToEntityReference();
            }

            var professionnelSender = xrmContext.depne_professionelshpSet.FirstOrDefault(pro => pro.depne_Email == emailSender);
            if (professionnelSender != null)
            {
                return professionnelSender.ToEntityReference();
            }

            var responsablesSender = xrmContext.depne_responsablesSet.FirstOrDefault(res => res.EmailAddress == emailSender);
            if (responsablesSender != null)
            {
                return responsablesSender.ToEntityReference();
            }

            var depContactSender = xrmContext.depne_contactSet.FirstOrDefault(res => res.EmailAddress == emailSender);
            if (depContactSender != null)
            {
                return depContactSender.ToEntityReference();
            }

            return null;
        }
        #endregion
    }
}
