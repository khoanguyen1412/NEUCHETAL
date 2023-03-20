using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACCOMPAGNEMENTCRM.Common
{
    public static class Constants
    {
        #region Log4Net Config

        /// <summary>
        /// Log4Net Registry Key Name 
        /// </summary>
        public const string Log4NetRegistryKeyName = "";

        /// <summary>
        /// Log4Net Registry Value Name
        /// </summary>
        public const string Log4NetRegistryValueName = "";

        /// <summary>
        /// Absolute path of log4Net's config file  when no configuration found in registry.
        /// </summary>
        public const string Log4NetConfigDefaultFilePath = "";

        /// <summary>
        /// The default log pattern layout used by default log instance.
        /// </summary>
        public const string DefaultLogPatternLayout = "%-5p %d %5rms %-22.22c{1} %-18.18M - %m%n";

        /// <summary>
        /// Soap Exception's format: message - detail inner text
        /// </summary>
        public const string SoapExceptionFormat = "{0} {1}";

        public const string ConfigParameter_DefaultLogName = "SynchronizePopulationLog.log";

        #endregion

        #region log constants
        public const char SeparateChar = ';';
        public const string CSVFileType = "*.csv";
        public const string ConfigParameter_FolderImport = "$Folder_import";
        public const string ConfigParameter_FolderArchive = "$Folder_archive";
        public const string ConfigParameter_FolderLogs = "$Folder_logs";
        //public const string ConfigParameter_ActiveLog = "ActiveLog";
        public const string ConfigParameter_InformationLogName = "LogFileName";
        #endregion

        #region configuration constants
        public const string ConfigParameter_CrmUrl = "CRMURL";
        #endregion

        /// <summary>
        /// The error message when cannot create CRM web service
        /// </summary>
        public const string ErrorMessage_CannotCreateCrmWebService = "Error when create CRM web service: ";

        public const char SEMICOLON = ';';
        public const string GENDER_HOMME = "homme"; //male
        public const string GENDER_FEMME = "femme"; //female
        public const string DATE_FORMAT_SEPARATOR_DOT = "dd.MM.yyyy";
        public const string CONTACT_CODE_POLITESSE = "Madame";
        public const string Request_Target = "Target";
    }
}
