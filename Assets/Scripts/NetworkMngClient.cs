using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class NetworkMngClient : NetworkManager
{
    NetworkConnection m_conn;

    public Text con_text;

    public override void Awake()
    {
        base.Awake();

        StartClient();
    }

    public override void OnStartClient()
    {
        Debug.Log("OnStartClient");
    }

    public override void OnStopClient()
    {
        Debug.Log("OnStopClient");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("[Client] OnClientConnect!!!!!!!!!!!!!!!!");
        m_conn = conn;
        con_text.text = "net O";
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("OnClientDisconnect");
        con_text.text = "net X";
    }

    /*public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
        Debug.Log("OnClientError");
    }*/

    public override void OnClientNotReady(NetworkConnection conn)
    {
        base.OnClientNotReady(conn);
        Debug.Log("OnClientNotReady");
    }

    
}
