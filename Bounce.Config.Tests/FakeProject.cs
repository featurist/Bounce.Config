using System;
using Bounce.Framework.VisualStudio;

namespace Bounce.Config.Tests
{
    class FakeProject : IVisualStudioProject
    {
        public void Build(string config = "Debug", string outputDir = null, string target = null)
        {
            throw new NotImplementedException();
        }

        public string OutputFile { get; set; }
        public string Name { get; set; }
        public string OutputDirectory { get; set; }
        public string ProjectFile { get; set; }
        public string ProjectDirectory { get; set; }
    }
}
