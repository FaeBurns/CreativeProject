using UnityEngine;

/// <summary>
/// A component reponsible for notifying the currently active <see cref="MissionCollectResources"/> of a deposited resource.
/// </summary>
public class NotifyActiveCollectionMissionOfDeposit : MonoBehaviour
{
    /// <summary>
    /// Takes in a resource and notifies the active mission about this deposit.
    /// </summary>
    /// <param name="resource">The reosurce being deposited.</param>
    public void OnDeposit(Resource resource)
    {
        Mission currentMission = GameManager.Instance.MissionManager.GetCurrentMission();

        if (currentMission is MissionCollectResources resourceMission)
        {
            resourceMission.OnResourceDeposited(resource);
        }
    }
}