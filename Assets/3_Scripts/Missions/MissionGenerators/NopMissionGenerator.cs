public sealed class NopMissionGenerator : IMissionGenerator
{
    public static readonly NopMissionGenerator Instance = new();
    
    NopMissionGenerator(){}
    
    public IMission Generate(MissionsData missionsData)
        => NopMission.Instance;

    public IMission Replace(MissionsData missionsData, IMission previous)
        => NopMission.Instance;
}