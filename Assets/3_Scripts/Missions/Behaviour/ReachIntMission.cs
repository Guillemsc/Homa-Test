public sealed class ReachIntMission : IMission
{
    public IMissionConfiguration Configuration => ActualConfiguration;
    public int CurrentAmmount { get; set; }
    public ReachIntMissionConfiguration ActualConfiguration { get; }
    
    public ReachIntMission(ReachIntMissionConfiguration configuration)
    {
        ActualConfiguration = configuration;
    }

    public bool IsCompleted()
    {
        return CurrentAmmount >= ActualConfiguration.Ammount;
    }

    public TReturn Accept<TReturn>(IMissionVisitor<TReturn> visitor)
    {
        return visitor.Visit(this);
    }
}