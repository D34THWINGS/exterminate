using UnityEngine;
using System.Collections.Generic;

public class CellCheckpoint : MonoBehaviour {

    public AudioClip passOver;
    private SoundManager soundMng;
    private List<string> players;
    public void Start () {
        soundMng = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        players = new List<string>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            var player = other.GetComponent<Player> ();
            if (!players.Contains(player.Id)) {
                players.Add (player.Id);
                soundMng.PlaySingle (passOver);
                player.SetCheckpoint ();
            }
        }
    }

}
