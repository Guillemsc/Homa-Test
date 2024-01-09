using UnityEngine;

[CreateAssetMenu(fileName = "ReachIntMissionConfiguration", menuName = "Missions/ReachIntMissionConfiguration", order = 1)]
public sealed class ReachIntMissionConfiguration : MissionConfiguration
{
    [SerializeField] public int Ammount = 1;
    
    public override TReturn Accept<TReturn>(IMissionConfigurationVisitor<TReturn> visitor)
    {
        return visitor.Visit(this);
    }
}