using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool isOver;
    public int maxPlayerNb;

    public Player[] playerPrefab;
    public Board boardPrefab;

    public IntVector2[] startingPos; 

    private Player playerInstance;
    private Board boardInstance;


    public void Start () {
        BeginGame ();
    }

    public void Update () {
        if (isOver && Input.GetKeyDown (KeyCode.Space)) {
            RestartGame ();
        }
    }

    public void BeginGame () {
        isOver = false;
        boardInstance = Instantiate (boardPrefab);
        boardInstance.Generate ();
    }



    public void RestartGame () {
        BeginGame ();
    }


}
