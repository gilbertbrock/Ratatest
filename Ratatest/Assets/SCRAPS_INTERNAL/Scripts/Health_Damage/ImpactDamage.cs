using UnityEngine;
using System.Collections;

public class ImpactDamage : Damage
{
	public float destroyDelay = 0;

	void OnCollisionEnter(Collision col)
	{
//		if(collision.relativeVelocity.magnitude > 2)
//		{
		Debug.Log("Impact Damage");
		col.gameObject.SendMessage("ApplyDamage", baseDamage, SendMessageOptions.DontRequireReceiver);
//		}

		if(fxObj)
		{
			Instantiate(fxObj, col.contacts[0].point, Quaternion.LookRotation(col.contacts[0].normal));
		}

        if (sound != null)
        {
            GetComponent<AudioSource>().clip = sound;
            GetComponent<AudioSource>().Play();
        }

        StartCoroutine( WaitToDestroy() );
	}

	IEnumerator WaitToDestroy()
	{
		yield return new WaitForSeconds(destroyDelay);
		Destroy(gameObject);
	}
}

