public interface IMissionVisitor<out TReturn>
{
    TReturn Visit(NopMission mission);
    TReturn Visit(ReachIntMission mission);
}