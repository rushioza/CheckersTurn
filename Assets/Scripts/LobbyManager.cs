using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Step 1: Connect to Photon
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connecting to Photon...");
        }
        else
        {
            JoinRoom();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master. Joining lobby...");
        PhotonNetwork.JoinLobby();     // optional but good practice
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby. Now joining/creating room...");
        JoinRoom();
    }

    void JoinRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom("CheckersRoom", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room! Loading game scene...");
        PhotonNetwork.LoadLevel("GameScene");   // your board scene
    }
}
