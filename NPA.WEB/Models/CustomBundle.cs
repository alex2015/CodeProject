using System.Collections.Generic;

namespace NPA.WEB.Models
{
    public class BundleInformation
    {
        public List<CustomBundle> Bundles { get; set; }
    }

    public class CustomBundle
    {
        public string BundleName { get; set; }
        public string Path { get; set; }
        public bool IsLoaded { get; set; }               
    }
}
