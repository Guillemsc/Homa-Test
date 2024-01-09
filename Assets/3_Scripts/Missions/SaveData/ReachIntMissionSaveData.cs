public sealed class ReachIntMissionSaveData : MissionSaveData
{
    public int CurrentAmmount;
    
    public override TReturn Accept<TReturn, TIn>(IMissionSaveDataVisitor<TReturn, TIn> visitor, TIn val)
    {
        return visitor.Visit(this, val);
    }
}