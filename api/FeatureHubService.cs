using FeatureHubSDK;

namespace api;

public class FeatureHubService
{
    private readonly EdgeFeatureHubConfig _config;
    
    public FeatureHubService()
    {
        _config = new EdgeFeatureHubConfig("http://featurehub:8085",
            "9473b90e-f482-4a88-b588-9f78ad687e39/6CjxEB3lrojdOqYWMU3md9vy03iH1qOyxucP1TvS");
    }

    public async Task<bool> IsFeatureEnabled(string featureKey)
    {
        var fh = await _config.NewContext().Build();
        return fh[featureKey].IsEnabled;
    }
}