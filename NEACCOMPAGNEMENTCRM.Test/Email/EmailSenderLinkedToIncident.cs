using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACCOMPAGNEMENTCRM.Test.Email
{
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

                
            }
        }

        #endregion
    }
}
