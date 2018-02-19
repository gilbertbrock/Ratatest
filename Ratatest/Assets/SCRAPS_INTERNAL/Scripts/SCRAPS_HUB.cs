using UnityEngine;

public class SCRAPS_HUB : MonoBehaviour {

    public GameObject placeholder;
    public GameObject final;

	// Use this for initialization
	void Awake () {
        placeholder.SetActive(false);
        Vector3 newPos = final.transform.position;
        newPos.y = 0;
        final.transform.position = newPos;
	}
}
