using System.Collections.Generic;

public sealed class CreateMissionFromMissionSaveData : IMissionSaveDataVisitor<IMission, IMissionConfiguration>
{
    public static readonly CreateMissionFromMissionSaveData Instance = new();
    
    CreateMissionFromMissionSaveData(){}

    public IMission Execute(Dictionary<string, IMissionConfiguration> missionUidByMissionConfigurations, MissionSaveData saveData)
    {
        bool configurationFound = missionUidByMissionConfigurations.TryGetValue(
            saveData.ConfigurationUid,
            out IMissionConfiguration missionConfiguration
        );

        if (!configurationFound)
        {
            return NopMission.Instance;
        }
        
        return saveData.Accept(this, missionConfiguration);
    }

    public IMission Visit(NopMissionSaveData saveData, IMissionConfiguration configuration)
    {
        return NopMission.Instance;
    }

    public IMission Visit(ReachIntMissionSaveData saveData, IMissionConfiguration configuration)
    {
        return new ReachIntMission((ReachIntMissionConfiguration)configuration)
        {
            CurrentAmmount = saveData.CurrentAmmount,
        };
    }
}