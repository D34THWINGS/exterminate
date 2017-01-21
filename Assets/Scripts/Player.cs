using UnityEngine;

public class Player : MonoBehaviour {

	private Board board;
	public LayerMask wallLayer;
	private IntVector2 coordinates;
	public IntVector2 Coordinates {
		get {
			return coordinates;
		}
		set {
			coordinates = value;
			SetLocation ();
		}
	}

	public string Id { get; set; }

	public void Awake () {
		board = GameObject.Find ("Board").GetComponent<Board>();
	}


	private void SetLocation () {
		transform.localPosition = new Vector3 (coordinates.x - board.Size.x * 0.5f + 0.5f, coordinates.y - board.Size.y * 0.5f + 0.5f, -0.1f);
	}

	public void Rotate (float degrees) {
		transform.localRotation *= Quaternion.Euler(0, 0, degrees);
	}

	public void Move (int speed) {
		Vector2 direction = transform.up;

		for (int i = 0; i < speed; i++) {
			RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, direction, 1f, wallLayer);
			if (hit.collider == null) {
				Debug.Log(coordinates);
				Coordinates = new IntVector2 ( Coordinates.x + (int)transform.up.x, Coordinates.y + (int)transform.up.y);
			}
		}
	}

	public void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			Move (1);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			Rotate (-90);
		} else if (Input.GetKeyDown (KeyCode.Q)) {
			Rotate (90);
		} else if (Input.GetKeyDown (KeyCode.S)) {
			Rotate (180);
		}
	}

}