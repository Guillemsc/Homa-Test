public sealed class CreateMissionSaveDataFromMission : IMissionVisitor<MissionSaveData>
{
    public static readonly CreateMissionSaveDataFromMission Instance = new();
    
    CreateMissionSaveDataFromMission(){}

    public MissionSaveData Execute(IMission mission)
    {
        return mission.Accept(this);
    }

    public MissionSaveData Visit(NopMission mission)
    {
        return new NopMissionSaveData();
    }

    public MissionSaveData Visit(ReachIntMission mission)
    {
        return new ReachIntMissionSaveData
        {
            ConfigurationUid = mission.Configuration.Uid,
            CurrentAmmount = mission.CurrentAmmount
        };
    }
}