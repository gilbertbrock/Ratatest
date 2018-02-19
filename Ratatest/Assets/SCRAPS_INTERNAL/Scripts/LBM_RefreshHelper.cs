using UnityEngine;

public class LBM_RefreshHelper : MonoBehaviour {

    [Header("Allow this to gather LBMs on Start?")]
    public bool enable = false;

    void Start()
    {
        if (enable)
        {
            LevelBlockingTools.LevelBlockingMesh[] LBMs = GameObject.FindObjectsOfType<LevelBlockingTools.LevelBlockingMesh>();

            foreach (LevelBlockingTools.LevelBlockingMesh lb in LBMs)
            {
                lb.gameObject.transform.parent = transform;
            }
        }
    }

}
