using System;

namespace TfsDeploymentChecker.Models.Tfs
{

    public class EnvironmentDeployedExecution
    {
        public int count { get; set; }
        public Execution[] value { get; set; }
    }

    public class Execution
    {
        public int id { get; set; }
        public Release release { get; set; }
        public Releasedefinition releaseDefinition { get; set; }
        public Releaseenvironment releaseEnvironment { get; set; }
        public object projectReference { get; set; }
        public int definitionEnvironmentId { get; set; }
        public int attempt { get; set; }
        public string reason { get; set; }
        public string deploymentStatus { get; set; }
        public string operationStatus { get; set; }
        public Requestedby requestedBy { get; set; }
        public Requestedfor1 requestedFor { get; set; }
        public DateTime queuedOn { get; set; }
        public DateTime startedOn { get; set; }
        public DateTime completedOn { get; set; }
        public DateTime lastModifiedOn { get; set; }
        public Lastmodifiedby lastModifiedBy { get; set; }
        public Condition[] conditions { get; set; }
        public Predeployapproval[] preDeployApprovals { get; set; }
        public Postdeployapproval[] postDeployApprovals { get; set; }
        public _Links6 _links { get; set; }
    }

    public class Release
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Artifact[] artifacts { get; set; }
        public string webAccessUri { get; set; }
        public _Links _links { get; set; }
    }


    public class Builduri
    {
        public string id { get; set; }
        public object name { get; set; }
    }

    public class Istriggeringartifact
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Isxamlbuildartifacttype
    {
        public string id { get; set; }
        public object name { get; set; }
    }

    public class Pullrequestmergecommitid
    {
        public string id { get; set; }
        public object name { get; set; }
    }


    public class RepositoryProvider
    {
        public string id { get; set; }
        public object name { get; set; }
    }

    public class Repository
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Requestedfor
    {
        public string id { get; set; }
        public object name { get; set; }
    }

    public class Requestedforid
    {
        public string id { get; set; }
        public object name { get; set; }
    }

    public class Sourceversion
    {
        public string id { get; set; }
        public object name { get; set; }
    }

    public class Version
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Artifactsourceversionurl
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Releasedefinition
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public Projectreference projectReference { get; set; }
        public string url { get; set; }
        public _Links1 _links { get; set; }
    }

    public class Projectreference
    {
        public string id { get; set; }
        public object name { get; set; }
    }

    public class Self1
    {
        public string href { get; set; }
    }

    public class Web1
    {
        public string href { get; set; }
    }

    public class Releaseenvironment
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public _Links2 _links { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Web2
    {
        public string href { get; set; }
    }

    public class Requestedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links3 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class Requestedfor1
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links4 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class Lastmodifiedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links5 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links5
    {
        public Avatar2 avatar { get; set; }
    }

    public class _Links6
    {
    }
    public class Predeployapproval
    {
        public int id { get; set; }
        public int revision { get; set; }
        public string approvalType { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public string status { get; set; }
        public string comments { get; set; }
        public bool isAutomated { get; set; }
        public bool isNotificationOn { get; set; }
        public int trialNumber { get; set; }
        public int attempt { get; set; }
        public int rank { get; set; }
        public Release1 release { get; set; }
        public Releasedefinition1 releaseDefinition { get; set; }
        public Releaseenvironment1 releaseEnvironment { get; set; }
        public string url { get; set; }
    }

    public class Release1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public _Links7 _links { get; set; }
    }

    public class _Links7
    {
        public Web3 web { get; set; }
        public Self3 self { get; set; }
    }

    public class Web3
    {
        public string href { get; set; }
    }

    public class Self3
    {
        public string href { get; set; }
    }

    public class Releasedefinition1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public object projectReference { get; set; }
        public string url { get; set; }
        public _Links8 _links { get; set; }
    }

    public class _Links8
    {
        public Web4 web { get; set; }
        public Self4 self { get; set; }
    }

    public class Web4
    {
        public string href { get; set; }
    }

    public class Self4
    {
        public string href { get; set; }
    }

    public class Releaseenvironment1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public _Links9 _links { get; set; }
    }

    public class _Links9
    {
        public Self5 self { get; set; }
    }

    public class Self5
    {
        public string href { get; set; }
    }

    public class Postdeployapproval
    {
        public int id { get; set; }
        public int revision { get; set; }
        public string approvalType { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public string status { get; set; }
        public string comments { get; set; }
        public bool isAutomated { get; set; }
        public bool isNotificationOn { get; set; }
        public int trialNumber { get; set; }
        public int attempt { get; set; }
        public int rank { get; set; }
        public Release2 release { get; set; }
        public Releasedefinition2 releaseDefinition { get; set; }
        public Releaseenvironment2 releaseEnvironment { get; set; }
        public string url { get; set; }
    }

    public class Release2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public _Links10 _links { get; set; }
    }

    public class _Links10
    {
        public Web5 web { get; set; }
        public Self6 self { get; set; }
    }

    public class Web5
    {
        public string href { get; set; }
    }

    public class Self6
    {
        public string href { get; set; }
    }

    public class Releasedefinition2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public object projectReference { get; set; }
        public string url { get; set; }
        public _Links11 _links { get; set; }
    }

    public class _Links11
    {
        public Web6 web { get; set; }
        public Self7 self { get; set; }
    }

    public class Web6
    {
        public string href { get; set; }
    }

    public class Self7
    {
        public string href { get; set; }
    }

    public class Releaseenvironment2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public _Links12 _links { get; set; }
    }

    public class _Links12
    {
        public Self8 self { get; set; }
    }

    public class Self8
    {
        public string href { get; set; }
    }
}