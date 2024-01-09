using UnityEngine;

[CreateAssetMenu(fileName = "MissionDifficultyConfiguration", menuName = "MissionDifficultyConfiguration", order = 1)]
public sealed class MissionDifficultyConfiguration : ScriptableObject, IMissionDifficultyConfiguration
{
    [SerializeField] public string DisplayName = "Placeholder";

    string IMissionDifficultyConfiguration.DisplayName => DisplayName;
}