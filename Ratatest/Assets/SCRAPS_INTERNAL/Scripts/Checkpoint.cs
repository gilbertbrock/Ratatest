using UnityEngine;

public class Checkpoint : MonoBehaviour {

    [Header("Display message when checkpoint is reached?")]
    public bool displayMessage = true;

    [Header("Name of location?")]
    public string locName = "Unknown Location";

    [Header("Where does the player respawn?")]
    public Transform respawnPos;

    private ZoneSystem zs;

    void Start()
    {
        zs = GameObject.FindObjectOfType<ZoneSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (other.GetComponent<Health>().respawnMarker != respawnPos)
            {
                if(displayMessage)
                    SCRAPS_MessageSystem.instance.NewMessage("", "<b>Checkpoint Activated</b> - "+locName, SCRAPS_MessageSystem.msgType.system);

                other.GetComponent<Health>().respawnMarker = respawnPos;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Color nColor = Color.yellow;
        nColor.a = 0.5f;
        Gizmos.color = nColor;
        Gizmos.DrawCube(new Vector3(respawnPos.position.x, respawnPos.position.y + 1, respawnPos.position.z), new Vector3(1, 2, 1));
    }
}
