using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Campfire : MonoBehaviour {

    private int numberSticks = 0;
    public GameObject fire;
    private bool fireStart = false;

    void Start()
    {
        fire.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Tutorial_Stick>() != null && fireStart == false)
        {
            Destroy(other.gameObject);
            numberSticks++;
        }
    }

    void Update()
    {
        if(numberSticks >= 5  && fireStart == false)
        {
            fire.SetActive(true);
            SCRAPS_ObjectiveList.instance.CompleteObjective("lvd_startfire");
            fireStart = true;
        }
    }
}
