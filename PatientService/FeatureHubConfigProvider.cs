using FeatureHubSDK;

namespace PatientService;

public static class FeatureHubConfigProvider
{
    public static async Task<(EdgeFeatureHubConfig, IClientContext)> InitializeFeatureHub()
    {
        var config = new EdgeFeatureHubConfig("http://featurehub:8085", "2fdc0046-ec6e-4958-a7d2-42c35fa4b63d/fHRM50jhH7yQDYafPGIwD1BmpTXYsC*sEBBuMZqomC9PoqrXO1v");
        var clientContext = await config.NewContext().UserKey("sergio").Build();
        return (config, clientContext);
    }
}
