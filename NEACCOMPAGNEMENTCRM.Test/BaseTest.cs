namespace NEACCOMPAGNEMENTCRM.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Messages;
    using NUnit.Framework;

    /// <summary>
    /// The test base class.
    /// </summary>
    [TestFixture]
    public class BaseTest
    {

        public const string UNIT_TEST_NAME = "UNIT_TEST_NAME";
        public const string NAME_A = "NAME A";
        public const string NAME_B = "NAME B";
        public const string NAME_C = "NAME C";
        
        public BaseTest()
        {
        }

        /// <summary>
        /// test cleanup.
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            using (TestHelper testHelper = new TestHelper())
            {
                List<OrganizationRequest> requests = new List<OrganizationRequest>();

                // Remove email
                foreach (var record in testHelper.XrmContext.EmailSet.Where(p => p.Subject != null && p.Subject.Contains(UNIT_TEST_NAME)))
                {
                    DeleteRequest deleteReq = new DeleteRequest()
                    {
                        Target = record.ToEntityReference()
                    };
                    requests.Add(deleteReq);
                }

                // Execute delete requests
                ExecuteMultipleRequest executeMultipleRequest = new ExecuteMultipleRequest
                {
                    Settings =
                        new ExecuteMultipleSettings
                        {
                            ContinueOnError = true,
                            ReturnResponses = false
                        },
                    Requests = new OrganizationRequestCollection()
                };
                executeMultipleRequest.Requests.AddRange(requests);
                testHelper.OrgService.Execute(executeMultipleRequest);
            }
        }     
    }
}
