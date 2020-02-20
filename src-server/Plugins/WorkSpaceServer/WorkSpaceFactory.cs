using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.Hive.Plugin;

namespace RCC.WorkSpaceVR.HivePlugin
{
    public class WorkSpaceFactory : IPluginFactory
    {
        public IGamePlugin Create(IPluginHost gameHost, string pluginName, Dictionary<string, string> config,
            out string errorMsg)
        {
            var prefix = gameHost.GameId.Contains("_") ? gameHost.GameId.Substring(0, gameHost.GameId.IndexOf('_')) : string.Empty;
            IGamePlugin plugin;
            switch (pluginName)
            {
                case "WorkSpaceServer":
                    plugin = new WorkSpaceServer();
                    break;
                case "Webhooks":
                    plugin = new WebHooksPlugin();
                    plugin.SetupInstance(gameHost, config, out errorMsg);
                    return plugin;
                default:
                    switch (prefix)
                    {
                        case "ForwardPlugin1":
                            plugin = new WebHooksPlugin();
                            config = new Dictionary<string, string> { { "BaseUrl", "X" } };
                            break;
                        case "ForwardPlugin2":
                            if (string.IsNullOrEmpty(pluginName))
                            {
                                plugin = new PluginBase();
                            }
                            else
                            {
                                plugin = new WebHooksPlugin();
                                gameHost = new PluginHostWrapper(gameHost);
                                config = new Dictionary<string, string>
                                {
                                    {"BaseUrl", "http://photon-photon-pluginsdk-v1.webscript.io"},
                                    {"PathClose", "GameClose"},
                                    {"PathCreate", "GameCreate"},
                                };
                            }
                            break;
                        default:
                            plugin = new PluginBase();
                            break;
                    }
                    break;
            }
            if (plugin.SetupInstance(gameHost, config, out errorMsg))
            {
                return plugin;
            }
            return null;
        }
    }
}
