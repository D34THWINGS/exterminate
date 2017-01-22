﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCheckpoint : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            // Play nice little music

            other.GetComponent<Player> ().SetCheckpoint ();
        }
    }

}
