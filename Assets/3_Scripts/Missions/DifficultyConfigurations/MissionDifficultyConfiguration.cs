using UnityEngine;

[CreateAssetMenu(fileName = "MissionDifficultyConfiguration", menuName = "MissionDifficultyConfiguration", order = 1)]
public sealed class MissionDifficultyConfiguration : ScriptableObject, IMissionDifficultyConfiguration
{
    [SerializeField] public string DisplayName = "Placeholder";
    [SerializeField] public int DifficultyIndex;

    string IMissionDifficultyConfiguration.DisplayName => DisplayName;
    int IMissionDifficultyConfiguration.DifficultyIndex => DifficultyIndex;
}