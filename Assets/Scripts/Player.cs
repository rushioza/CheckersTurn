using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public Turn mySide;
    public Vector2Int myPosition;
    public PhotonView photonView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        BoardManager.insta.RegisterPiece(this);
        BoardManager.insta.PlacePieceOnBoard(this);
    }

    void OnMouseDown()
    {
        if (!photonView.IsMine)
            return;

        if (BoardManager.insta.currentTurn == mySide)
        {
            Debug.Log("YESS Your Turn!");
            BoardManager.insta.OnSelectedPlayer(this);
        }
        else
        {
            Debug.Log("Not Your Turn!");
        }
    }
}
