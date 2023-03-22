namespace NEACCOMPAGNEMENTCRM.Test.Email
{
    using NEACCOMPAGNEMENTCRM.Common;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class EmailSenderLinkedToIncidentTests : BaseTest
    {
        #region Test Method

        [Test]
        public void TestIncidentCreatedByEmailIsLinkedToEmailSender()
        {
            using (var testHelper = new TestHelper())
            {
                var context = testHelper.XrmContext;
                var service = testHelper.OrgService;

                var sender = context.SystemUserSet.Where(user => user.DomainName == "ELCADEV\\Test001").FirstOrDefault();
                var toRef = context.depne_contactSet.FirstOrDefault(con => con.Id == new Guid("a04de377-7dc8-ed11-a885-0050569fda1a")).ToEntityReference();

                ActivityParty[] fromParty = new ActivityParty[] { new ActivityParty() { PartyId = sender.ToEntityReference() } };
                ActivityParty[] toParty = new ActivityParty[] { new ActivityParty() { PartyId = toRef } };
                // Case 1: email is created and there is no record sender found
                var email1 = new Email()
                {
                    Subject = UNIT_TEST_NAME + NAME_A,
                    From = fromParty,
                    To = toParty
                };

                context.AddObject(email1);
                context.SaveChanges();

                Thread.Sleep(15000);
                context.ClearChanges();
                var retrievedEmail1 = context.EmailSet.FirstOrDefault(email => email.Id == email1.Id);
                Assert.IsNotNull(retrievedEmail1.RegardingObjectId);
                Assert.AreEqual(Incident.EntityLogicalName, retrievedEmail1.RegardingObjectId.LogicalName);

                var incident1 = context.IncidentSet.FirstOrDefault(incident => incident.Id == retrievedEmail1.RegardingObjectId.Id);
                Assert.IsNotNull(incident1.depne_Type);
                Assert.AreEqual(Incident_depne_Type.PopulationInstitution, incident1.depne_Type);
                Assert.IsNull(incident1.depne_Professioneldesante);
                Assert.IsNull(incident1.depne_Responsabledemande);
                Assert.IsNull(incident1.depne_Contactdemande);

                // Case 2: email is created and there is 1 record sender found of "depne_contact"
                var contact = new Contact()
                {
                    FirstName = UNIT_TEST_NAME + NAME_B,
                    EMailAddress1 = sender.InternalEMailAddress
                };

                context.AddObject(contact);
                context.SaveChanges();

                var email2 = new Email()
                {
                    Subject = UNIT_TEST_NAME + NAME_B,
                    From = fromParty,
                    To = toParty
                };

                context.AddObject(email2);
                context.SaveChanges();

                Thread.Sleep(15000);
                context.ClearChanges();
                var retrievedEmail2 = context.EmailSet.FirstOrDefault(email => email.Id == email2.Id);
                Assert.IsNotNull(retrievedEmail2.RegardingObjectId);
                Assert.AreEqual(Incident.EntityLogicalName, retrievedEmail2.RegardingObjectId.LogicalName);

                var incident2 = context.IncidentSet.FirstOrDefault(incident => incident.Id == retrievedEmail2.RegardingObjectId.Id);
                Assert.IsNotNull(incident2.depne_Type);
                Assert.AreEqual(Incident_depne_Type.PopulationInstitution, incident2.depne_Type);
                Assert.IsNotNull(incident2.CustomerId);
                Assert.AreEqual(contact.Id, incident2.CustomerId.Id);
                Assert.IsNull(incident1.depne_Professioneldesante);
                Assert.IsNull(incident1.depne_Responsabledemande);
                Assert.IsNull(incident1.depne_Contactdemande);

                service.Delete(Contact.EntityLogicalName, contact.Id);

                // Case 3: email is created and there are 2 records of sender found:
                // "Professionnel de santé" and "Responsable" => take "Professionnel de santé" record
                var professionnel = new depne_professionelshp()
                {
                    depne_Nom = UNIT_TEST_NAME + NAME_C,
                    depne_Email = sender.InternalEMailAddress
                };

                var responsable = new depne_responsables()
                {
                    depne_Nom = UNIT_TEST_NAME + NAME_C,
                    EmailAddress = sender.InternalEMailAddress
                };

                context.AddObject(professionnel);
                context.AddObject(responsable);
                context.SaveChanges();

                var email3 = new Email()
                {
                    Subject = UNIT_TEST_NAME + NAME_C,
                    From = fromParty,
                    To = toParty
                };

                context.AddObject(email3);
                context.SaveChanges();

                Thread.Sleep(15000);
                context.ClearChanges();
                var retrievedEmail3 = context.EmailSet.FirstOrDefault(email => email.Id == email3.Id);
                Assert.IsNotNull(retrievedEmail3.RegardingObjectId);
                Assert.AreEqual(Incident.EntityLogicalName, retrievedEmail3.RegardingObjectId.LogicalName);

                var incident3 = context.IncidentSet.FirstOrDefault(incident => incident.Id == retrievedEmail3.RegardingObjectId.Id);
                Assert.IsNotNull(incident3.depne_Type);
                Assert.AreEqual(Incident_depne_Type.Professionnel, incident3.depne_Type);
                Assert.IsNotNull(incident3.depne_Professioneldesante);
                Assert.AreEqual(professionnel.Id, incident3.depne_Professioneldesante.Id);
                Assert.IsNull(incident1.depne_Responsabledemande);
                Assert.IsNull(incident1.depne_Contactdemande);
            }
        }

        #endregion
    }
}
