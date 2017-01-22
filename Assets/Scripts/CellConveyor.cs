using UnityEngine;

public class CellConveyor : MonoBehaviour {

	private Player player;
	public CellConveyor nextConveyor;
	public Direction direction;
	public float conveySpeed;
	private SoundManager soundMng;
	public AudioClip translate;
	private Animator anim;


	public void Start () {
		anim = GetComponent<Animator>();
		soundMng = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			player = other.GetComponent<Player> ();
			//GameObject.Find("GameManager").GetComponent<GameManager> ().PlayEnvironment(); // TODO Delete
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
			// Start animation

			if (!soundMng.IsPlayingFx())
				soundMng.PlaySingle (translate);


			anim.SetTrigger ("Active");

			player.MoveLerp(direction, conveySpeed);
			if (nextConveyor != null) {
				nextConveyor.player = player;
				nextConveyor.Translate();
			}
			player = null;
			
		}
	}

	public void Translate (Player player) {
		this.player = player;
		Translate ();
	}

}

public enum Direction {
	Up, 
	Down, 
	Left, 
	Right
}

public static class Directions {
	private static IntVector2[] vectors = {
		new IntVector2(0, 1),
		new IntVector2(0, -1),
		new IntVector2(-1, 0),
		new IntVector2(1, 0),
	};

	public static IntVector2 ToIntVector2 (this Direction direction) {
		return vectors[(int)direction];
	}

}
