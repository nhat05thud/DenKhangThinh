using Umbraco.Cms.Core.Models.PublishedContent;

namespace DenKhangThinh.Models;

public class ProductViewModel
{
    public ProductViewModel()
    {
        IsFullWidth = false;
        ColumnWitdh = "w-6/12";
    }
    public bool IsHome { get; set; }
    public bool IsFullWidth { get; set; }
    public string ColumnWitdh { get; set; }
    public IPublishedContent? Data { get; set; }
}
