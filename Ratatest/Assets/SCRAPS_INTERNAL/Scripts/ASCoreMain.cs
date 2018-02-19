using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASCoreMain : MonoBehaviour {

    [Header("UI SYSTEM")]
    public GameObject gradingCanvas;
    public Text timeEstimateTxt;
    //private SetupVolumeTimeEstimator volEst;
    private GameObject mapProjector;
    private bool toggleMap = false;

    [Header("GOD CAMERA SYSTEM")]
    public bool toggleCam = false;
    public GameObject playerAvatar;
    public GameObject gravnullSys;
    public GameObject playerCamera;
    public GameObject godCamera;
    private bool isPlayer = true;

    public Checkpoint[] myPoints;

    void Awake()
    {
        //GenerateASCoreConsole();
        gradingCanvas.SetActive(false);
    }

    void Start()
    {
        myPoints = GameObject.FindObjectsOfType<Checkpoint>();

        if (playerAvatar == null)
            playerAvatar = GameObject.FindWithTag("Player");

        if (gravnullSys == null)
            gravnullSys = GameObject.Find("GravnullSystem");

        if (playerCamera == null)
            playerCamera = GameObject.Find("ASCoreCamera");

        if (mapProjector == null)
            mapProjector = GameObject.Find("ASCoreMapProjector");
    }

    public GameObject GetChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

    // Update is called once per frame
    void Update () {

        //Show or Hide the Console!
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            ASCoreConsole();
        }

        //volEst = GameObject.FindObjectOfType<SetupVolumeTimeEstimator>();
        
        GodCamera();
        MapSystem();
        //TimerSystem();

        /*if(volEst != null)
        {
            int minutes = Mathf.FloorToInt(volEst.levelTimeEstimate / 60F);
            int seconds = Mathf.FloorToInt(volEst.levelTimeEstimate - minutes * 60);
            string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            timeEstimateTxt.text = niceTime;
        }
        else
        {
            timeEstimateTxt.text = "Time Estimator Not Found";
        }*/

    }

    public void ASCoreConsole()
    {
        gradingCanvas.SetActive(!gradingCanvas.activeSelf);
        Cursor.visible = gradingCanvas.activeSelf;

        if (gradingCanvas.activeSelf == true)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }

    void GodCamera()
    {
        if (toggleCam)
        {
            //assume player starts first
            if (isPlayer)
            {
                godCamera.transform.position = playerCamera.transform.position;
                godCamera.transform.rotation = playerCamera.transform.rotation;

                godCamera.SetActive(true);

                playerCamera.SetActive(false);
                playerAvatar.SetActive(false);
                gravnullSys.SetActive(false);

                isPlayer = false;
            }
            else
            {
                playerAvatar.transform.position = godCamera.transform.position;
                playerCamera.transform.position = godCamera.transform.position;

                playerAvatar.transform.rotation = godCamera.transform.rotation;
                playerCamera.transform.rotation = godCamera.transform.rotation;

                playerCamera.SetActive(true);
                playerAvatar.SetActive(true);
                gravnullSys.SetActive(true);

                godCamera.SetActive(false);

                isPlayer = true;
            }

            toggleCam = false;
        }
    }

    void MapSystem()
    {
        if(toggleMap)
        {
            if(mapProjector.layer != LayerMask.NameToLayer("Projection"))
            {
                //mapProjector.layer = 1;
                mapProjector.layer = LayerMask.NameToLayer("Projection");
            }
            else if(mapProjector.layer != LayerMask.NameToLayer("LBM"))
            {
                //mapProjector.layer = 1;
                mapProjector.layer = LayerMask.NameToLayer("LBM");
            }

            toggleMap = false;
        }
    }

    public void ToggleCam()
    {
        print("Toggle Cam");
        toggleCam = true;
    }

    public void ToggleProjection()
    {
        print("Toggle Map");
        toggleMap = true;
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
