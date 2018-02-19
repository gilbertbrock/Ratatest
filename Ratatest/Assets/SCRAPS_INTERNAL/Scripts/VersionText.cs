using UnityEngine;
using UnityEngine.UI;

public class VersionText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = INIWorker.IniReadValue(INIWorker.Sections.Version, INIWorker.Keys.value1);
    }
}
