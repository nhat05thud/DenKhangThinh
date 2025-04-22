using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace DenKhangThinh.App_Start;

public class RemoveDashboard : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Dashboards().Remove<ContentDashboard>();
        builder.Dashboards().Remove<RedirectUrlDashboard>();
    }
}
