using UnityEngine;
using System.Collections;

public class MapDetection : MonoBehaviour {

    public float near = 15.0f;
    private Minimap map;
    private float oldNear = 100.0f;
    public LayerMask ignoreLayer;
    private RaycastHit hit;

    void Start()
    {
        map = GameObject.FindObjectOfType<Minimap>();
    }

    void LateUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, ~ignoreLayer))
        {
            near = hit.point.y - 1.0f;
            //print("HIT");
        }
        else
        {
            near = oldNear;
            //print("...no hit");
        }

        if(map != null)
            map.heightPos = near;
    }
}
