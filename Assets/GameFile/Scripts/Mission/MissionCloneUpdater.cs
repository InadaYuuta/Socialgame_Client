using UnityEngine;

public class MissionCloneUpdater : MonoBehaviour
{
    void Update()
    {
        MissionCloneManager[] all = FindObjectsOfType<MissionCloneManager>();
        foreach (MissionCloneManager clone in all)
        {
            clone.UpdateMission();
        }
    }
}
