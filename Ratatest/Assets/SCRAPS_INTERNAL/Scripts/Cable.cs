using UnityEngine;

public class Cable : MonoBehaviour {

    private LineRenderer lRend;

    [Header("What are we connected to?")]
    public Transform connect;

    private bool didBreak = false;

    [Header("Joint broken... what else is broken?")]
    public GameObject[] destroyObjs;

	// Use this for initialization
	void Start () {
        lRend = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (didBreak == false)
        {
            lRend.SetPosition(0, connect.position);
            lRend.SetPosition(1, transform.position);
        }
        else
        {
            lRend.enabled = false;
        }
    }

    void OnJointBreak()
    {
        didBreak = true;

        foreach(GameObject go in destroyObjs)
        {
            Destroy(go);
        }
    }
}
