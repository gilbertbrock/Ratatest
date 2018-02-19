using UnityEngine;

public class LightingVolume : MonoBehaviour {

    public bool goingIndoors = false;

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(SCRAPS_LightingManager.isDaytime && SCRAPS_LightingManager.currentLighting == SCRAPS_LightingManager.lType.indoors && goingIndoors == false)
            {
                SCRAPS_LightingManager.currentLighting = SCRAPS_LightingManager.lType.outdoorsDay;
            }
            else if (!SCRAPS_LightingManager.isDaytime && SCRAPS_LightingManager.currentLighting == SCRAPS_LightingManager.lType.indoors && goingIndoors == false)
            {
                SCRAPS_LightingManager.currentLighting = SCRAPS_LightingManager.lType.outdoorsNight;
            }
            else
            {
                SCRAPS_LightingManager.currentLighting = SCRAPS_LightingManager.lType.indoors;
            }
        }
    }
}
