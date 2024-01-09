public interface IMissionConfiguration
{
    string Uid { get; }
    IMissionDifficultyConfiguration DifficultyConfiguration { get; }
    MissionType MissionType { get; }
    string DisplayName { get; }
    
    TReturn Accept<TReturn>(IMissionConfigurationVisitor<TReturn> visitor);
}