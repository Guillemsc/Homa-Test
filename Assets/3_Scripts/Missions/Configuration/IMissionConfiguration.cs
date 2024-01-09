public interface IMissionConfiguration
{
    string Uid { get; }
    IMissionDifficultyConfiguration DifficultyConfiguration { get; }
    string DisplayName { get; }
    
    TReturn Accept<TReturn>(IMissionConfigurationVisitor<TReturn> visitor);
}