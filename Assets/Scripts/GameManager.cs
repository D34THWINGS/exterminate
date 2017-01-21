using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

    public static bool isOver;
    public int maxPlayerNb;

    public Player[] playerPrefab;
    public Board boardPrefab;

    public Vector3[] startingPos; 

    private List<Player> players;
    private Board board;


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
        board = GetComponentInChildren<Board> ();
    }

    public void RestartGame () {
        BeginGame ();
    }

    public void GeneratePlayer (IEnumerable<string> playerIds) {
        
        Debug.Log (string.Join(", ", playerIds.ToArray()));

        players = playerIds.Select(p => {
            var data = p.Split('.');
            //var player = Instantiate (playerPrefab[int.Parse(data[1])]) as Player;
            var player = Instantiate (playerPrefab[0]) as Player;
            player.Coordinates = new IntVector2 (0, 0);
            player.Id = data[0];
            return player;
        }).ToList();
    }

    public Player GetPlayer (string playerId) {
        return players.Where(p => p.Id == playerId).Single();
    }

    public void Perform (IEnumerable<string> data) {
        

        string playerId = data.First();
        var player = GetPlayer (playerId);


        foreach (var action in data.Skip(1)) {
            var actionData = action.Split(':');
            switch ((Action) int.Parse(actionData[0])) {
                case Action.FWD1:
                    player.Move(1);
                    break;
                case Action.FWD2:
                    player.Move(2);
                    break;
                case Action.FWD3:
                    player.Move(3);
                    break;
                case Action.BKWD:
                    player.Move(-1);
                    break;
                case Action.LEFT:
                    player.Rotate(90);
                    break;
                case Action.RIGHT:
                    player.Rotate(-90);
                    break;
                case Action.UTURN:
                    player.Rotate(180);
                    break;
                default:
                    break;
            }
        }
    }
}

public enum Action {
    FWD1,
    FWD2,
    FWD3,
    BKWD,
    LEFT,
    RIGHT,
    UTURN
}
