using System.IO;
using System.Net.Sockets;
using UnityEngine;


public class SocketClient : MonoBehaviour {

    private TcpClient client;
    public string ip;
    public int port;

    private Stream s;
    private StreamReader sr;
    private StreamWriter sw;
    private ServerEvents se;

    public void Start () {
        client = new TcpClient(ip, port);
        
        s = client.GetStream();
        sr = new StreamReader(s);
        sw = new StreamWriter(s);
        sw.AutoFlush = true;
        sw.Write(12345678912345678913);
        sw.Write(12345678912345678913);

    }

    public void Update () {
        se = (ServerEvents) sr.Read ();

        switch (se) {
            case ServerEvents.Start:
                break;
            case ServerEvents.Stop:
                break;
            case ServerEvents.Rotate:
                break;
            case ServerEvents.Move:
                break;
            default:
                break;
        }

    }
}

public enum ServerEvents {
    Start = 1,
    Stop = 2,
    Rotate = 4,
    Move = 8
}

public enum ClientEvents {

}