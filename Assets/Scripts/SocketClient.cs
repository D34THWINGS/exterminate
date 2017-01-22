using System.IO;
using System.Net.Sockets;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
 using System.Threading;

public class SocketClient : MonoBehaviour {

    private TcpClient client;
    public string ip;
    public int port;

    private Stream s;
    private StreamReader sr;
    private StreamWriter sw;

    private GameManager manager;
    private Thread workerThread;
    private List<string> events;

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

        // workerThread = new Thread(new ThreadStart(ReadEvent));
        // workerThread.Start();

        // events = new List<string>();
    }

    public void Update () {
        // lock(events) {
        //     if (events.Count > 0) {
        //         foreach (var payload in events) {
        //             var data = payload.Split('.');
        //             var eventType = int.Parse(data[0]);
        //             this.HandleEvents((ServerEvents) eventType, data.Skip(1));
        //         }
        //         events.Clear();
        //     }
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
        while(true) {
            if (!sr.EndOfStream) {
                var payload = sr.ReadLine();
                print(payload);
                lock(events) {
                    events.Add(payload);
                }
            }
        }
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