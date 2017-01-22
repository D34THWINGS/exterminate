using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellHole : MonoBehaviour {

	private SoundManager soundMng;
	public AudioClip fallDown;

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log (other.name);
		if (other.tag == "Player") {
			// Fall to Hell

			soundMng.PlaySingle (fallDown);

			other.GetComponent<Player> ().Respawn ();
		}
	}
}
