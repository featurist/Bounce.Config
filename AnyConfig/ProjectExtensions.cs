using System.Collections.Generic;
using Bounce.Framework.VisualStudio;

namespace AnyConfig
{
    public static class ProjectExtensions
    {
        public static void Configure(this IVisualStudioProject project, string environmentUrl)
        {
            new TemplateConfigurer().GenerateConfigurationForProject(project, environmentUrl);
        }

        public static void Configure(this IVisualStudioProject project, Dictionary<string, string> environment)
        {
            new TemplateConfigurer().GenerateConfigurationForProject(project, environment);            
        }

        public static bool HasTemplateConfig(this IVisualStudioProject project)
        {
            return new TemplateConfigurer().HasTemplateConfig(project);
        }
    }
}
