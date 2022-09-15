using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private const string _roomName = "room";
    public GameObject playerPrefab;
    public GameObject localPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server");
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom(_roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        int numPlayers = PhotonNetwork.CurrentRoom.PlayerCount;

        Debug.Log($"Joined chess game: {_roomName}");
        Debug.Log($"Number of players: {numPlayers}");

        // Instantiate a player with a different position and rotation based on which player they are
        switch (numPlayers)
        {
            // Instantiate at one end of the table
            case 1:
                localPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0.0f, 0.0f, -0.6f), Quaternion.identity);
                break;
            // Instantiate at the opposite end of the table
            case 2:
                localPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0.0f, 0.0f, 0.6f), Quaternion.Euler(0.0f, 180.0f, 0.0f));
                break;
        }

    }
}
