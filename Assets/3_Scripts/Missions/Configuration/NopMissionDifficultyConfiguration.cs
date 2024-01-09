public sealed class NopMissionDifficultyConfiguration : IMissionDifficultyConfiguration
{
    public static readonly NopMissionDifficultyConfiguration Instance = new();
    
    public string DisplayName => string.Empty;
    
    NopMissionDifficultyConfiguration() {}
}