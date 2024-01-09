public abstract class MissionSaveData
{
    public string ConfigurationUid;

    public abstract TReturn Accept<TReturn, TIn>(IMissionSaveDataVisitor<TReturn, TIn> visitor, TIn val);
}