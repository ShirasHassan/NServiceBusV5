using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using NUnit.Framework;

namespace IntegrityTests
{
    public class TestRunner
    {
        private string glob;
        private string errorMessage;
        private List<Regex> ignoreRegexes;

        public TestRunner(string glob, string errorMessage)
        {
            this.glob = glob;
            this.errorMessage = errorMessage;
            this.ignoreRegexes = new List<Regex>();
            this.ignoreRegexes.Add(new Regex(@"\\IntegrityTests\\"));
        }

        public void Run(Func<string, bool> testDelegate)
        {
            var badProjects = new List<string>();

            foreach (var rootPath in TestSetup.RootDirectories)
            {
                var projectFiles = Directory.GetFiles(rootPath, glob, SearchOption.AllDirectories);

                foreach (var projectFilePath in projectFiles)
                {
                    if (ignoreRegexes.Any(r => r.IsMatch(projectFilePath)))
                    {
                        continue;
                    }

                    bool success = testDelegate(projectFilePath);

                    if (!success)
                    {
                        badProjects.Add(projectFilePath);
                    }
                }
            }

            if (badProjects.Count > 0)
            {
                Assert.Fail($"{errorMessage}:\r\n  > {string.Join("\r\n  > ", badProjects)}");
            }
        }

        public TestRunner IgnoreRegex(string pattern, RegexOptions regexOptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)
        {
            ignoreRegexes.Add(new Regex(pattern, regexOptions));
            return this;
        }

        public TestRunner IgnoreWildcard(string wildcardExpression)
        {
            string pattern = Regex.Escape(wildcardExpression).Replace(@"\*", ".*").Replace(@"\?", ".");
            return IgnoreRegex(pattern);
        }

        public TestRunner IgnoreSamples()
        {
            return IgnoreRegex(@"\\samples\\");
        }

        public TestRunner IgnoreSnippets()
        {
            return IgnoreRegex(@"\\snippets\\");
        }

        public TestRunner IgnoreTutorials()
        {
            return IgnoreRegex(@"\\tutorials\\");
        }
    }
}