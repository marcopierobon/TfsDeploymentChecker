using System;

namespace TfsDeploymentChecker.Models.Tfs
{
    public class ReleaseDefinition
    {
        public string source { get; set; }
        public int revision { get; set; }
        public object description { get; set; }
        public Createdby createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public Modifiedby modifiedBy { get; set; }
        public DateTime modifiedOn { get; set; }
        public bool isDeleted { get; set; }
        public Variables variables { get; set; }
        public object[] variableGroups { get; set; }
        public Environment[] environments { get; set; }
        public Artifact[] artifacts { get; set; }
        public Trigger[] triggers { get; set; }
        public string releaseNameFormat { get; set; }
        public object[] tags { get; set; }
        public Pipelineprocess pipelineProcess { get; set; }
        public Properties properties { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public object projectReference { get; set; }
        public string url { get; set; }
        public _Links2 _links { get; set; }
    }

    public class Createdby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links
    {
        public Avatar avatar { get; set; }
    }

    public class Avatar
    {
        public string href { get; set; }
    }

    public class Modifiedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links1 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links1
    {
        public Avatar1 avatar { get; set; }
    }

    public class Avatar1
    {
        public string href { get; set; }
    }

    public class Variables
    {
        public Artifactname ArtifactName { get; set; }
        public Browsers Browsers { get; set; }
        public Cachesize CacheSize { get; set; }
        public Channel Channel { get; set; }
        public Consumer_Loggin_Minimum_Level Consumer_loggin_minimum_level { get; set; }
        public Deploydomain DeployDomain { get; set; }
        public Deploypassword DeployPassword { get; set; }
        public Deployuser DeployUser { get; set; }
        public Healthchecksettingsapikey HealthCheckSettingsApiKey { get; set; }
        public Origin_Loggin_Minimum_Level Origin_loggin_minimum_level { get; set; }
        public Rabbitadminpass RabbitAdminPass { get; set; }
        public Rabbitadminuser RabbitAdminUser { get; set; }
        public Rulebookbaseurl RulebookBaseURL { get; set; }
        public SystemDebug SystemDebug { get; set; }
        public Workflowrulebookbaseurl WorkflowRulebookBaseURL { get; set; }
        public Workflowrulebookpassword WorkflowRulebookPassword { get; set; }
        public Workflowrulebookusername WorkflowRulebookUsername { get; set; }
    }

    public class Artifactname
    {
        public string value { get; set; }
    }

    public class Browsers
    {
        public string value { get; set; }
    }

    public class Cachesize
    {
        public string value { get; set; }
    }

    public class Channel
    {
        public string value { get; set; }
    }

    public class Consumer_Loggin_Minimum_Level
    {
        public string value { get; set; }
    }

    public class Deploydomain
    {
        public string value { get; set; }
    }

    public class Deploypassword
    {
        public object value { get; set; }
        public bool isSecret { get; set; }
    }

    public class Deployuser
    {
        public string value { get; set; }
    }

    public class Healthchecksettingsapikey
    {
        public string value { get; set; }
    }

    public class Origin_Loggin_Minimum_Level
    {
        public string value { get; set; }
    }

    public class Rabbitadminpass
    {
        public object value { get; set; }
        public bool isSecret { get; set; }
    }

    public class Rabbitadminuser
    {
        public string value { get; set; }
    }

    public class Rulebookbaseurl
    {
        public string value { get; set; }
    }

    public class SystemDebug
    {
        public string value { get; set; }
    }

    public class Workflowrulebookbaseurl
    {
        public string value { get; set; }
    }

    public class Workflowrulebookpassword
    {
        public string value { get; set; }
    }

    public class Workflowrulebookusername
    {
        public string value { get; set; }
    }

    public class Pipelineprocess
    {
        public string type { get; set; }
    }

    public class Properties
    {
        public SystemEnvironmentranklogicversion SystemEnvironmentRankLogicVersion { get; set; }
    }

