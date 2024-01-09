public sealed class NopMission : IMission
{
    public static readonly NopMission Instance = new();
    
    NopMission(){}

    public IMissionConfiguration Configuration => NopMissionConfiguration.Instance;

    public bool IsCompleted() => false;

    public TReturn Accept<TReturn>(IMissionVisitor<TReturn> visitor)
    {
        return visitor.Visit(this);
    }
}