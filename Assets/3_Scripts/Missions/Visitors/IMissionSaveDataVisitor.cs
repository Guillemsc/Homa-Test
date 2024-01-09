public interface IMissionSaveDataVisitor<out TReturn, in TIn>
{
    TReturn Visit(NopMissionSaveData saveData, TIn val);
    TReturn Visit(ReachIntMissionSaveData saveData, TIn val);
}