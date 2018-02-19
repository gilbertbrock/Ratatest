using UnityEngine;

public class AvatarEvents : MonoBehaviour {

    public AudioSource footSound;
    public AudioClip[] footSFX;

	void PlayFootStep()
    {
        if(INIWorker.IniReadValue(INIWorker.Sections.Config, INIWorker.Keys.value1) == "pnp3")
            footSound.PlayOneShot(footSFX[Random.Range(0, footSFX.Length)], 0.15f);
    }
}
