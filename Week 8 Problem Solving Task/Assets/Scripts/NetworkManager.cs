using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private const string _roomName = "room";
    
    // Start is called before the first frame update
    void Start()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.JoinOrCreateRoom(_roomName, roomOptions, TypedLobby.Default);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
