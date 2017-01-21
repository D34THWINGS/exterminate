using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Cell : MonoBehaviour {
	
    private SpriteRenderer renderer;
    public Sprite[] sprites;

    
    public void Awake()
    {
        renderer = GetComponent<SpriteRenderer> ();
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
