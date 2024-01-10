using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class MissionsUi : MonoBehaviour
{
    [SerializeField] public Transform MissionEntriesParent;
    [SerializeField] public MissionEntryUiView MissionEntryPrefab;

    readonly List<MissionEntryUiView> m_entries = new();
    
    public void Refresh()
    {
        RefreshEntries();
    }
    
    void RefreshEntries()
    {
        foreach (MissionEntryUiView entry in m_entries)
        {
            Destroy(entry.gameObject);
        }
        
        m_entries.Clear();
        
        IEnumerable<IMission> missions = MissionsService.Instance.ActiveMissions.OrderBy(
            m => m.Configuration.DifficultyConfiguration.DifficultyIndex
        );
        
        foreach (IMission mission in missions)
        {
            bool completed = mission.IsCompleted();
            
            MissionEntryUiView entryInstance = Instantiate(MissionEntryPrefab, MissionEntriesParent);
            entryInstance.NameLabel.text = mission.Configuration.DisplayName;
            entryInstance.DifficultyLabel.text = mission.Configuration.DifficultyConfiguration.DisplayName;
            entryInstance.ProgressLabel.text = mission.GetDisplayProgress();
            
            entryInstance.ProgressLabel.gameObject.SetActive(!completed);
            entryInstance.ClaimRewardButton.gameObject.SetActive(completed);

            void ClaimButtonClick() => WhenClaimButtonClick(entryInstance);
            entryInstance.ClaimRewardButton.onClick.AddListener(ClaimButtonClick);

            entryInstance.LinkedMission = mission;
            
            m_entries.Add(entryInstance);
        }
    }

    void WhenClaimButtonClick(MissionEntryUiView entryInstance)
    {
        IMission mission = entryInstance.LinkedMission;
        
        // We would actually reward stuff here
        Debug.Log($"Yout got a reward: {mission.Configuration.RewardConfiguration}");
        
        MissionsService.Instance.ReplaceMission(mission);

        RefreshEntries();
    }

    public void WhenCloseMissionUiClick()
    {
        gameObject.SetActive(false);
    }
}