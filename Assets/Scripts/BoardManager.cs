using UnityEngine;
using System.Collections.Generic;
public class BoardManager : MonoBehaviour
{
    int[,] board = new int[4, 4];

    GameObject[,] cellObj = new GameObject[4, 4];


    public GameObject[] allCells;
    public List<GameObject> PlayerA;
    public List<GameObject> PlayerB;

    public Turn currentTurn = Turn.PlayerA; // 0 = PlayerA , 1 = PlayerB

    public static BoardManager insta;

    Player player;

    void Awake()
    {
        if (insta == null)
        {
            insta = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetStartPosition();
    }

    private void SetStartPosition()
    {

        int k = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                // Debug.Log(i + " , " + j);
                cellObj[i, j] = allCells[k];
                k++;
            }
        }

        // Player A
        SetPiece(PlayerA[0].transform, cellObj[0, 0].transform);
        SetPiece(PlayerA[1].transform, cellObj[0, 2].transform);

        // Player B
        SetPiece(PlayerB[0].transform, cellObj[3, 1].transform);
        SetPiece(PlayerB[1].transform, cellObj[3, 3].transform);
    }

    private void SetPiece(Transform piece, Transform parentCell)
    {
        piece.SetParent(parentCell);
        piece.localPosition = Vector3.zero;
    }


    public void OnSelectedPlayer(Player selectedPlayer)
    {
        player = selectedPlayer;

        // clear HighLights

        // Find Valid Moves--- START

        // Find Valid Moves--- END


        // Show HighLights
    }

}

public enum Turn
{
    PlayerA,
    PlayerB
}