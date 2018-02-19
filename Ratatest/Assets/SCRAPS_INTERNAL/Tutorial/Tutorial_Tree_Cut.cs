using UnityEngine;

public class Tutorial_Tree_Cut : MonoBehaviour {

    private bool didCut = false;
    public GameObject[] boards;
    public GameObject[] sticks;
    private Rigidbody rb;
    private bool isHere = false;
    private bool canCut = false;

    private float fallTimer = 3.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        foreach(GameObject bo in boards)
        {
            bo.SetActive(false);
        }

        foreach (GameObject st in sticks)
        {
            st.SetActive(false);
        }
    }

    void Update()
    {
        if(rb.isKinematic == false)
        {
            fallTimer -= Time.deltaTime;

            if(fallTimer <= 0)
            {
                fallTimer = 0;
                canCut = true;
            }
        }


        if (didCut == false && canCut == true)
        {
            if (isHere)
            {
                //SCRAPS_MessageSystem.instance.NewMessage("Scrapper", "I can <b>interact</b> with this tree to cut it into boards by pressing <b>E</b>.", 1.0f);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    foreach (GameObject bo in boards)
                    {
                        bo.SetActive(true);
                        bo.transform.parent = null;
                    }

                    foreach (GameObject st in sticks)
                    {
                        st.SetActive(true);
                        st.transform.parent = null;
                    }

                    //SCRAPS_MessageSystem.instance.NewMessage("Scrapper", "I can <b>collect</b> the boards, and then use my <b>gravnull</b> to lift the sticks into the fire by holding <b>LEFT MOUSE BUTTON</b> down and aiming at them.", 10.0f);
                    didCut = true;
                }
            }
        }
        else if(didCut == true)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && rb.isKinematic == false)
        {
            isHere = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isHere = false;
        }
    }
}
