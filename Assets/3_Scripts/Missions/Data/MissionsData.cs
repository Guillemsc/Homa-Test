using System.Collections.Generic;

public sealed class MissionsData
{
    public Dictionary<string, IMissionConfiguration> MissionUidByMissionConfigurations { get; } = new();
    public Dictionary<IMissionDifficultyConfiguration, List<IMissionConfiguration>> MissionDifficultyConfigurationByMissionConfiguration { get; } = new();
    public List<IMissionDifficultyConfiguration> MissionDifficultyConfigurations { get; } = new();
    
    public List<IMission> ActiveMissions = new();
}