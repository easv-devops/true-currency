using FeatureHubSDK;

namespace api;

public class FeatureHubService
{
    private readonly EdgeFeatureHubConfig _config;
    
    public FeatureHubService()
    {
        _config = new EdgeFeatureHubConfig("http://featurehub:8085",
            "5c0f0b36-21ed-4da1-bb6c-2ef1316ea865/J3tcF5V9eHZBwrw9IVgOaHMTDthmnCZi6claDzSw");
    }

    public async Task<bool> IsFeatureEnabled(string featureKey)
    {
        var fh = await _config.NewContext().Build();
        return fh[featureKey].IsEnabled;
    }
}