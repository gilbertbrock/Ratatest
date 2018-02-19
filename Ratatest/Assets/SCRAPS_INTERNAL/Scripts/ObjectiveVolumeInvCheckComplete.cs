using UnityEngine;

public class ObjectiveVolumeInvCheckComplete : MonoBehaviour {

    [Header("Objective to complete")]
    public string objectiveID = "";

    private Scraps_HUD_Inventory inv;
    public string itemType = "";
    public int itemNum = 0;

    void Start () {
        inv = FindObjectOfType<Scraps_HUD_Inventory>();
	}

    void Update()
    {
        if(inv.GetPickupAmount(itemType) >= itemNum)
        {
            SCRAPS_ObjectiveList.instance.CompleteObjective(objectiveID);
            Destroy(gameObject);
        }
    }
}
