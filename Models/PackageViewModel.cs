using Umbraco.Cms.Core.Models.PublishedContent;

namespace DenKhangThinh.Models;

public class PackageViewModel
{
    public bool IsPackage { get; set; }
    public IPublishedContent Data { get; set; }
}
