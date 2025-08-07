using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;

namespace ReplaceDotnetHttpClient_LabAPI
{
    public class ReplaceDotnetHttpClient : Plugin<Config>
    {
        public override string Name => "ReplaceDotnetHttpClient";
        public override string Description => "Replace SCPSL's httpclient to use system proxy.";
        public override string Author => "The_BlueSky";
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
