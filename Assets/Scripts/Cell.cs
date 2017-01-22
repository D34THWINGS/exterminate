using UnityEngine;
using Random = UnityEngine.Random;

public class Cell : MonoBehaviour {
	
    public Sprite[] sprites;

    
    public void Awake()
    {
        var renderer = GetComponent<SpriteRenderer> ();
        renderer.sprite = sprites[Random.Range(0, sprites.Length)];

        var angle = Random.Range(0, 10);

        if (angle > 7f) {
            transform.Rotate (Vector3.forward * 90);
        } else if (angle > 5f) {
            transform.Rotate (Vector3.forward * 180);
        }  else if (angle > 3f) {
            transform.Rotate (Vector3.forward * -90);
        }


    }

}
