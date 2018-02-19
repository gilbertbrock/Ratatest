using UnityEngine;

public class ColSound : MonoBehaviour {

    private AudioSource aSource;
    private Rigidbody rb;
    public AudioClip colSFX;

	// Use this for initialization
	void Start () {
        if (INIWorker.IniReadValue(INIWorker.Sections.Config, INIWorker.Keys.value1) == "pnp3")
        {
            gameObject.AddComponent<AudioSource>();
            aSource = GetComponent<AudioSource>();
            aSource.spatialBlend = 1.0f;
            rb = GetComponent<Rigidbody>();
        }
        else
        {
            enabled = false;
        }
	}
	
	void OnCollisionEnter(Collision col)
    {
        if(rb.velocity.magnitude > 2.15f)
            aSource.PlayOneShot(colSFX, 1.0f);
    }
}
