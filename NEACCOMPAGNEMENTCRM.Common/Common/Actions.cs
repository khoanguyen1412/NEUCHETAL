
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// Created via this command line: "C:\Users\khg\AppData\Roaming\MscrmTools\XrmToolBox\Plugins\DLaB.EarlyBoundGenerator\crmsvcutil.exe" /url:"http://bcv-crm:5555/test" /generateActions /out:"C:\Users\khg\Desktop\ReactJS\NEUCHETAL\NEACCOMPAGNEMENTCRM.Common\Common\Actions.cs" /namespace:"NEACCOMPAGNEMENTCRM.Common" /SuppressGeneratedCodeAttribute /codecustomization:"DLaB.CrmSvcUtilExtensions.Action.CustomizeCodeDomService,DLaB.CrmSvcUtilExtensions" /codegenerationservice:"DLaB.CrmSvcUtilExtensions.Action.CustomCodeGenerationService,DLaB.CrmSvcUtilExtensions" /codewriterfilter:"DLaB.CrmSvcUtilExtensions.Action.CodeWriterFilterService,DLaB.CrmSvcUtilExtensions" /metadataproviderservice:"DLaB.CrmSvcUtilExtensions.BaseMetadataProviderService,DLaB.CrmSvcUtilExtensions" 
//------------------------------------------------------------------------------

