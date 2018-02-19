using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {

    [Header("Tracked Elements")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    public GameObject playerIcon;
    [SerializeField]
    private float zoomHeight;
    [SerializeField]
    private RectTransform mapPanel;
    [SerializeField]
    private float mapScale = 1.0f;
    private bool mapScaled = false;
    private Vector3 oldPos;
    [SerializeField]
    private RawImage map;

    public float heightPos;

    // Use this for initialization
    void Start() {

        heightPos = transform.position.y;

        //player = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            print("Cannot find object tagged - Player - !");
        }

        if (GameObject.FindGameObjectWithTag("PlayerIcon") != null)
        { 
            playerIcon = GameObject.FindGameObjectWithTag("PlayerIcon");
        }
        else
        {
            print("Cannot find object tagged - PlayerIcon - !");
        }

        oldPos = mapPanel.localPosition;

    }
	
	// Update is called once per frame
	void Update () {

        //SetDrawLayer();

        if (player != null)
        {
            Vector3 rot = player.transform.rotation.eulerAngles;
            playerIcon.transform.rotation = Quaternion.Euler(rot);
            playerIcon.transform.position = new Vector3(player.transform.position.x, transform.position.y - 0.5f, player.transform.position.z);

            transform.position = new Vector3(player.transform.position.x, heightPos, player.transform.position.z);
        }

        if (Input.GetKey(KeyCode.KeypadPlus))
            zoomHeight++;

        if (Input.GetKey(KeyCode.KeypadMinus))
            zoomHeight--;

        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            mapScaled = !mapScaled;

            if (mapScaled == true)
            {
                map.color = new Color(255, 255, 255, 0.6f);
                zoomHeight = 35;
            }
            else
            {
                map.color = new Color(255, 255, 255, 1.0f);
                zoomHeight = 10;
            }
        }

        if(mapScaled)
        {
            mapPanel.localPosition = new Vector3(0.5f,0.5f,0);
            mapPanel.sizeDelta = new Vector3(mapScale, mapScale, 1.0f);
        }
        else
        {
            mapPanel.localPosition = oldPos;
            mapPanel.sizeDelta = new Vector3(1.0f, 1.0f, 1.0f);
        }

        if (zoomHeight < 5)
            zoomHeight = 5;
    }

    void LateUpdate()
    {

        if(GetComponent<Camera>() != null)
        {
            if(GetComponent<Camera>().orthographicSize != zoomHeight)
            {
                GetComponent<Camera>().orthographicSize = zoomHeight;
            }

        }
    }

    void SetDrawLayer()
    {
        LevelBlockingTools.LevelBlockingMesh[] lbms = GameObject.FindObjectsOfType<LevelBlockingTools.LevelBlockingMesh>();

        foreach(LevelBlockingTools.LevelBlockingMesh lm in lbms)
        {
            if (lm.gameObject.layer != 28 && lm.gameObject.layer != 27)
                lm.gameObject.layer = 28;
        }

        Terrain[] ter = GameObject.FindObjectsOfType<Terrain>();

        foreach(Terrain t in ter)
        {
            if (t.gameObject.layer != 28)
                t.gameObject.layer = 28;
        }
    }
}
