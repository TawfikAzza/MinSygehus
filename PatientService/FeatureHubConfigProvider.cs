using FeatureHubSDK;

namespace PatientService;

public static class FeatureHubConfigProvider
{
    public static async Task<(EdgeFeatureHubConfig, IClientContext)> InitializeFeatureHub()
    {

        var config = new EdgeFeatureHubConfig("http://featurehub:8085", "46de0e52-f745-4b07-a362-966a972dfaa4/59Qurjmdr8zutsAfDPwIOjEsSrY94yTCW2l6RojW");
        var clientContext = await config.NewContext().Build();
        return (config, clientContext);
    }
}
