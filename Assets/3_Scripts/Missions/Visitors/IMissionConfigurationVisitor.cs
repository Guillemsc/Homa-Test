public interface IMissionConfigurationVisitor<out TReturn>
{
    TReturn Visit(NopMissionConfiguration configuration);
    TReturn Visit(ReachIntMissionConfiguration configuration);
}