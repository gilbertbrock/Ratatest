using UnityEngine;

public class OutdoorAmbience : MonoBehaviour {

    public AudioClip day, night;

	// Use this for initialization
	void Start () {
        AudioSource auS = GetComponent<AudioSource>();

        //check time of day
        string iniString = INIWorker.IniReadValue(INIWorker.Sections.Config, INIWorker.Keys.value2);
        iniString.ToLower();

        if (iniString == "night")
            auS.clip = night;
        else
            auS.clip = day;

        auS.Play();
    }
}
