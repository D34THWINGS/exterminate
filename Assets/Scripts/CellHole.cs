using UnityEngine;

public class CellHole : MonoBehaviour {

	private SoundManager soundMng;
	public AudioClip fallDown;
	private bool isFalling;
	private Transform player;

	public void Start () {
		soundMng = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		isFalling = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			var playerComp = player.GetComponent<Player> ();
			if (!playerComp.IsLocked()) {
				soundMng.PlaySingle (fallDown);
				player = other.transform;
				playerComp.token.enabled = false;
				playerComp.Kill();
				isFalling = true;
			}
		}
	}

	private float timer;
	public float fallTime;
	public void Update () {

		if (isFalling) {
			// Rotate like Hell
			
			player.localRotation *= Quaternion.Euler(0,0, 10);
			// Scale down to hell
			player.localScale = new Vector3(0.8f-timer/2,0.8f-timer/2,0);
			timer += Time.deltaTime;

			print(timer);
			print(fallTime);

			// call Respawn
			if (fallTime < timer) {
				timer = 0;
				isFalling = false;
				player.GetComponent<Player> ().Respawn ();
				player = null;
			}

		
		}
	}

}
