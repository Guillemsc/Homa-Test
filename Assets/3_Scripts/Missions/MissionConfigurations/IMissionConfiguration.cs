public interface IMissionConfiguration
{
    string Uid { get; }
    IMissionDifficultyConfiguration DifficultyConfiguration { get; }
    IRewardConfiguration RewardConfiguration { get; }
    MissionType MissionType { get; }
    string DisplayName { get; }
    
    TReturn Accept<TReturn>(IMissionConfigurationVisitor<TReturn> visitor);
}