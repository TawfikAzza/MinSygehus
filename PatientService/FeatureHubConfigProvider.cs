using FeatureHubSDK;

namespace PatientService;

public static class FeatureHubConfigProvider
{
    public static async Task<(EdgeFeatureHubConfig, IClientContext)> InitializeFeatureHub()
    {

        var config = new EdgeFeatureHubConfig("http://featurehub:8085", "1892bde9-881e-48d2-b5fe-8c4d8375190b/Nuvs2FY2TyPG0qIHMd04N1PILs88toTkcliZ9r2K");
        var clientContext = await config.NewContext().Build();
        return (config, clientContext);
    }
}
