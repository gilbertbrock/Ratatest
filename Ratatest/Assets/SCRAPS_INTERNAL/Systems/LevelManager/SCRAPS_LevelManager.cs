using UnityEngine;
using UnityEngine.SceneManagement;

public class SCRAPS_LevelManager : MonoBehaviour {

    private int sceneCount = 0;

    void Awake()
    {
        //get all scenes loaded in the build settings
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }

	// Use this for initialization
	void Start () {

        // We don't want this running in the editor, only when we build the project out
        if (!Application.isEditor)
        {
            //do not load the menu or the hub... load anything else afterwards
            for(int i = 0; i < sceneCount; i++)
            {
                if(i == 0 || i == 1 || i == 2)
                {
                    //do not load
                }
                else
                {
                    SceneManager.LoadScene(i, LoadSceneMode.Additive);
                }
            }
        }
	}
}
