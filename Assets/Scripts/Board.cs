using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour {
    public IntVector2 Size;
    public List<CellConveyor> conveyors;
    public List<CellRotor> rotors;
    public bool endOfTurn;

    public void Start() {
        conveyors = new List<CellConveyor> (GetComponentsInChildren<CellConveyor>());
        rotors = new List<CellRotor> (GetComponentsInChildren<CellRotor>());
        endOfTurn = false;
    }


    public void Update() {
        if (endOfTurn) {
            conveyors.ForEach (c => c.Translate());
            rotors.ForEach (r => r.Rotate());
            endOfTurn = false;
        }
    }

}
