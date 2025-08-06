using System;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;

namespace ReplaceDotnetHttpClient_LabAPI
{
    public class ReplaceDotnetHttpClient : Plugin<Config>
    {
        public override string Name { get; } = "ReplaceDotnetHttpClient";
        public override string Description { get; } = "替换SCPSL的httpclient以使用系统代理.";
        public override string Author { get; } = "The_BlueSky";
        public override Version Version { get; } = new(1, 0, 0);
        public override Version RequiredApiVersion { get; } = new();

        public override void Enable()
        {
            if (Config is not { Enabled: true })
                Logger.Info("Plugin is set to not start, change the configuration file if this is a mistake");
            
            HttpExtensions.ReplaceDotnetHttpClient();
        }

        public override void Disable()
        {
        }
    }
}
