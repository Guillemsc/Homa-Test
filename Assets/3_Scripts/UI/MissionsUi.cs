using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            MissionEntryUiView entryInstance = Instantiate(MissionEntryPrefab, MissionEntriesParent);
            entryInstance.NameLabel.text = mission.Configuration.DisplayName;
            entryInstance.DifficultyLabel.text = mission.Configuration.DifficultyConfiguration.DisplayName;
            entryInstance.ProgressLabel.text = mission.GetDisplayProgress();
            
            m_entries.Add(entryInstance);
        }
    }
}