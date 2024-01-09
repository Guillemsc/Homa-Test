public interface IMissionGenerator
{
    IMission Generate(MissionsData missionsData);
    IMission Replace(MissionsData missionsData, IMission previous);
}