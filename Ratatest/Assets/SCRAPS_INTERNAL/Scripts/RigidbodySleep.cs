using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody))]
public class RigidbodySleep : MonoBehaviour {

	void Awake () {

        GetComponent<Rigidbody>().Sleep();
	
	}
}
