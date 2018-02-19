using UnityEngine;

public class ParenterVolume : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        //did the player step into the volume
        if(other.tag == "Player")
        {
            //if so, make them a child of the platform
            other.transform.parent = transform;
        }
        //did a Phys object land in our volume
        else if(other.tag == "Phys")
        {
            //make them a child of the platform
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //did the player step out of the volume
        if(other.tag == "Player")
        {
            //if so, the player has no parent
            other.transform.parent = null;
        }
        //did a Phys object exit our volume
        else if (other.tag == "Phys")
        {
            //the object has no parent
            other.transform.parent = null;
        }
    }
}
