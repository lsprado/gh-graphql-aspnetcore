namespace gh_graphql.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public Repository repository { get; set; }
    }

    public class DefaultBranchRef
    {
        public string name { get; set; }
    }

    public class Dependencies
    {
        public List<Node> nodes { get; set; }
    }

    public class DependencyGraphManifests
    {
        public List<Node> nodes { get; set; }
    }

    public class LicenseInfo
    {
        public string name { get; set; }
    }

    public class Node
    {
        public string blobPath { get; set; }
        public Dependencies dependencies { get; set; }
        public string packageName { get; set; }
        public string requirements { get; set; }
        public bool hasDependencies { get; set; }
        public Repository repository { get; set; }
    }

    public class Owner
    {
        public string login { get; set; }
    }

    public class PrimaryLanguage
    {
        public string name { get; set; }
    }

    public class Repository
    {
        public string id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public DateTime createdAt { get; set; }
        public bool allowUpdateBranch { get; set; }
        public DefaultBranchRef defaultBranchRef { get; set; }
        public bool isInOrganization { get; set; }
        public bool autoMergeAllowed { get; set; }
        public LicenseInfo licenseInfo { get; set; }
        public DependencyGraphManifests dependencyGraphManifests { get; set; }
        public string nameWithOwner { get; set; }
        public Owner owner { get; set; }
        public PrimaryLanguage primaryLanguage { get; set; }
    }

    public class Result
    {
        public Data data { get; set; }
    }


}
