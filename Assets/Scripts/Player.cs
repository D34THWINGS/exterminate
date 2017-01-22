using UnityEngine;

public class Player : MonoBehaviour {

	public float rotationSpeed;
	public float moveSpeed;
	private IntVector2 boardSize;
	public LayerMask wallLayer;
	private IntVector2 lastCheckPoint;

	private Quaternion finalRotation;
	private float speedR;
	
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
		boardSize = GameObject.Find ("GameManager").GetComponent<GameManager>().Size;
	}


	private Vector3 finalPosition;
	private float speedT;
	private void SetLocation () {
		finalPosition = new Vector3 (coordinates.x - boardSize.x * 0.5f + 0.5f, coordinates.y - boardSize.y * 0.5f + 0.5f, transform.localPosition.z);
		speedT = moveSpeed;
	}

	public void Rotate (float angle, float? speed) {
		finalRotation = transform.localRotation * Quaternion.Euler(0,0, angle);
		if (speed.HasValue)
			speedR = speed.Value;
		else
			speedR = rotationSpeed;
	}

	public void Move (int speed) {
		Vector2 direction = transform.up;

		for (int i = 0; i < speed; i++) {
			RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, direction, 1f, wallLayer);
			if (hit.collider == null) {
				Coordinates = new IntVector2 ( Coordinates.x + Mathf.RoundToInt(transform.up.x), Coordinates.y + Mathf.RoundToInt(transform.up.y));
			}
		}
	}

	public void MoveLerp (Direction direction, float speed) {
		Coordinates += direction.ToIntVector2();
        speedT = speed;
	}

	public void SetCheckpoint () {
		lastCheckPoint = Coordinates;
	}

	public void Respawn () {
		Coordinates = lastCheckPoint;
		transform.localScale = new Vector3 (0.8f, 0.8f, 0);
		transform.localRotation = Quaternion.identity;
	}

	public void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			Move (1);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			Rotate (-90, null);
		} else if (Input.GetKeyDown (KeyCode.Q)) {
			Rotate (90, null);
		} else if (Input.GetKeyDown (KeyCode.S)) {
			Rotate (180, null);
		}
	}
	
	void FixedUpdate()
	{
		transform.localRotation = Quaternion.Lerp(transform.localRotation, finalRotation, speedR * Time.deltaTime);
		transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition, speedT * Time.deltaTime);		
	}

}