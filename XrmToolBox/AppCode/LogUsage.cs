using System;

#if !DEBUG
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
#endif

using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.AppCode
{
    /// <summary>
    /// Shamely stolen from Jonas Rapp @Cinteros
    /// </summary>
    public class LogUsage
    {
#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone

        public static async Task DoLog(Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
#if !DEBUG
            try
            {
                var assembly = Assembly.GetExecutingAssembly().GetName();
                var version = assembly.Version.ToString();
                var name = assembly.Name.Replace(" ", "");

                var query = "t.php?sc_project=10479559&security=34bb1777&java=1&invisible=1&u={2}&camefrom={0} {1}";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(name, version));
                    client.BaseAddress = new Uri("http://c.statcounter.com/");
                    var response = await client.GetAsync(string.Format(query, name, version, plugin.Metadata.Name))
                                .ConfigureAwait(continueOnCapturedContext: false);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch { }

#endif
        }

        internal static bool PromptToLog()
        {
            var msg = "Allow XrmToolBox to collect anonymous usage statistics (Only plugin opening is collected)?\n\n" +
                      "Statistics will be used to understand which plugins are most used.\n\n" +
                      "You can change this setting in XrmToolBox options anytime.\n\n" +
                      "Thanks!";

            return MessageBox.Show(msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}