    public class SystemEnvironmentranklogicversion
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class _Links2
    {
        public Self self { get; set; }
        public Web web { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Web
    {
        public string href { get; set; }
    }

    public class Environment
    {
        public int id { get; set; }
        public string name { get; set; }
        public int rank { get; set; }
        public Owner owner { get; set; }
        public object variables { get; set; }
        public object[] variableGroups { get; set; }
        public Predeployapprovals preDeployApprovals { get; set; }
        public Deploystep deployStep { get; set; }
        public Postdeployapprovals postDeployApprovals { get; set; }
        public Deployphas[] deployPhases { get; set; }
        public Environmentoptions environmentOptions { get; set; }
        public object[] demands { get; set; }
        public Condition[] conditions { get; set; }
        public Executionpolicy executionPolicy { get; set; }
        public object[] schedules { get; set; }
        public Currentrelease currentRelease { get; set; }
        public Retentionpolicy retentionPolicy { get; set; }
        public Processparameters processParameters { get; set; }
        public Properties1 properties { get; set; }
        public Predeploymentgates preDeploymentGates { get; set; }
        public Postdeploymentgates postDeploymentGates { get; set; }
        public object[] environmentTriggers { get; set; }
        public string badgeUrl { get; set; }
    }

    public class Owner
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links3 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links3
    {
        public Avatar2 avatar { get; set; }
    }

    public class Avatar2
    {
        public string href { get; set; }
    }

    public class Predeployapprovals
    {
        public Approval[] approvals { get; set; }
        public Approvaloptions approvalOptions { get; set; }
    }

    public class Approvaloptions
    {
        public object requiredApproverCount { get; set; }
        public bool releaseCreatorCanBeApprover { get; set; }
        public bool autoTriggeredAndPreviousEnvironmentApprovedCanBeSkipped { get; set; }
        public bool enforceIdentityRevalidation { get; set; }
        public int timeoutInMinutes { get; set; }
        public string executionOrder { get; set; }
    }

    public class Approval
    {
        public int rank { get; set; }
        public bool isAutomated { get; set; }
        public bool isNotificationOn { get; set; }
        public int id { get; set; }
    }

    public class Deploystep
    {
        public int id { get; set; }
    }

    public class Postdeployapprovals
    {
        public Approval1[] approvals { get; set; }
        public Approvaloptions1 approvalOptions { get; set; }
    }

    public class Approvaloptions1
    {
        public object requiredApproverCount { get; set; }
        public bool releaseCreatorCanBeApprover { get; set; }
        public bool autoTriggeredAndPreviousEnvironmentApprovedCanBeSkipped { get; set; }
        public bool enforceIdentityRevalidation { get; set; }
        public int timeoutInMinutes { get; set; }
        public string executionOrder { get; set; }
    }

    public class Approval1
    {
        public int rank { get; set; }
        public bool isAutomated { get; set; }
        public bool isNotificationOn { get; set; }
        public int id { get; set; }
    }

    public class Environmentoptions
    {
        public string emailNotificationType { get; set; }
        public string emailRecipients { get; set; }
        public bool skipArtifactsDownload { get; set; }
        public int timeoutInMinutes { get; set; }
        public bool enableAccessToken { get; set; }
        public bool publishDeploymentStatus { get; set; }
        public bool badgeEnabled { get; set; }
        public bool autoLinkWorkItems { get; set; }
        public bool pullRequestDeploymentEnabled { get; set; }
    }

    public class Executionpolicy
    {
        public int concurrencyCount { get; set; }
        public int queueDepthCount { get; set; }
    }

    public class Currentrelease
    {
        public int id { get; set; }
        public string url { get; set; }
        public _Links4 _links { get; set; }
    }

    public class _Links4
    {
    }

    public class Retentionpolicy
    {
        public int daysToKeep { get; set; }
        public int releasesToKeep { get; set; }
        public bool retainBuild { get; set; }
    }

    public class Processparameters
    {
    }

    public class Properties1
    {
    }

    public class Predeploymentgates
    {
        public int id { get; set; }
        public object gatesOptions { get; set; }
        public object[] gates { get; set; }
    }

    public class Postdeploymentgates
    {
        public int id { get; set; }
        public object gatesOptions { get; set; }
        public object[] gates { get; set; }
    }

    public class Deployphas
    {
        public Deploymentinput deploymentInput { get; set; }
        public int rank { get; set; }
        public string phaseType { get; set; }
        public string name { get; set; }
        public object refName { get; set; }
        public Workflowtask[] workflowTasks { get; set; }
    }

    public class Deploymentinput
    {
        public Parallelexecution parallelExecution { get; set; }
        public object agentSpecification { get; set; }
        public bool skipArtifactsDownload { get; set; }
        public Artifactsdownloadinput artifactsDownloadInput { get; set; }
        public int queueId { get; set; }
        public object[] demands { get; set; }
        public bool enableAccessToken { get; set; }
        public int timeoutInMinutes { get; set; }
        public int jobCancelTimeoutInMinutes { get; set; }
        public string condition { get; set; }
        public Overrideinputs overrideInputs { get; set; }
    }

    public class Parallelexecution
    {
        public string parallelExecutionType { get; set; }
    }

    public class Artifactsdownloadinput
    {
        public object[] downloadInputs { get; set; }
    }

    public class Overrideinputs
    {
    }

    public class Workflowtask
    {
        public Environment1 environment { get; set; }
        public string taskId { get; set; }
        public string version { get; set; }
        public string name { get; set; }
        public string refName { get; set; }
        public bool enabled { get; set; }
        public bool alwaysRun { get; set; }
        public bool continueOnError { get; set; }
        public int timeoutInMinutes { get; set; }
        public string definitionType { get; set; }
        public Overrideinputs1 overrideInputs { get; set; }
        public string condition { get; set; }
        public Inputs inputs { get; set; }
    }

    public class Environment1
    {
    }

    public class Overrideinputs1
    {
    }

    public class Inputs
    {
        public string scriptType { get; set; }
        public string scriptName { get; set; }
        public string arguments { get; set; }
        public string workingFolder { get; set; }
        public string inlineScript { get; set; }
        public string failOnStandardError { get; set; }
        public string TargetServerFqdn { get; set; }
        public string ApplicationModulesToInstall { get; set; }
        public string ArtifactName { get; set; }
        public string Channel { get; set; }
        public string DatabaseModulesToInstall { get; set; }
        public string DeployDomain { get; set; }
        public string DeployPassword { get; set; }
        public string DeployUser { get; set; }
        public string Product { get; set; }
        public string RabbitAdminPass { get; set; }
        public string RabbitAdminUser { get; set; }
        public string SecurityServer { get; set; }
    }

    public class Condition
    {
        public string name { get; set; }
        public string conditionType { get; set; }
        public string value { get; set; }
    }

    public class Artifact
    {
        public string sourceId { get; set; }
        public string type { get; set; }
        public string alias { get; set; }
        public Definitionreference definitionReference { get; set; }
        public bool isPrimary { get; set; }
        public bool isRetained { get; set; }
    }

    public class Definitionreference
    {
        public Artifactsourcedefinitionurl artifactSourceDefinitionUrl { get; set; }
        public Artifactsourcedefinitionurl artifactSourceVersionUrl { get; set; }

        public Branch branch { get; set; }
        public Defaultversionspecific defaultVersionSpecific { get; set; }
        public Defaultversiontags defaultVersionTags { get; set; }
        public Defaultversiontype defaultVersionType { get; set; }
        public Definition definition { get; set; }
        public SourceVersion sourceVersion { get; set; }
        public Version version { get; set; }
        public Project project { get; set; }
    }

    public class Artifactsourcedefinitionurl
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class SourceVersion
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Branch
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Defaultversionspecific
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Defaultversiontags
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Defaultversiontype
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Definition
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Trigger
    {
        public string artifactAlias { get; set; }
        public Triggercondition[] triggerConditions { get; set; }
        public string triggerType { get; set; }
    }

    public class Triggercondition
    {
        public string sourceBranch { get; set; }
        public string[] tags { get; set; }
        public bool useBuildDefinitionBranch { get; set; }
        public bool createReleaseOnBuildTagging { get; set; }
    }
}