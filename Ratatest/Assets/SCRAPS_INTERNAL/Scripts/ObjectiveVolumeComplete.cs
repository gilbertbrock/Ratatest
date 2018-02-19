using UnityEngine;

public class ObjectiveVolumeComplete : MonoBehaviour {

    [Header("Objective to complete")]
    public string objectiveID = "";

    [Header("Who can activate this?")]
    public string neededTag = "Player";
	
	void OnTriggerEnter(Collider other)
    {
        if(other.tag == neededTag)
        {
            SCRAPS_ObjectiveList.instance.CompleteObjective(objectiveID);
            Destroy(gameObject);
        }
    }
}
