using System.IO;
using System.Net.Sockets;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SocketClient : MonoBehaviour {

    private TcpClient client;
    public string ip;
    public int port;

    private Stream s;
    private StreamReader sr;
    private StreamWriter sw;

    private GameManager manager;

    public void Start () {
        // manager = GetComponent<GameManager>();

        // client = new TcpClient(ip, port);
        
        // s = client.GetStream();
        // sr = new StreamReader(s);
        // sw = new StreamWriter(s);
        
        // this.WriteEvent(ClientEvents.StartGame, new List<int>() {
        //     1234,
        //     5678,
        //     90
        // });
    }

    public void Update () {
        // if (!sr.EndOfStream) {
        //     ReadEvent();
        // }
    }

    public void WriteEvent(ClientEvents eventCode, List<int> args) {
        sw.Write((int) eventCode);
        sw.Write('.');
        args.ForEach(arg => {
            sw.Write(arg);
            sw.Write('.');
        });
        sw.Write("EOL");
        sw.Flush();
    }

    public void ReadEvent() {
        var payload = sr.ReadToEnd();
        var data = payload.Split('.');
        var eventType = int.Parse(data[0]);
        HandleEvents((ServerEvents) eventType, data.Skip(1));
    }

    public void HandleEvents(ServerEvents eventType, IEnumerable<string> data) {
        switch (eventType) {
            case ServerEvents.Start:
                manager.GeneratePlayer (data);
                break;
            case ServerEvents.Perform:
                manager.Perform (data);
                break;
            default:
                break;
        }
    }
}

public enum ServerEvents {
    Nop = -1,
    Start = 0,
    Perform = 1
}

public enum ClientEvents {
    StartGame = 0, 
    NextTurn = 1
}