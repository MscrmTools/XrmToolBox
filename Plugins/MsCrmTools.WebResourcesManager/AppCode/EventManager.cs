using System;
using System.Diagnostics;

namespace MsCrmTools.WebResourcesManager.AppCode
{
    public class EventManager
    {
        private readonly Options options;

        public EventManager(Options options)
        {
            this.options = options;
        }

        public void ActAfterPublish(WebResource resource)
        {
            if (options.AfterPublishCommand.Contains("{FilePath}") && string.IsNullOrEmpty(resource.FilePath))
            {
                throw new Exception("It is required that the web resource has a file path in its properties to use a command referencing the tag {FilePath}");
            }

            RunCommand(options.AfterPublishCommand.Replace("{FilePath}", resource.FilePath));
        }

        public void ActAfterUpdate(WebResource resource)
        {
            if (options.AfterUpdateCommand.Contains("{FilePath}") && string.IsNullOrEmpty(resource.FilePath))
            {
                throw new Exception("It is required that the web resource has a file path in its properties to use a command referencing the tag {FilePath}");
            }

            RunCommand(options.AfterUpdateCommand.Replace("{FilePath}", resource.FilePath));
        }

        private void RunCommand(string command)
        {
            Process process = Process.Start(new ProcessStartInfo(command)
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            });

            string stderr = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrEmpty(stderr))
            {
                throw new Exception("An error occured when executing additional action (" + command + "):\r\n\r\n" +
                                    stderr);
            }
        }
    }
}