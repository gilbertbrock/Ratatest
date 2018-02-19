using UnityEngine;
using System.Collections;

public class ExplosionDamage : Damage
{
	public bool activateOnCollision;
	public float explosionDelay = 0;
	public float explosionRadius = 5;
	public float explosionForce = 20;
	
	void OnCollisionEnter()
	{	
		if(activateOnCollision)
		{
			StartCoroutine( WaitToDestroy() );
		}
	}
	
	IEnumerator WaitToDestroy()
	{
		yield return new WaitForSeconds(explosionDelay);

		DoDamage();
	}

	public void DoDamage()
	{
		Debug.Log("Explosion Damage");
		if(fxObj)
		{
			Instantiate(fxObj, transform.position, transform.rotation);
		}

        if (sound != null)
        {
            GetComponent<AudioSource>().clip = sound;
            GetComponent<AudioSource>().Play();
        }

        foreach (Collider col in Physics.OverlapSphere(transform.position,explosionRadius))
		{
			if(col != GetComponent<Collider>() && col.GetComponent<Rigidbody>())
			{
				Vector3 vectorToObject = col.transform.position - transform.position;
				float fallOffMultiplier = 1 - Mathf.Clamp( vectorToObject.magnitude / explosionRadius, 0.1f, 1.0f );
				col.gameObject.SendMessage("ApplyDamage", baseDamage * fallOffMultiplier, SendMessageOptions.DontRequireReceiver);
				col.GetComponent<Rigidbody>().AddForce( vectorToObject.normalized * explosionForce * fallOffMultiplier, ForceMode.Impulse);
			}
		}

		Destroy(gameObject);
	}
}

