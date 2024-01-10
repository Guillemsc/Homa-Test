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
        IMissionConfiguration missionConfiguration = GetNewRandomMissionConfigurationWithSameDifficulty(
            missionsData,
            previous
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
    
    IMissionConfiguration GetNewRandomMissionConfigurationWithSameDifficulty(
        MissionsData missionsData,
        IMission previousMission
    )
    {
        bool difficultyFound = missionsData.MissionDifficultyConfigurationByMissionConfiguration.TryGetValue(
            previousMission.Configuration.DifficultyConfiguration,
            out List<IMissionConfiguration> missionConfigurations
        );

        if (!difficultyFound)
        {
            return NopMissionConfiguration.Instance;
        }

        if (missionConfigurations.Count == 0)
        {
            return NopMissionConfiguration.Instance;
        }

        List<IMissionConfiguration> uniqueMissions = new();

        foreach (IMissionConfiguration missionConfiguration in missionConfigurations)
        {
            bool isDifferent = missionConfiguration != previousMission.Configuration;

            if (isDifferent)
            {
                uniqueMissions.Add(missionConfiguration);
            }
        }

        if (uniqueMissions.Count == 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, missionConfigurations!.Count);
            return missionConfigurations[randomIndex];
        }

        int uniqueRandomIndex = UnityEngine.Random.Range(0, uniqueMissions!.Count);

        return uniqueMissions[uniqueRandomIndex];
    }
}