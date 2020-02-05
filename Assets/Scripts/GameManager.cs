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
        // GeneratePlayer (new List<string>(){"aaa:0"});
        // Debug.Log("Player Generated");
        // RecursivePerform (
        //     new List<Order>() {
        //         new Order("aaa", 1, Action.FWD1),
        //         new Order("aaa", 2, Action.RIGHT),
        //          new Order("aaa", 3, Action.FWD1),
        //          new Order("aaa", 4, Action.LEFT),
        //          new Order("aaa", 5, Action.FWD2),
        //          new Order("aaa", 6, Action.FWD1),
        //          new Order("aaa", 7, Action.FWD1),
        //          new Order("aaa", 8, Action.BKWD),
        //          new Order("aaa", 9, Action.FWD1)
        //     }
        // );
        // Debug.Log("Order Performed");
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
        var orders = new List<Order>();
        foreach (var payload in data) {
            var payloadData = payload.Split('|');
            string playerId = payloadData.First();
            var actions = payloadData.Skip(1).ToList();
            GetPlayer (playerId).Unlock();

            foreach (var action in actions) {
                var actionData = action.Split(':');
                orders.Add(new Order(playerId, int.Parse(actionData[1]), (Action)int.Parse(actionData[0]), actions.IndexOf(action)));
            }
        }

        RecursivePerform(orders.OrderBy(order => order.turn).ThenBy(order => order.priority).ToList());
    }

    public void RecursivePerform (List<Order> orders) {
        if (orders.Count == 0) {
            PlayEnvironment();
            socket.WriteEvent(ClientEvents.NextTurn, new List<string>());
            return;
        }

        var player = GetPlayer (orders.First().playerId);
        switch (orders.First().action) {
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

        player.onAnimationEnd += () => {
            player.ResetAnimationHandlers();
            RecursivePerform(orders.Skip(1).ToList());
        };
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
