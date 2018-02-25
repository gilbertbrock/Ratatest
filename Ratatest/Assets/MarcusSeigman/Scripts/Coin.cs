using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public AudioClip collectionNoise;
    private GameObject cheeseChild;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.GetComponent<AudioSource>().Play();
            GameManager.Instance.GetCoin();
           
           GetComponentInChildren<Transform>().gameObject.SetActive(false);
            Invoke("Disable", collectionNoise.length);
        }
    }

    private void Disable ()
    {
        gameObject.SetActive(false);
    } 
}
