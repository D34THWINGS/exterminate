using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellHole : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log (other.name);
		if (other.tag == "Player") {
			// Fall to Hell

			other.GetComponent<Player> ().Respawn ();
		}
	}
}
