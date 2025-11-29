using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.JoinOrCreateRoom(
            "CheckersRoom", 
            new RoomOptions { MaxPlayers = 2 }, 
            TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room!");

        if (PhotonNetwork.IsMasterClient)
        {
            // Load main game scene
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}
