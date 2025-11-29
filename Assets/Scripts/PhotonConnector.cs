using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();  
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
