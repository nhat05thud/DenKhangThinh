using DenKhangThinh.Services;
using Umbraco.Cms.Core.Composing;

namespace DenKhangThinh.App_Start;

public class RegisterServicesComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IDataTypeValueService, DataTypeValueService>();
        builder.Services.AddScoped<IViewRenderService, ViewRenderService>();
        builder.Services.AddTransient<ISearchService, SearchService>();
    }
}