namespace NEACCOMPAGNEMENTCRM.Common
{
	
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("PvaCreateBotComponents")]
	public partial class PvaCreateBotComponentsRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public static class Fields
		{
			public const string BotComponents = "BotComponents";
			public const string Target = "Target";
		}
		
		public const string ActionLogicalName = "PvaCreateBotComponents";
		
		public Microsoft.Xrm.Sdk.EntityCollection BotComponents
		{
			get
			{
				if (this.Parameters.Contains("BotComponents"))
				{
					return ((Microsoft.Xrm.Sdk.EntityCollection)(this.Parameters["BotComponents"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.EntityCollection);
				}
			}
			set
			{
				this.Parameters["BotComponents"] = value;
			}
		}
		
		public Microsoft.Xrm.Sdk.EntityReference Target
		{
			get
			{
				if (this.Parameters.Contains("Target"))
				{
					return ((Microsoft.Xrm.Sdk.EntityReference)(this.Parameters["Target"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.EntityReference);
				}
			}
			set
			{
				this.Parameters["Target"] = value;
			}
		}
		
		public PvaCreateBotComponentsRequest()
		{
			this.RequestName = "PvaCreateBotComponents";
			this.BotComponents = default(Microsoft.Xrm.Sdk.EntityCollection);
			this.Target = default(Microsoft.Xrm.Sdk.EntityReference);
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("PvaCreateBotComponents")]
	public partial class PvaCreateBotComponentsResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public const string ActionLogicalName = "PvaCreateBotComponents";
		
		public PvaCreateBotComponentsResponse()
		{
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("PvaGetUserBots")]
	public partial class PvaGetUserBotsRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public static class Fields
		{
			public const string RoleType = "RoleType";
		}
		
		public const string ActionLogicalName = "PvaGetUserBots";
		
		public int RoleType
		{
			get
			{
				if (this.Parameters.Contains("RoleType"))
				{
					return ((int)(this.Parameters["RoleType"]));
				}
				else
				{
					return default(int);
				}
			}
			set
			{
				this.Parameters["RoleType"] = value;
			}
		}
		
		public PvaGetUserBotsRequest()
		{
			this.RequestName = "PvaGetUserBots";
			this.RoleType = default(int);
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("PvaGetUserBots")]
	public partial class PvaGetUserBotsResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public static class Fields
		{
			public const string Bots = "Bots";
		}
		
		public const string ActionLogicalName = "PvaGetUserBots";
		
		public PvaGetUserBotsResponse()
		{
		}
		
		public string Bots
		{
			get
			{
				if (this.Results.Contains("Bots"))
				{
					return ((string)(this.Results["Bots"]));
				}
				else
				{
					return default(string);
				}
			}
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("PvaGetFeatureControlSet")]
	public partial class PvaGetFeatureControlSetRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public const string ActionLogicalName = "PvaGetFeatureControlSet";
		
		public PvaGetFeatureControlSetRequest()
		{
			this.RequestName = "PvaGetFeatureControlSet";
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("PvaGetFeatureControlSet")]
	public partial class PvaGetFeatureControlSetResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public static class Fields
		{
			public const string FeatureControlSet = "FeatureControlSet";
		}
		
		public const string ActionLogicalName = "PvaGetFeatureControlSet";
		
		public PvaGetFeatureControlSetResponse()
		{
		}
		
		public string FeatureControlSet
		{
			get
			{
				if (this.Results.Contains("FeatureControlSet"))
				{
					return ((string)(this.Results["FeatureControlSet"]));
				}
				else
				{
					return default(string);
				}
			}
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("PvaGetDirectLineEndpoint")]
	public partial class PvaGetDirectLineEndpointRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public static class Fields
		{
			public const string Target = "Target";
		}
		
		public const string ActionLogicalName = "PvaGetDirectLineEndpoint";
		
		public Microsoft.Xrm.Sdk.EntityReference Target
		{
			get
			{
				if (this.Parameters.Contains("Target"))
				{
					return ((Microsoft.Xrm.Sdk.EntityReference)(this.Parameters["Target"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.EntityReference);
				}
			}
			set
			{
				this.Parameters["Target"] = value;
			}
		}
		
		public PvaGetDirectLineEndpointRequest()
		{
			this.RequestName = "PvaGetDirectLineEndpoint";
			this.Target = default(Microsoft.Xrm.Sdk.EntityReference);
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("PvaGetDirectLineEndpoint")]
	public partial class PvaGetDirectLineEndpointResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public static class Fields
		{
			public const string Endpoint = "Endpoint";
		}
		
		public const string ActionLogicalName = "PvaGetDirectLineEndpoint";
		
		public PvaGetDirectLineEndpointResponse()
		{
		}
		
		public string Endpoint
		{
			get
			{
				if (this.Results.Contains("Endpoint"))
				{
					return ((string)(this.Results["Endpoint"]));
				}
				else
				{
					return default(string);
				}
			}
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("PvaCreateContentSnapshot")]
	public partial class PvaCreateContentSnapshotRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public static class Fields
		{
			public const string Target = "Target";
		}
		
		public const string ActionLogicalName = "PvaCreateContentSnapshot";
		
		public Microsoft.Xrm.Sdk.EntityReference Target
		{
			get
			{
				if (this.Parameters.Contains("Target"))
				{
					return ((Microsoft.Xrm.Sdk.EntityReference)(this.Parameters["Target"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.EntityReference);
				}
			}
			set
			{
				this.Parameters["Target"] = value;
			}
		}
		
		public PvaCreateContentSnapshotRequest()
		{
			this.RequestName = "PvaCreateContentSnapshot";
			this.Target = default(Microsoft.Xrm.Sdk.EntityReference);
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("PvaCreateContentSnapshot")]
	public partial class PvaCreateContentSnapshotResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public static class Fields
		{
			public const string SnapshotId = "SnapshotId";
		}
		
		public const string ActionLogicalName = "PvaCreateContentSnapshot";
		
		public PvaCreateContentSnapshotResponse()
		{
		}
		
		public string SnapshotId
		{
			get
			{
				if (this.Results.Contains("SnapshotId"))
				{
					return ((string)(this.Results["SnapshotId"]));
				}
				else
				{
					return default(string);
				}
			}
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("PvaPublish")]
	public partial class PvaPublishRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public static class Fields
		{
			public const string Target = "Target";
		}
		
		public const string ActionLogicalName = "PvaPublish";
		
		public Microsoft.Xrm.Sdk.EntityReference Target
		{
			get
			{
				if (this.Parameters.Contains("Target"))
				{
					return ((Microsoft.Xrm.Sdk.EntityReference)(this.Parameters["Target"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.EntityReference);
				}
			}
			set
			{
				this.Parameters["Target"] = value;
			}
		}
		
		public PvaPublishRequest()
		{
			this.RequestName = "PvaPublish";
			this.Target = default(Microsoft.Xrm.Sdk.EntityReference);
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("PvaPublish")]
	public partial class PvaPublishResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public static class Fields
		{
			public const string PublishedBotContentId = "PublishedBotContentId";
		}
		
		public const string ActionLogicalName = "PvaPublish";
		
		public PvaPublishResponse()
		{
		}
		
		public string PublishedBotContentId
		{
			get
			{
				if (this.Results.Contains("PublishedBotContentId"))
				{
					return ((string)(this.Results["PublishedBotContentId"]));
				}
				else
				{
					return default(string);
				}
			}
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("PvaDeleteBot")]
	public partial class PvaDeleteBotRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public static class Fields
		{
			public const string Target = "Target";
		}
		
		public const string ActionLogicalName = "PvaDeleteBot";
		
		public Microsoft.Xrm.Sdk.EntityReference Target
		{
			get
			{
				if (this.Parameters.Contains("Target"))
				{
					return ((Microsoft.Xrm.Sdk.EntityReference)(this.Parameters["Target"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.EntityReference);
				}
			}
			set
			{
				this.Parameters["Target"] = value;
			}
		}
		
		public PvaDeleteBotRequest()
		{
			this.RequestName = "PvaDeleteBot";
			this.Target = default(Microsoft.Xrm.Sdk.EntityReference);
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("PvaDeleteBot")]
	public partial class PvaDeleteBotResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public const string ActionLogicalName = "PvaDeleteBot";
		
		public PvaDeleteBotResponse()
		{
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("PvaAuthorize")]
	public partial class PvaAuthorizeRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public static class Fields
		{
			public const string Users = "Users";
			public const string SharingRoleType = "SharingRoleType";
			public const string ShareOrUnshare = "ShareOrUnshare";
			public const string Target = "Target";
		}
		
		public const string ActionLogicalName = "PvaAuthorize";
		
		public Microsoft.Xrm.Sdk.EntityCollection Users
		{
			get
			{
				if (this.Parameters.Contains("Users"))
				{
					return ((Microsoft.Xrm.Sdk.EntityCollection)(this.Parameters["Users"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.EntityCollection);
				}
			}
			set
			{
				this.Parameters["Users"] = value;
			}
		}
		
		public Microsoft.Xrm.Sdk.OptionSetValue SharingRoleType
		{
			get
			{
				if (this.Parameters.Contains("SharingRoleType"))
				{
					return ((Microsoft.Xrm.Sdk.OptionSetValue)(this.Parameters["SharingRoleType"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.OptionSetValue);
				}
			}
			set
			{
				this.Parameters["SharingRoleType"] = value;
			}
		}
		
		public bool ShareOrUnshare
		{
			get
			{
				if (this.Parameters.Contains("ShareOrUnshare"))
				{
					return ((bool)(this.Parameters["ShareOrUnshare"]));
				}
				else
				{
					return default(bool);
				}
			}
			set
			{
				this.Parameters["ShareOrUnshare"] = value;
			}
		}
		
		public Microsoft.Xrm.Sdk.EntityReference Target
		{
			get
			{
				if (this.Parameters.Contains("Target"))
				{
					return ((Microsoft.Xrm.Sdk.EntityReference)(this.Parameters["Target"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.EntityReference);
				}
			}
			set
			{
				this.Parameters["Target"] = value;
			}
		}
		
		public PvaAuthorizeRequest()
		{
			this.RequestName = "PvaAuthorize";
			this.Users = default(Microsoft.Xrm.Sdk.EntityCollection);
			this.SharingRoleType = default(Microsoft.Xrm.Sdk.OptionSetValue);
			this.ShareOrUnshare = default(bool);
			this.Target = default(Microsoft.Xrm.Sdk.EntityReference);
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011//")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("PvaAuthorize")]
	public partial class PvaAuthorizeResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public const string ActionLogicalName = "PvaAuthorize";
		
		public PvaAuthorizeResponse()
		{
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011/depne/")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("depne_LinkIncidentToEmailSender")]
	public partial class depne_LinkIncidentToEmailSenderRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public static class Fields
		{
			public const string Target = "Target";
		}
		
		public const string ActionLogicalName = "depne_LinkIncidentToEmailSender";
		
		public Microsoft.Xrm.Sdk.EntityReference Target
		{
			get
			{
				if (this.Parameters.Contains("Target"))
				{
					return ((Microsoft.Xrm.Sdk.EntityReference)(this.Parameters["Target"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.EntityReference);
				}
			}
			set
			{
				this.Parameters["Target"] = value;
			}
		}
		
		public depne_LinkIncidentToEmailSenderRequest()
		{
			this.RequestName = "depne_LinkIncidentToEmailSender";
			this.Target = default(Microsoft.Xrm.Sdk.EntityReference);
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011/depne/")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("depne_LinkIncidentToEmailSender")]
	public partial class depne_LinkIncidentToEmailSenderResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public const string ActionLogicalName = "depne_LinkIncidentToEmailSender";
		
		public depne_LinkIncidentToEmailSenderResponse()
		{
		}
	}
}
