using System.IO;
using NUnit.Framework;

namespace Bounce.Config.Tests
{
    [TestFixture]
    public class ConfigureTests
    {
        [SetUp]
        public void SetUp()
        {
            var fs = new FS.FileSystem();
            fs.Delete(@"projectdirectory");
            fs.CreateDirectory(@"projectdirectory\bin\debug");
            WriteToFile(@"dev.json", @"{""name"": ""bob"", ""number"": 1}");
        }

        [Test]
        public void FindsTemplateFileAndGeneratesConfigFiles()
        {
            WriteToFile(@"projectdirectory\app.template.config", "hi {{name}} +{{number}}");
            WriteToFile(@"projectdirectory\bin\debug\somefile.dll.config", "hi jeff +1");

            Project.Configure("dev.json");

            Assert.That(File.ReadAllText(@"projectdirectory\app.config"), Is.EqualTo("hi bob +1\r\n"));
            Assert.That(File.ReadAllText(@"projectdirectory\bin\debug\somefile.dll.config"), Is.EqualTo("hi bob +1\r\n"));
        }

        [Test]
        public void DoesntWriteOutputConfigIfItDoesntExist()
        {
            WriteToFile(@"projectdirectory\web.template.config", "hi {{name}} +{{number}}");

            Project.Configure("dev.json");

            Assert.That(File.ReadAllText(@"projectdirectory\web.config"), Is.EqualTo("hi bob +1\r\n"));
            Assert.That(File.Exists(@"projectdirectory\bin\debug\somefile.dll.config"), Is.False);
        }

        private static FakeProject Project
        {
            get
            {
                var project = new FakeProject
                {
                    OutputFile = @"projectdirectory\bin\debug\somefile.dll",
                    ProjectDirectory = "projectdirectory"
                };
                return project;
            }
        }

        private static void WriteToFile(string filename, string text)
        {
            using (var template = File.CreateText(filename))
            {
                template.WriteLine(text);
            }
        }
    }
}
