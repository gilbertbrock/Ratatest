using UnityEngine;

public class Tutorial_Tree : MonoBehaviour {

    public GameObject topTree;
    public GameObject bottomTree;
    private bool didCut = false;
    private bool showOnce = false;
	
	void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (didCut == false)
            {
                //SCRAPS_MessageSystem.instance.NewMessage("Scrapper", "I can <b>interact</b> with this tree to cut it down by pressing <b>E</b>.", 1.0f);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    topTree.GetComponent<Rigidbody>().isKinematic = false;

                    if (showOnce == false)
                    {
                        //SCRAPS_MessageSystem.instance.NewMessage("Scrapper", "Felled with ease! These trees are brittle. I need to break them down further.", 3.0f);
                        showOnce = true;
                    }
                    didCut = true;
                }
            }
        }
    }
}
