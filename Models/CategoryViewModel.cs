using Umbraco.Cms.Core.Models.PublishedContent;

namespace DenKhangThinh.Models;

public class CategoryViewModel
{
    public bool IsCategory { get; set; }
    public IPublishedContent Data { get; set; }
}
