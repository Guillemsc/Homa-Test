using System.Collections.Generic;

#nullable enable

public sealed class MissionsService
{
    static MissionsService? m_instance;
    public static MissionsService Instance => m_instance ??= new MissionsService();

    readonly List<IMissionConfiguration> m_missionConfigurations = new();
    readonly Dictionary<string, IMissionConfiguration> m_missionUidByMissionConfigurations = new();
    readonly Dictionary<IMissionDifficultyConfiguration, List<IMissionConfiguration>> m_missionDifficultyConfigurationByMissionConfiguration = new();
    
    readonly List<IMission> m_activeMissions = new();

    public void Init(IReadOnlyList<IMissionConfiguration> missionConfigurations)
    {
        m_missionConfigurations.AddRange(missionConfigurations);
        
        foreach (IMissionConfiguration missionConfiguration in missionConfigurations)
        {
            m_missionUidByMissionConfigurations.Add(missionConfiguration.Uid, missionConfiguration);

            bool difficultyFound = m_missionDifficultyConfigurationByMissionConfiguration.TryGetValue(
                missionConfiguration.DifficultyConfiguration,
                out List<IMissionConfiguration> missionConfigurationList
            );

            if (!difficultyFound)
            {
                missionConfigurationList = new();
                m_missionDifficultyConfigurationByMissionConfiguration.Add(missionConfiguration.DifficultyConfiguration, missionConfigurationList);
            }
            
            missionConfigurationList!.Add(missionConfiguration);
        }
    }
    
    void LoadFromSaveData()
    {
        MissionsSaveData saveData = SaveData.Load(MissionsSaveDataConstants.SaveDataLocation, new MissionsSaveData());

        foreach (MissionSaveData missionSaveData in saveData.ActiveMissions)
        {
            IMission mission = CreateMissionFromMissionSaveData.Instance.Execute(
                m_missionUidByMissionConfigurations,
                missionSaveData
            );
            
            m_activeMissions.Add(mission);
        }
    }

    void SaveToSaveData()
    {
        MissionsSaveData missionsSaveData = new();
        
        foreach (IMission mission in m_activeMissions)
        {
            MissionSaveData missionSaveData = CreateMissionSaveDataFromMission.Instance.Execute(mission);
            
            missionsSaveData.ActiveMissions.Add(missionSaveData);
        }
        
        SaveData.Save(MissionsSaveDataConstants.SaveDataLocation, missionsSaveData);
    }

    void GenerateNewActiveMissionsIfCompleted()
    {
        List<IMission> completedMissions = new();
        
        foreach (IMission mission in m_activeMissions)
        {
            bool completed = mission.IsCompleted();

            if (completed)
            {
                completedMissions.Add(mission);
            }
        }

        foreach (IMission completedMission in completedMissions)
        {
            m_activeMissions.Remove(completedMission);

            IMissionConfiguration configuration = GetRandomMissionConfigurationWithDifficulty(
                completedMission.Configuration.DifficultyConfiguration
            );

            IMission newMission = CreateMissionFromMissionConfiguration.Instance.Execute(configuration);
            
            m_activeMissions.Add(newMission);
        }
    }

    IMissionConfiguration GetRandomMissionConfigurationWithDifficulty(
        IMissionDifficultyConfiguration difficultyConfiguration
        )
    {
        bool difficultyFound = m_missionDifficultyConfigurationByMissionConfiguration.TryGetValue(
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