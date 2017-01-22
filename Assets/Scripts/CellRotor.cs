using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellRotor : MonoBehaviour {

	private Player player;
	public int angle;

	private SoundManager soundMng;
	public AudioClip rotate;

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log (other.name);
		if (other.tag == "Player") {
			player = other.GetComponent<Player>();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") {
			player = null;
		}
	}

	public void Rotate() {
		if (player != null) {
			// Start animation 

			soundMng.PlaySingle (rotate);

			player.Rotate((float)angle); // A changer contre un Lerp pour plus de smoothness
		}
	}
}
