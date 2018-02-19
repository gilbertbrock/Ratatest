using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMotor : MonoBehaviour {

    // Use this for initialization
    public GameObject Player;
    public Transform lookAt;
    public Vector3 offset;
    public Camera mainCam;
    public float camSize;
    private bool isZoomed;
    private Animator anim;

    public bool isMoving { set;  get;}

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        
        if (!isMoving)
            return;
        if(!isZoomed)
        Invoke("ZoomCamera", 0);
        transform.position = new Vector3(Player.transform.position.x + offset.x, transform.position.y, Player.transform.position.z + offset.z);        // Vector3 desiredPosition = Player.transform.position + offset;
        //desiredPosition.z = 10;
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
    }

    private void ZoomCamera()
    {
        anim.SetTrigger("ZoomCamera");
        isZoomed = true;
    }

}
