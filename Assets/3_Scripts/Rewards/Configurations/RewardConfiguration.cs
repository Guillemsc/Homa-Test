using UnityEngine;

public abstract class RewardConfiguration : ScriptableObject, IRewardConfiguration
{
    [SerializeField] public string DisplayName = "Placeholder";

    string IRewardConfiguration.DisplayName => DisplayName;
}