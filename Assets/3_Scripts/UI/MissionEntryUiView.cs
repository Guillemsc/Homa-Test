using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class MissionEntryUiView : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI NameLabel;
    [SerializeField] public TextMeshProUGUI DifficultyLabel;
    [SerializeField] public TextMeshProUGUI ProgressLabel;
    [SerializeField] public Button ClaimRewardButton;

    public IMission LinkedMission;
}