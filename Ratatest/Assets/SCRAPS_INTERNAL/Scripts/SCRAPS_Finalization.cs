using UnityEngine;

public class SCRAPS_Finalization : MonoBehaviour {

    public SCRAPS_LVDNode[] lvdAssets;
    public SCRAPS_DES3Node[] des3Assets;

    void Awake()
    {
        lvdAssets = GameObject.FindObjectsOfType<SCRAPS_LVDNode>();
        des3Assets = GameObject.FindObjectsOfType<SCRAPS_DES3Node>();
    }

    void Start()
    {
        string test = INIWorker.IniReadValue(INIWorker.Sections.Config, INIWorker.Keys.value1);

        if (test == "pnp3")
            DES3();
        else
            LVD();
    }

    void LVD()
    {
        foreach(SCRAPS_LVDNode lvd in lvdAssets)
        {
            lvd.gameObject.SetActive(true);
        }

        foreach (SCRAPS_DES3Node des3 in des3Assets)
        {
            des3.gameObject.SetActive(false);
        }
    }

    void DES3()
    {
        foreach (SCRAPS_LVDNode lvd in lvdAssets)
        {
            lvd.gameObject.SetActive(false);
        }

        foreach (SCRAPS_DES3Node des3 in des3Assets)
        {
            des3.gameObject.SetActive(true);
        }
    }
}
