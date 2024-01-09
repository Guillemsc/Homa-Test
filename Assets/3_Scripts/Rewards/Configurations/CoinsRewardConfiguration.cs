using UnityEngine;

public sealed class CoinsRewardConfiguration : RewardConfiguration
{
    [SerializeField, Min(0)] public int Ammount;
}