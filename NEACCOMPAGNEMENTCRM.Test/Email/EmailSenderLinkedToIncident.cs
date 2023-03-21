namespace NEACCOMPAGNEMENTCRM.Test.Email
{
    using NEACCOMPAGNEMENTCRM.Common;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class EmailSenderLinkedToIncident : BaseTest
    {
        #region Test Method

        [Test]
        public void TestIncidentCreatedByEmailIsLinkedToEmailSender()
        {
            using (var testHelper = new TestHelper())
            {
                var context = testHelper.XrmContext;
                var service = testHelper.OrgService;

                var sender1 = "user1@elca.vn";
                var sender2 = "user2@elca.vn";
                var sender3 = "user3@elca.vn";

                // Case 1: email is created and there is no record sender in CRM
                var email1 = new Email()
                {
                    Subject = UNIT_TEST_NAME + NAME_A,
                    Sender = sender1
                };

                context.AddObject(email1);
                context.SaveChanges();

                var retrievedEmail1 = context.EmailSet.FirstOrDefault(email => email.Id == email1.Id);
                Assert.IsNotNull(retrievedEmail1.RegardingObjectId);
                Assert.AreEqual(Incident.EntityLogicalName, retrievedEmail1.RegardingObjectId.LogicalName);

                var incident1 = context.IncidentSet.FirstOrDefault(incident => incident.Id == retrievedEmail1.RegardingObjectId.Id);
                Assert.IsNull(incident1.depne_Type);

                // Case 2: email is created and there is 1 record sender found of "depne_contact"
                var depContact = new depne_contact()
                {
                    depne_name = UNIT_TEST_NAME + NAME_B,
                    EmailAddress = sender2
                };

                context.AddObject(depContact);
                context.SaveChanges();

                var email2 = new Email()
                {
                    Subject = UNIT_TEST_NAME + NAME_B,
                    Sender = sender2
                };

                context.AddObject(email2);
                context.SaveChanges();

                var retrievedEmail2 = context.EmailSet.FirstOrDefault(email => email.Id == email2.Id);
                Assert.IsNotNull(retrievedEmail2.RegardingObjectId);
                Assert.AreEqual(Incident.EntityLogicalName, retrievedEmail2.RegardingObjectId.LogicalName);

                var incident2 = context.IncidentSet.FirstOrDefault(incident => incident.Id == retrievedEmail2.RegardingObjectId.Id);
                Assert.IsNotNull(incident2.depne_Type);
                Assert.AreEqual(Incident_depne_Type.Contact, incident2.depne_Type);
                Assert.IsNotNull(incident2.depne_Contactdemande);
                Assert.AreEqual(depContact.Id, incident2.depne_Contactdemande.Id);

                // Case 3: email is created and there are 2 records of sender found:
                // "Professionnel de santé" and "Responsable" => take "Professionnel de santé" record
                var professionnel = new depne_professionelshp()
                {
                    depne_name = UNIT_TEST_NAME + NAME_C,
                    depne_Email = sender3
                };

                var responsable = new depne_contact()
                {
                    depne_name = UNIT_TEST_NAME + NAME_C,
                    EmailAddress = sender3
                };

                context.AddObject(professionnel);
                context.AddObject(responsable);
                context.SaveChanges();

                var email3 = new Email()
                {
                    Subject = UNIT_TEST_NAME + NAME_C,
                    Sender = sender3
                };

                context.AddObject(email3);
                context.SaveChanges();

                var retrievedEmail3 = context.EmailSet.FirstOrDefault(email => email.Id == email3.Id);
                Assert.IsNotNull(retrievedEmail3.RegardingObjectId);
                Assert.AreEqual(Incident.EntityLogicalName, retrievedEmail3.RegardingObjectId.LogicalName);

                var incident3 = context.IncidentSet.FirstOrDefault(incident => incident.Id == retrievedEmail3.RegardingObjectId.Id);
                Assert.IsNotNull(incident3.depne_Type);
                Assert.AreEqual(Incident_depne_Type.Professionnel, incident3.depne_Type);
                Assert.IsNotNull(incident3.depne_Contactdemande);
                Assert.AreEqual(depContact.Id, incident3.depne_Contactdemande.Id);
            }
        }

        #endregion
    }
}
