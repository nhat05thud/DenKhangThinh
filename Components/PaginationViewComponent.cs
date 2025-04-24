using DenKhangThinh.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DenKhangThinh.Components;

[ViewComponent(Name = "Pagination")]
public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PaginationViewModel model)
    {
        model ??= new PaginationViewModel();

        return View(model);
    }
}