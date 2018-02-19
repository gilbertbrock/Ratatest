using UnityEngine;

public class CompassRotation : MonoBehaviour {

    public Transform follow;

    void Start()
    {
        if (follow == null)
            follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

	// Update is called once per frame
	void Update () {
        Vector3 newRot = follow.rotation.eulerAngles;
        newRot.x = 0;
        newRot.z = 0;

        transform.rotation = Quaternion.Euler(newRot);
	}
}
