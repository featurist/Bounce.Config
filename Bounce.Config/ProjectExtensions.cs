using System.Collections.Generic;
using Bounce.Framework.VisualStudio;

namespace Bounce.Config
{
    public static class ProjectExtensions
    {
        public static void Configure(this IVisualStudioProject project, string environmentUrl)
        {
            new TemplateConfigurer().GenerateConfigurationForProject(project, environmentUrl);
        }

        public static void Configure(this IVisualStudioProject project, Dictionary<string, object> environment)
        {
            new TemplateConfigurer().GenerateConfigurationForProject(project, environment);            
        }

        public static bool HasTemplateConfig(this IVisualStudioProject project)
        {
            return new TemplateConfigurer().HasTemplateConfig(project);
        }
    }
}
