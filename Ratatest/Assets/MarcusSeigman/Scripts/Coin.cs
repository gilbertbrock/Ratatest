using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public AudioClip collectionNoise;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.GetComponent<AudioSource>().Play();
            GameManager.Instance.GetCoin();
            GetComponent<MeshRenderer>().enabled = false;
            Invoke("Disable", collectionNoise.length);
        }
    }

    private void Disable ()
    {
        gameObject.SetActive(false);
    } 
}
