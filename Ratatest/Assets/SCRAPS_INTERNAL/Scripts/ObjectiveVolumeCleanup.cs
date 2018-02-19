using UnityEngine;

public class ObjectiveVolumeCleanup : MonoBehaviour {

    [Header("Objective IDs")]
    public string[] objectiveids;

    [Header("Who can activate this?")]
    public string neededTag = "Player";

    void OnTriggerExit(Collider other)
    {
        if(other.tag == neededTag)
        {
            for(int i = 0; i < objectiveids.Length; i++)
            {
                SCRAPS_ObjectiveList.instance.RemoveObjective(objectiveids[i]);
            }
            
            Destroy(gameObject);
        }
    }
}
