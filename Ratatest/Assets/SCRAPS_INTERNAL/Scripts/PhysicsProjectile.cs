using UnityEngine;
using System.Collections;

public class PhysicsProjectile : Damage {

    //baseDamage

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag != "Player")
        {
            col.gameObject.SendMessage("ApplyDamage", baseDamage, SendMessageOptions.DontRequireReceiver);

            if(fxObj)
            {
                Instantiate(fxObj, col.contacts[0].point, Quaternion.LookRotation(col.contacts[0].normal));
            }

            if(sound != null)
            {
                GetComponent<AudioSource>().clip = sound;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
