public sealed class NopMissionConfiguration : IMissionConfiguration
{
    public static readonly NopMissionConfiguration Instance = new();
        
    public string Uid => string.Empty;
    public IMissionDifficultyConfiguration DifficultyConfiguration => NopMissionDifficultyConfiguration.Instance;
    public IRewardConfiguration RewardConfiguration => NopRewardConfiguration.Instance;
    public MissionType MissionType => default;
    public string DisplayName => string.Empty;
        
    NopMissionConfiguration() { }
        
    public TReturn Accept<TReturn>(IMissionConfigurationVisitor<TReturn> visitor)
    {
        return visitor.Visit(this);
    }
}