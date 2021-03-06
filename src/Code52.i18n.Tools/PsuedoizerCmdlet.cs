using System.IO;
using System.Management.Automation;

namespace Code52.i18n
{
    [Cmdlet("Code52", "Psuedoizer")]
    public class PsuedoizerCmdlet : PSCmdlet
    {
        [Parameter(HelpMessage = "InputResourceFile", Mandatory = true)]
        public string InputResourceFile { get; set; }

        [Parameter(HelpMessage = "OutputResourceFile", Mandatory = true)]
        public string OutputResourceFile { get; set; }

        protected override void ProcessRecord()
        {
            var fullInputPath = InputResourceFile;
            if (!Path.IsPathRooted(InputResourceFile))
            {
                fullInputPath = Resolve(InputResourceFile);
            }

            var fullOutputPath = OutputResourceFile;
            if (!Path.IsPathRooted(OutputResourceFile))
            {
                fullOutputPath = Resolve(OutputResourceFile);
            }

            if(!File.Exists(fullInputPath))
            {
                WriteObject(string.Format("input file does not exist: {0}", fullOutputPath));
                WriteObject("closing...");
                return;
            }

            WriteObject("Pseudoizer: Adapted from MSDN BugSlayer 2004-Apr i18n Article");
            WriteObject(string.Format(" - generating resource file from {0}", fullInputPath));
            WriteObject(string.Format(" - output to {0}", fullOutputPath));

            var psuedoizer = new Psuedoizer();
            psuedoizer.Run(fullInputPath, fullOutputPath);

            WriteObject(string.Format("Psuedoizer completed successfully"));
        }

        private string Resolve(string relativePath)
        {
            var currentPath = SessionState.Path.CurrentFileSystemLocation.Path;

            if (relativePath.StartsWith(@".\"))
            {
                relativePath = relativePath.Substring(2);
            }

            return Path.Combine(currentPath, relativePath);
        }
    }
}