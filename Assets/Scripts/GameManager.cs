using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

    public bool isOver;
    public Player playerPrefab;
    public Sprite[] playerSprites;
    public AudioClip[] spawnSound;
    public Color[] playerColor;
    public IntVector2 Size;

    private List<CellConveyor> conveyors;
    private List<CellRotor> rotors;
    private List<Player> players;
    private SocketClient socket;
    private SoundManager soundMng;
    private int nbPlayer;


    public void Start () {
        conveyors = new List<CellConveyor> (GameObject.Find("Board").GetComponentsInChildren<CellConveyor>());
        rotors = new List<CellRotor> (GameObject.Find("Board").GetComponentsInChildren<CellRotor>());
        socket = GetComponent<SocketClient>();
        soundMng = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        BeginGame ();
    }

    public void Update () {
        if (isOver && Input.GetKeyDown (KeyCode.Space)) {
            RestartGame ();
        }
    }

    public void BeginGame () {
        isOver = false;
        GeneratePlayer(new List<string>(){"aaa:0"});
    }

    public void RestartGame () {
        BeginGame ();
    }

    public void GeneratePlayer (IEnumerable<string> playerIds) {
        soundMng.RandomizeSfx(spawnSound);

        players = playerIds.Select(p => {
            var data = p.Split(':');
            var player = Instantiate (playerPrefab) as Player;
            player.Coordinates = new IntVector2 (0, 0);
            player.GetComponent<SpriteRenderer>().sprite = playerSprites[int.Parse(data[1])];
            player.SetColor(playerColor[nbPlayer]);
            player.Id = data[0];
            nbPlayer++;
            return player;
        }).ToList();
    }

    public Player GetPlayer (string playerId) {
        return players.Where(p => p.Id == playerId).Single();
    }

    public void HandlePlayerOrders(IEnumerable<string> data) {
        foreach (var payload in data) {
            Perform (payload.Split('|'));
        }
        PlayEnvironment();
        socket.WriteEvent(ClientEvents.NextTurn, new List<string>());
    }

    public void Perform (IEnumerable<string> data) {

        string playerId = data.First();
        print(playerId);
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
                    player.Rotate(90, null);
                    break;
                case Action.RIGHT:
                    player.Rotate(-90, null);
                    break;
                case Action.UTURN:
                    player.Rotate(180, null);
                    break;
                default:
                    break;
            }
        }
    }

    public void PlayEnvironment() {
        conveyors.ForEach (c => c.Translate());
        rotors.ForEach (r => r.Rotate());
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
