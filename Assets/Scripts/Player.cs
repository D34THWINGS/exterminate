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
		Debug.Log (board.Size.ToString ());
		transform.localPosition = new Vector3 (coordinates.x - board.Size.x * 0.5f + 0.5f, coordinates.y - board.Size.y * 0.5f + 0.5f, -0.1f);
	}

	public void Rotate (float degrees) {
		transform.localRotation *= Quaternion.Euler(0, 0, degrees);
	}

	public void Move (int speed) {
		Vector2 direction = transform.forward;

		for (int i = 0; i < speed; i++) {
			Debug.DrawRay(transform.localPosition, direction, Color.blue);
			RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, direction, 2f, wallLayer);

			if (hit.collider == null) {

				var deltaX = transform.forward.x > transform.forward.y ? 1 : 0;
				var deltaY = transform.forward.x < transform.forward.y ? 1 : 0;

				Coordinates = new IntVector2 ( Coordinates.x + deltaX, Coordinates.y + deltaY);
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