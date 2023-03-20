using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using NEACCOMPAGNEMENTCRM.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACCOMPAGNEMENTCRM.BATCH.SynchroPopulation
{
    class Program
    {
        #region Member
        // Path
        static private string m_importFolderPath;
        static private string m_archiveFolderPath;
        static private string m_logFolderPath;
        static private int m_chunkSize = 300;
        public static IOrganizationService m_orgService { get; set; }
        public static CrmServiceContext m_xrmContext { get; set; }
        // Field
        // Constant

        #endregion

        static void Main(string[] args)
        {
            InitData();
            LogHelper.LogInformation("==========================================================");
            LogHelper.LogInformation("START SYNCHRO POPULATION BATCH");
            SynchronizeDataToCrm();
            LogHelper.LogInformation("END SYNCHRO POPULATION BATCH");
        }

        #region Private Methods
        static public void InitData()
        {
            // Get folder path
            m_importFolderPath = ConfigurationManager.AppSettings[Constants.ConfigParameter_FolderImport];
            m_archiveFolderPath = ConfigurationManager.AppSettings[Constants.ConfigParameter_FolderArchive];
            m_logFolderPath = ConfigurationManager.AppSettings[Constants.ConfigParameter_FolderLogs];
            // Get Log File
            string infoLogName = ConfigurationManager.AppSettings[Constants.ConfigParameter_InformationLogName];
            LogHelper.InitLogHelper(infoLogName, m_logFolderPath, true);
            //create service
            m_orgService = CrmHelper.CreateCrmWebService(ConfigurationManager.AppSettings[Constants.ConfigParameter_CrmUrl]);
            m_xrmContext = new CrmServiceContext(m_orgService);
        }

        private static void SynchronizeDataToCrm()
        {
            try
            {
                CreateArchiveFolder();
                ImportDataToCRM();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("Exception when synchronize Data : {0}", ex.Message));
            }
        }

        private static void CreateArchiveFolder()
        {
            try
            {
                if (!Directory.Exists(m_archiveFolderPath)) 
                { 
                    Directory.CreateDirectory(m_archiveFolderPath); 
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("Exception when creating archive folder : {0}", ex.Message));
                throw ex;
            }
        }

        private static void ImportDataToCRM()
        {
            try
            {
                List<string> importFiles = Directory.GetFiles(m_importFolderPath, Constants.CSVFileType).ToList();
                if (!importFiles.Any())
                {
                    LogHelper.LogInformation("There is no file to import");
                    return;
                }

                foreach (var file in importFiles)
                {
                    try
                    {
                        ImportContactToCRM(file);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError(string.Format("Exception when processing file: {0}, message: {1}", Path.GetFileName(file), ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("Exception when getting files in Folder : {0}", ex.Message));
                throw ex;
            }
        }

        private static void ImportContactToCRM(string file)
        {
            var fileName = Path.GetFileName(file);
            LogHelper.LogInformation(String.Format("Start Importing file: {0}", fileName));
            List<string> records = LoadRecordsFromCsv(file);

            if (!records.Any())
            {
                LogHelper.LogInformation("There is no record to import");
                return;
            }

            int totalRecords = records.Count;
            int countImportSuccess = 0;
            int countImportFail = 0;
            LogHelper.LogInformation(String.Format("Number of records to process: {0}", totalRecords));

            OrganizationRequestCollection lstReq = new OrganizationRequestCollection();
            foreach (var record in records)
            {
                var columns = record.Split(Constants.SEMICOLON).ToList();
                var contact = CreateContactFromCSVRecord(columns);
                var retrievedContact = m_xrmContext.ContactSet.Where(con => con.depne_NBDP == contact.depne_NBDP).FirstOrDefault();
                if (retrievedContact == null)
                {
                    //create
                    contact.depne_IsPAT = true;
                    contact.depne_Etat_PAT = Contact_depne_Etat_PAT.BDP;
                    lstReq.Add(new CreateRequest()
                    {
                        Target = contact
                    });
                }
                else
                {
                    //update
                    contact.Id = retrievedContact.Id;
                    lstReq.Add(new UpdateRequest()
                    {
                        Target = contact
                    });
                }
            }

            var lstFailedMessage = ExecuteMultiRequest(lstReq);
            countImportFail += lstFailedMessage.Count;
            countImportSuccess += (records.Count() - lstFailedMessage.Count);

            LogHelper.LogInformation(String.Format("Number of records imported successfully: {0}", countImportSuccess));
            LogHelper.LogInformation(String.Format("Number of records imported fail: {0}", countImportFail));

            if (countImportFail > 0)
            {
                LogHelper.LogError("Fail messages:");
                foreach(var failMessage in lstFailedMessage)
                {
                    LogHelper.LogError("    " + failMessage);
                }
            }

            // Move file
            string archivedFile = Path.Combine(m_archiveFolderPath, fileName);
            File.Move(file, archivedFile);
        }

        private static List<string> ExecuteMultiRequest(OrganizationRequestCollection requestCol)
        {
            List<string> lstFailed = new List<string>();

            for (var index = 0; index < requestCol.Count; index += m_chunkSize)
            {
                var chunkRequests = requestCol.Skip(index).Take(m_chunkSize);

                ExecuteMultipleRequest requestWithResults = new ExecuteMultipleRequest()
                {
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = true,
                        ReturnResponses = true
                    },
                    Requests = new OrganizationRequestCollection()
                };

                requestWithResults.Requests.AddRange(chunkRequests);

                ExecuteMultipleResponse responseWithResults = m_orgService.Execute(requestWithResults) as ExecuteMultipleResponse;

                // Log if error
                foreach (var responseItem in responseWithResults.Responses)
                {
                    if (responseItem.Fault != null)
                    {
                        var errorIndex = responseItem.RequestIndex;
                        var requestItem = chunkRequests.ElementAt(errorIndex);
                        var targetContact = (Contact)(((OrganizationRequest)requestItem)[Constants.Request_Target]);
                        var fault = string.Format("Data: [ColA,ColC,ColD]=[{0},{1},{2}], Fail message: {3}",
                            targetContact.depne_NBDP,
                            targetContact.LastName,
                            targetContact.FirstName,
                            responseItem.Fault.Message);

                        lstFailed.Add(fault);
                    }
                }
            }

            return lstFailed;
        }

        private static Contact CreateContactFromCSVRecord(List<string> columns)
        {
            var contact = new Contact();
            
            //check if this unique field is empty?
            if (!string.IsNullOrEmpty(columns[0]))
            {
                contact.depne_NBDP = columns[0];
            }

            if (!string.IsNullOrEmpty(columns[1]))
            {
                contact.depne_Codepolitesse = columns[1] == Constants.CONTACT_CODE_POLITESSE ? Contact_depne_Codepolitesse.Madame : Contact_depne_Codepolitesse.Monsieur;
            }

            if (!string.IsNullOrEmpty(columns[2]))
            {
                contact.LastName = columns[2];
            }

            if (!string.IsNullOrEmpty(columns[3]))
            {
                contact.FirstName = columns[3];
            }

            //check datetime is valid?
            if (!string.IsNullOrEmpty(columns[4]))
            {
                contact.BirthDate = DateTime.ParseExact(columns[4], Constants.DATE_FORMAT_SEPARATOR_DOT, CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(columns[5]))
            {
                contact.GenderCode = columns[5].ToLower() == Constants.GENDER_FEMME ? Contact_GenderCode.Female : Contact_GenderCode.Male;
            }

            if (!string.IsNullOrEmpty(columns[6]))
            {
                contact.Address1_Line2 = columns[6];
            }

            if (!string.IsNullOrEmpty(columns[7]))
            {
                contact.Address1_Line1 = columns[7];
            }

            if (!string.IsNullOrEmpty(columns[8]))
            {
                contact.Address1_PostalCode = columns[8];
            }

            if (!string.IsNullOrEmpty(columns[9]))
            {
                contact.Address1_City = columns[9];
            }

            if (!string.IsNullOrEmpty(columns[10]))
            {
                contact.Address1_Country = columns[10];
            }

            return contact;
        }

        private static List<string> LoadRecordsFromCsv(string fileName)
        {
            try
            {
                var lines = File.ReadAllLines(fileName, Encoding.Default);
                return lines.Where(line => !string.IsNullOrEmpty(line)).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("Exception when loading records from csv file {0} : {1}", fileName, ex.Message));
                return new List<string>();
            }
        }

        #endregion
    }
}
