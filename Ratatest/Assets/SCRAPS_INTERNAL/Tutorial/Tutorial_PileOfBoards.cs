using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_PileOfBoards : MonoBehaviour {

    private Rigidbody[] boards;
    private bool spawned = false;
    private bool isHere = false;
    private Scraps_HUD_Inventory inv;
    public GameObject outpostTrigger;

	// Use this for initialization
	void Start () {
        outpostTrigger.SetActive(false);

        boards = GetComponentsInChildren<Rigidbody>();

        inv = GameObject.FindObjectOfType<Scraps_HUD_Inventory>();

        foreach(Rigidbody bo in boards)
        {
            bo.gameObject.SetActive(false);
        }

        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        if(isHere == true && spawned == false)
        {
            if(Input.GetKeyDown(KeyCode.E) && inv.GetPickupAmount("Wood Board") >= 12)
            {
                foreach (Rigidbody bo in boards)
                {
                    bo.transform.parent = null;
                    bo.gameObject.SetActive(true);
                }

                inv.RemoveAllPickups("Wood Board");
                outpostTrigger.SetActive(true);
                spawned = true;
            }
        }
    }
	
	void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (spawned == false && inv.GetPickupAmount("Wood Board") >= 12)
            {
                GetComponent<MeshRenderer>().enabled = true;
                //SCRAPS_MessageSystem.instance.NewMessage("An empty location", "I can <b>interact</b> with this location to stack wood boards by pressing <b>E</b>.", 1.0f);
                isHere = true;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<MeshRenderer>().enabled = false;
            isHere = false;
        }
    }
}
