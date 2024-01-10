public sealed class NopRewardConfiguration : IRewardConfiguration
{
    public static readonly NopRewardConfiguration Instance = new();
    
    public string DisplayName => "Nop";
    
    NopRewardConfiguration(){}
}