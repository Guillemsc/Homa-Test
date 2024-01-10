using UnityEngine;

[CreateAssetMenu(fileName = "CoinsRewardConfiguration", menuName = "Rewards/CoinsRewardConfiguration", order = 1)]
public sealed class CoinsRewardConfiguration : RewardConfiguration
{
    [SerializeField, Min(0)] public int Ammount;

    public override string ToString()
    {
        return $"{DisplayName}->{Ammount}";
    }
}