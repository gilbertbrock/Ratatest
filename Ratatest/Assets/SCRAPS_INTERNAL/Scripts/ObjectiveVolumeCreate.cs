using UnityEngine;

public class ObjectiveVolumeCreate : MonoBehaviour {

    [Header("Objective to create")]
    public string objectiveID = "";
    public string objectiveText = "";

    [Header("Who can activate this?")]
    public string neededTag = "Player";

    [Header("Activate ObjectiveVolumeComplete after creation?")]
    public GameObject completeVolume;

    void Start () {
        if (completeVolume != null)
            completeVolume.SetActive(false);
    }
	
	void OnTriggerEnter(Collider other)
    {
        if(other.tag == neededTag)
        {
            SCRAPS_ObjectiveList.instance.CreateObjective(objectiveID, objectiveText);

            if(completeVolume != null)
                completeVolume.SetActive(true);

            Destroy(gameObject);
        }
    }
}
