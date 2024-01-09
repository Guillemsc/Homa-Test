public sealed class NopMissionDifficultyConfiguration : IMissionDifficultyConfiguration
{
    public static readonly NopMissionDifficultyConfiguration Instance = new();
    
    public string DisplayName => string.Empty;
    public int DifficultyIndex => 0;

    NopMissionDifficultyConfiguration() {}
}