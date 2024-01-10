using System;
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

    public void ProgressReachIntMission(int add, MissionType missionType)
    {
        ProgressMission<ReachIntMission>(missionType, m => m.CurrentAmmount += add);
    }

    void ProgressMission<T>(MissionType missionType, Action<T> progressAction) where T : IMission
    {
        bool somethingChanged = false;
        
        List<T> missions = GetActiveMissionsOfType<T>(missionType);
        
        foreach (T mission in missions)
        {
            bool wasCompleted = mission.IsCompleted();

            if (wasCompleted)
            {
                continue;
            }
            
            progressAction?.Invoke(mission);

            somethingChanged = true;
        }

        if (somethingChanged)
        {
            SaveToSaveData();
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
    
    public void ReplaceMission(IMission mission)
    {
        bool wasActive = m_missionsData.ActiveMissions.Remove(mission);

        if (!wasActive)
        {
            return;
        }

        IMission newMission = m_missionGenerator.Replace(m_missionsData, mission); 
            
        m_missionsData.ActiveMissions.Add(newMission);
        
        SaveToSaveData();
    }
    
    void CreateNecessaryMissions()
    {
        while (m_maxActiveMissions > m_missionsData.ActiveMissions.Count)
        {
            IMission newMission = m_missionGenerator.Generate(m_missionsData); 
            
            m_missionsData.ActiveMissions.Add(newMission);
        }
        
        SaveToSaveData();
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