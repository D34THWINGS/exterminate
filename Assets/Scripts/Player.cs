using UnityEngine;
using System;

public class Player : MonoBehaviour {

	public float rotationSpeed;
	public float moveSpeed;
	private IntVector2 boardSize;
	public LayerMask wallLayer;
	private IntVector2 lastCheckPoint;

	private Quaternion finalRotation;
	private float speedR;
	public SpriteRenderer token;
	
	public void SetColor(Color color) {
		token.color = color;
	}

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

	private Quaternion originRotation;
	private Vector3 originCoordinates;
	private AnimationTypes animationType;
	private float animationStartTime = 0f;
	public float animationDuration = 300f;
	public delegate void HandleAnimationEnd();
	public event HandleAnimationEnd onAnimationEnd;

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
		Debug.Log("Init Rotate: " + DateTime.Now.Millisecond);
		animationStartTime = DateTime.Now.Millisecond;
		originRotation = transform.localRotation;
		animationType = AnimationTypes.Rotate;
		finalRotation = transform.localRotation * Quaternion.Euler(0,0, angle);
		if (speed.HasValue)
			speedR = speed.Value;
		else
			speedR = rotationSpeed;
	}

	public void Move (int speed) {
		Debug.Log("Init Move: " + DateTime.Now.Millisecond);
		animationStartTime = DateTime.Now.Millisecond;
		originCoordinates = transform.localPosition;
		animationType = AnimationTypes.Translate;
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
		//token.enabled = true;
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

	public void ResetAnimationHandlers() {
		onAnimationEnd = null;
	}
	
	void FixedUpdate() {
		if (animationStartTime != 0f) {
			var progress = Mathf.Min(Mathf.Abs(DateTime.Now.Millisecond - animationStartTime) / animationDuration, 1f);
			Debug.Log (progress);
			if (animationType == AnimationTypes.Rotate) {
				transform.localRotation = Quaternion.Lerp(originRotation, finalRotation, 2f);
			}
			if (animationType == AnimationTypes.Translate) {
				transform.localPosition = Vector3.Lerp(originCoordinates, finalPosition, 2f);	
			}

			if (progress == 1f) {
				animationStartTime = 0f;
				if (onAnimationEnd != null) {
					onAnimationEnd();
				}
			}
		}
	}

}

public enum AnimationTypes {
	Translate,
	Rotate
}
