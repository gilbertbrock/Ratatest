using UnityEngine;
using System.Collections;

public class Scraps_HUD_Pickup : MonoBehaviour {

    public string typeName;
    [HideInInspector]
    public Texture2D icon;
    // public ParticleSystem particlePop;
    public bool isUnknown = false;
    private Scraps_HUD_Inventory inventory = null;

	// Use this for initialization
	void Start () {
        handShakeWithInventory();
	}
	
	// Update is called once per frame
	void Update () {
        handShakeWithInventory();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inventory.CountPickup(this);
            Destroy(gameObject);
        }
		/*
        ParticleSystem newSystem = Instantiate(particlePop, transform.position, particlePop.transform.rotation) as ParticleSystem;
        Destroy(gameObject, particlePop.duration * 0.25f);
        Destroy(newSystem.gameObject, particlePop.duration + 0.1f);
		*/
    }

    void handShakeWithInventory()
    {
        if(inventory == null)
        {
            inventory = FindObjectOfType<Scraps_HUD_Inventory>();
            if(inventory != null)
            {
                inventory.AccountForPickup(this);
            }
        }
    }
}
