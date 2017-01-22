using UnityEngine;

public class CellRotor : MonoBehaviour {

	private Player player;
	public int angle;
	public float rotationSpeed;

	private SoundManager soundMng;
	public AudioClip rotate;
	private Animator anim;

	public void Start () {
		anim = GetComponent<Animator>();
		soundMng = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			player = other.GetComponent<Player>();
			//GameObject.Find("GameManager").GetComponent<GameManager> ().PlayEnvironment(); // TODO Delete
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
			anim.SetTrigger("Active");
			soundMng.PlaySingle (rotate);

			player.Rotate((float)angle, rotationSpeed); // A changer contre un Lerp pour plus de smoothness
		}
	}
}
