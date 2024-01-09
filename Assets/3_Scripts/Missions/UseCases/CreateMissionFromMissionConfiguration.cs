public sealed class CreateMissionFromMissionConfiguration : IMissionConfigurationVisitor<IMission>
{
    public static readonly CreateMissionFromMissionConfiguration Instance = new();
    
    CreateMissionFromMissionConfiguration(){}

    public IMission Execute(IMissionConfiguration missionConfiguration)
    {
        return missionConfiguration.Accept(this);
    }
    
    public IMission Visit(NopMissionConfiguration configuration)
    {
        return NopMission.Instance;
    }

    public IMission Visit(ReachIntMissionConfiguration configuration)
    {
        return new ReachIntMission(configuration);
    }
}