using System.Collections.Generic;

public sealed class EneOfEachDifficultyMissionGenerator : IMissionGenerator
{
    public IMission Generate(MissionsData missionsData)
    {
        IMissionDifficultyConfiguration missionDifficulty = GetMissionDifficultyNotFoundOnActiveMissions(missionsData);

        missionsData.MissionDifficultyConfigurationByMissionConfiguration.TryGetValue(
            missionDifficulty,
            out List<IMissionConfiguration> missionConfigurations
        );
        
        int randomIndex = UnityEngine.Random.Range(0, missionConfigurations!.Count);

        IMissionConfiguration missionConfiguration = missionConfigurations[randomIndex];
        
        IMission newMission = CreateMissionFromMissionConfiguration.Instance.Execute(missionConfiguration);

        return newMission;
    }

    public IMission Replace(MissionsData missionsData, IMission previous)
    {
        IMissionConfiguration missionConfiguration = GetRandomMissionConfigurationWithDifficulty(
            missionsData,
            previous.Configuration.DifficultyConfiguration
        );

        IMission newMission = CreateMissionFromMissionConfiguration.Instance.Execute(missionConfiguration);

        return newMission;
    }

    IMissionDifficultyConfiguration GetMissionDifficultyNotFoundOnActiveMissions(MissionsData missionsData)
    {
        if (missionsData.MissionDifficultyConfigurationByMissionConfiguration.Count == 0)
        {
            return NopMissionDifficultyConfiguration.Instance;
        }
        
        foreach (KeyValuePair<IMissionDifficultyConfiguration, List<IMissionConfiguration>> item in
                 missionsData.MissionDifficultyConfigurationByMissionConfiguration)
        {
            bool found = false;
            
            foreach (IMission activeMission in missionsData.ActiveMissions)
            {
                found = activeMission.Configuration.DifficultyConfiguration == item.Key;

                if (found)
                {
                    break;
                }
            }

            if (!found)
            {
                return item.Key;
            }
        }

        int randomIndex = UnityEngine.Random.Range(0, missionsData.MissionDifficultyConfigurations!.Count);
        
        return missionsData.MissionDifficultyConfigurations[randomIndex];
    }
    
    IMissionConfiguration GetRandomMissionConfigurationWithDifficulty(
        MissionsData missionsData,
        IMissionDifficultyConfiguration difficultyConfiguration
    )
    {
        bool difficultyFound = missionsData.MissionDifficultyConfigurationByMissionConfiguration.TryGetValue(
            difficultyConfiguration,
            out List<IMissionConfiguration> missionConfigurations
        );

        if (!difficultyFound)
        {
            return NopMissionConfiguration.Instance;
        }

        int randomIndex = UnityEngine.Random.Range(0, missionConfigurations!.Count);

        return missionConfigurations[randomIndex];
    }
}