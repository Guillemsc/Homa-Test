using System;
using UnityEngine;

public abstract class MissionConfiguration : ScriptableObject, IMissionConfiguration
{
    [SerializeField] public string Uid = Guid.NewGuid().ToString();
    [SerializeField] public MissionDifficultyConfiguration DifficultyConfiguration;
    [SerializeField] public RewardConfiguration RewardConfiguration;
    [SerializeField] public MissionType MissionType;
    [SerializeField] public string DisplayName = "Placeholder";

    string IMissionConfiguration.Uid => Uid;
    IMissionDifficultyConfiguration IMissionConfiguration.DifficultyConfiguration => DifficultyConfiguration;
    IRewardConfiguration IMissionConfiguration.RewardConfiguration => RewardConfiguration;
    MissionType IMissionConfiguration.MissionType => MissionType;
    string IMissionConfiguration.DisplayName => DisplayName;
    public abstract TReturn Accept<TReturn>(IMissionConfigurationVisitor<TReturn> visitor);
}