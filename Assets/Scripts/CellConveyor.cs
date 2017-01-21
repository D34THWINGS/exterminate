using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellConveyor : MonoBehaviour {

	private Transform player;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			player = other.transform;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") {
			player = null;
		}
	}

	public void Translate () {
		if (player != null) {
			// Do your thing mothafucka
		}
	}

}
