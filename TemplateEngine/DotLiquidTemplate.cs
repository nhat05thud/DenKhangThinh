using DotLiquid;
using System.Text;

namespace DenKhangThinh.TemplateEngine;

public class DotLiquidTemplate : ITextTemplate
{
    public DotLiquidTemplate(string templateDir)
    {
        TemplateDir = templateDir;
    }

    public string TemplateDir { get; }

    public string Render(string templateName, object model)
    {
        if (templateName?.Contains("\\") == true) throw new ArgumentException($"templateName {templateName} is invalid");

        var templateFile = Path.Combine(TemplateDir, $"{templateName}.html");
        var templateFileCulture = Path.Combine(TemplateDir, $"{templateName}.html");

        if (System.IO.File.Exists(templateFileCulture))
        {
            templateFile = templateFileCulture;
        }
        else if (!System.IO.File.Exists(templateFile))
        {
            throw new FileNotFoundException($"TemplateName {templateName} not found.");
        }

        var liquidTemplate = System.IO.File.ReadAllText(templateFile, Encoding.UTF8);

        var template = DotLiquid.Template.Parse(liquidTemplate);
        try
        {
            return template.Render(Hash.FromAnonymousObject(model));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}