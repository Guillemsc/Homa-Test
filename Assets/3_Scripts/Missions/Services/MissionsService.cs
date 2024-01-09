using System.Collections.Generic;

#nullable enable

public sealed class MissionsService
{
    static MissionsService? m_instance;
    public static MissionsService Instance => m_instance ??= new MissionsService();

    public IReadOnlyList<IMission> ActiveMissions => m_missionsData.ActiveMissions;
    
    readonly MissionsData m_missionsData = new();

    IMissionGenerator m_missionGenerator = NopMissionGenerator.Instance;
    int m_maxActiveMissions;

    bool m_inited;
    
    public void Init(
        IReadOnlyList<IMissionConfiguration> missionConfigurations, 
        IMissionGenerator missionGenerator,
        int maxActiveMissions
        )
    {
        if (m_inited)
        {
            return;
        }

        m_inited = true;
        
        m_missionGenerator = missionGenerator;
        m_maxActiveMissions = maxActiveMissions;
        
        PopulateConfigurationData(missionConfigurations);
        LoadFromSaveData();
        CreateNecessaryMissions();
    }

    public void AddToReachIntMission(int add, MissionType missionType)
    {
        List<ReachIntMission> missions = GetActiveMissionsOfType<ReachIntMission>(missionType);

        foreach (ReachIntMission mission in missions)
        {
            mission.CurrentAmmount += add;
        }
    }

    void PopulateConfigurationData(IReadOnlyList<IMissionConfiguration> missionConfigurations)
    {
        foreach (IMissionConfiguration missionConfiguration in missionConfigurations)
        {
            m_missionsData.MissionUidByMissionConfigurations.Add(missionConfiguration.Uid, missionConfiguration);

            bool difficultyFound = m_missionsData.MissionDifficultyConfigurationByMissionConfiguration.TryGetValue(
                missionConfiguration.DifficultyConfiguration,
                out List<IMissionConfiguration> missionConfigurationList
            );

            if (!difficultyFound)
            {
                missionConfigurationList = new List<IMissionConfiguration>();
                m_missionsData.MissionDifficultyConfigurationByMissionConfiguration.Add(
                    missionConfiguration.DifficultyConfiguration,
                    missionConfigurationList
                );
                m_missionsData.MissionDifficultyConfigurations.Add(missionConfiguration.DifficultyConfiguration);
            }

            missionConfigurationList!.Add(missionConfiguration);
        }
    }
    
    public void RefreshCompletedMissions()
    {
        List<IMission> completedMissions = new();
        
        foreach (IMission mission in m_missionsData.ActiveMissions)
        {
            bool completed = mission.IsCompleted();

            if (completed)
            {
                completedMissions.Add(mission);
            }
        }

        foreach (IMission completedMission in completedMissions)
        {
            m_missionsData.ActiveMissions.Remove(completedMission);
            
            IMission newMission = m_missionGenerator.Replace(m_missionsData, completedMission); 
            
            m_missionsData.ActiveMissions.Add(newMission);
        }
    }
    
    void CreateNecessaryMissions()
    {
        while (m_maxActiveMissions > m_missionsData.ActiveMissions.Count)
        {
            IMission newMission = m_missionGenerator.Generate(m_missionsData); 
            
            m_missionsData.ActiveMissions.Add(newMission);
        }
    }

    List<T> GetActiveMissionsOfType<T>(MissionType missionType) where T : IMission
    {
        List<T> ret = new();

        foreach (IMission mission in m_missionsData.ActiveMissions)
        {
            if (mission.Configuration.MissionType != missionType)
            {
                continue;
            }
            
            if (mission is T castedMission)
            {
                ret.Add(castedMission);
            }
        }
        
        return ret;
    }
    
    void LoadFromSaveData()
    {
        MissionsSaveData saveData = SaveData.Load(MissionsSaveDataConstants.SaveDataLocation, new MissionsSaveData());

        foreach (MissionSaveData missionSaveData in saveData.ActiveMissions)
        {
            IMission mission = CreateMissionFromMissionSaveData.Instance.Execute(
                m_missionsData.MissionUidByMissionConfigurations,
                missionSaveData
            );
            
            m_missionsData.ActiveMissions.Add(mission);
        }
    }

    void SaveToSaveData()
    {
        MissionsSaveData missionsSaveData = new();
        
        foreach (IMission mission in m_missionsData.ActiveMissions)
        {
            MissionSaveData missionSaveData = CreateMissionSaveDataFromMission.Instance.Execute(mission);
            
            missionsSaveData.ActiveMissions.Add(missionSaveData);
        }
        
        SaveData.Save(MissionsSaveDataConstants.SaveDataLocation, missionsSaveData);
    }
}