public sealed class NopMissionSaveData : MissionSaveData
{
    public override TReturn Accept<TReturn, TIn>(IMissionSaveDataVisitor<TReturn, TIn> visitor, TIn val)
    {
        return visitor.Visit(this, val);
    }
}