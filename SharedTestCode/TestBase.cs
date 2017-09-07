using System;
using System.IO;

namespace DocMySwagApp.UnitTest
{
    public abstract class TestBase
    {
        private readonly Lazy<string> _lazyInputDir;
        private readonly Lazy<string> _lazyOutputDir;

        public string InputTestDir => _lazyInputDir.Value;
        public string OutputTestDir => _lazyOutputDir.Value;

        protected TestBase()
        {
            var baseDir = AppContext.BaseDirectory;
            var guidToUse = Guid.NewGuid().ToString();

            _lazyInputDir = new Lazy<string>(() =>
            {
                var path = Path.Combine(baseDir, "TestInput" + guidToUse);
                Directory.CreateDirectory(path);
                File.Copy("SwaggerFile.xml", Path.Combine(path, "SwaggerFile.xml"));
                File.Copy("SwaggerFileInvalid.xml", Path.Combine(path, "SwaggerFileInvalid.xml"));
                return path;
            });
            _lazyOutputDir = new Lazy<string>(() =>
            {
                var path = Path.Combine(baseDir, "TestOutput" + guidToUse);
                Directory.CreateDirectory(path);
                return path;
            });
        }


    }
}
