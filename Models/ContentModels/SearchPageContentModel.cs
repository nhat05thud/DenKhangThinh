using DenKhangThinh.Models.Search;
using DenKhangThinh.Models.ViewModels;

namespace DenKhangThinh.Models.ContentModels;

public class SearchPageContentModel : ContentModel
{
    public SearchPageContentModel(IPublishedContent? content) : base(content)
    {
    }

    public SearchRequestModel? SearchRequest { get; set; }

    public SearchResponseModel? SearchResponse { get; set; }

    public PaginationViewModel Pagination { get; set; }
}