using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Bounce.Framework.VisualStudio;
using Newtonsoft.Json;
using Nustache.Core;

namespace Bounce.Config
{
    public class TemplateConfigurer
    {
        public void ConfigureFile(string templateFile, Dictionary<string, object> environment,
                                                      string outputFile)
        {
            var config = Render.FileToString(templateFile, environment);
            File.WriteAllText(outputFile, config);
        }

        public void GenerateConfigurationForProject(IVisualStudioProject project, string environmentUrl)
        {
            GenerateConfigurationForProject(project, LoadEnvironmentConfiguration(environmentUrl));
        }

        public void GenerateConfigurationForProject(IVisualStudioProject project, Dictionary<string, object> environment)
        {
            var exeConfig = project.OutputFile + ".config";
            var templateFile = ProjectTemplateConfig(project);
            if (templateFile != null)
            {
                var configFile = Regex.Replace(templateFile, @"\.template\.config$", ".config");
                List<string> configFiles = new List<string>();
                configFiles.Add(configFile);
                if (File.Exists(exeConfig))
                {
                    configFiles.Add(exeConfig);
                }
                ConfigureFiles(templateFile, environment, configFiles);
            }
        }

        public Dictionary<string, object> LoadEnvironmentConfiguration(string environmentUrl)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(new WebClient().DownloadString(environmentUrl));
        }

        public void ConfigureFiles(string templateFile, Dictionary<string, object> environment, IEnumerable<string> outputConfigFiles)
        {
            var config = Render.FileToString(templateFile, environment);
            foreach (var configFile in outputConfigFiles)
            {
                File.WriteAllText(configFile, config);
            }
        }

        private string ProjectTemplateConfig(IVisualStudioProject project)
        {
            return Directory.GetFiles(project.ProjectDirectory, "*.template.config").FirstOrDefault();
        }

        public bool HasTemplateConfig(IVisualStudioProject project)
        {
            return ProjectTemplateConfig(project) != null;
        }
    }
}
