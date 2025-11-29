using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
public class BoardManager : MonoBehaviour
{
    GameObject[,] cellObj = new GameObject[4, 4];
    public List<Vector2Int> validMoves = new List<Vector2Int>();
    public Player[,] playerOnCell = new Player[4, 4];
    public GameObject[] allCells;
    public List<GameObject> PlayerA;
    public List<GameObject> PlayerB;
    public Turn currentTurn = Turn.PlayerA; // Starting Turn
    public List<Vector2Int> highlightedCells = new List<Vector2Int>();
    public bool gameOver = false;
    public int A_Points = 0;
    public int B_Points = 0;

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

    #region  Initialize
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

        // Player B
        SetPiece(PlayerB[0].transform, cellObj[0, 0].transform);
        SetPiece(PlayerB[1].transform, cellObj[0, 2].transform);

        // Player A
        SetPiece(PlayerA[0].transform, cellObj[3, 1].transform);
        SetPiece(PlayerA[1].transform, cellObj[3, 3].transform);

        playerOnCell[0, 0] = PlayerB[0].GetComponent<Player>();
        playerOnCell[0, 2] = PlayerB[1].GetComponent<Player>();
        playerOnCell[3, 1] = PlayerA[0].GetComponent<Player>();
        playerOnCell[3, 3] = PlayerA[0].GetComponent<Player>();

    }

    private void SetPiece(Transform piece, Transform parentCell)
    {
        piece.SetParent(parentCell);
        piece.localPosition = Vector3.zero;
    }

    #endregion

    #region  Main Execution Part
    public void OnSelectedPlayer(Player selectedPlayer)
    {
        player = selectedPlayer;

        // clear HighLights
        closeHighLight();

        // Find Valid Moves--- START
        validMoves.Clear();
        validMoves = GetValidMoves(player);
        Debug.Log("Get Valid Moves");
        for (int i = 0; i < validMoves.Count; i++)
        {
            Debug.Log(validMoves[i].x + " , " + validMoves[i].y);
        }
        // Find Valid Moves--- END

        ShowHighLight(player, validMoves);

        // Show HighLights
    }

    List<Vector2Int> GetValidMoves(Player selectedPlayer)
    {
        List<Vector2Int> result = new List<Vector2Int>();
        List<Vector2Int> captureMoves = new List<Vector2Int>();

        int row = selectedPlayer.myPosition.x;
        int column = selectedPlayer.myPosition.y;


        if (selectedPlayer.mySide == Turn.PlayerA)
        {
            Vector2Int temp = new Vector2Int(row - 1, column - 1);        // LEFT Diagonal
            if (temp.x >= 0 && temp.x <= 3 && temp.y >= 0 && temp.y <= 3)
            {
                result.Add(temp);
                Player middle = playerOnCell[temp.x, temp.y];
                if (middle != null && middle.mySide != selectedPlayer.mySide)
                {
                    Vector2Int Furthertemp = new Vector2Int(row - 2, column - 2);        // LEFT Diagonal
                    if (Furthertemp.x >= 0 && Furthertemp.x <= 3 && Furthertemp.y >= 0 && Furthertemp.y <= 3)
                    {
                        Player Third = playerOnCell[Furthertemp.x, Furthertemp.y];
                        if (Third == null)
                        {
                            captureMoves.Add(Furthertemp);
                            return captureMoves;
                        }
                    }
                }
                if (middle != null && middle.mySide == selectedPlayer.mySide)
                {
                    result.RemoveAt(result.Count - 1);
                }
            }
            Vector2Int temp2 = new Vector2Int(row - 1, column + 1);        // RIGHT Diagonal
            if (temp2.x >= 0 && temp2.x <= 3 && temp2.y >= 0 && temp2.y <= 3)
            {
                result.Add(temp2);
                Player middle = playerOnCell[temp2.x, temp2.y];
                if (middle != null && middle.mySide != selectedPlayer.mySide)
                {
                    Vector2Int Furthertemp = new Vector2Int(row - 2, column + 2);        // LEFT Diagonal
                    if (Furthertemp.x >= 0 && Furthertemp.x <= 3 && Furthertemp.y >= 0 && Furthertemp.y <= 3)
                    {
                        Player Third = playerOnCell[Furthertemp.x, Furthertemp.y];
                        if (Third == null)
                        {
                            captureMoves.Add(Furthertemp);
                            return captureMoves;
                        }
                    }
                }
                if (middle != null && middle.mySide == selectedPlayer.mySide)
                {
                    result.RemoveAt(result.Count - 1);
                }
            }


        }
        if (selectedPlayer.mySide == Turn.PlayerB)
        {
            Vector2Int temp = new Vector2Int(row + 1, column + 1);        // LEFT Diagonal
            if (temp.x >= 0 && temp.x <= 3 && temp.y >= 0 && temp.y <= 3)
            {
                result.Add(temp);
                Player middle = playerOnCell[temp.x, temp.y];
                if (middle != null && middle.mySide != selectedPlayer.mySide)
                {
                    Vector2Int Furthertemp = new Vector2Int(row + 2, column + 2);        // LEFT Diagonal
                    if (Furthertemp.x >= 0 && Furthertemp.x <= 3 && Furthertemp.y >= 0 && Furthertemp.y <= 3)
                    {
                        Player Third = playerOnCell[Furthertemp.x, Furthertemp.y];
                        if (Third == null)
                        {
                            captureMoves.Add(Furthertemp);
                            return captureMoves;
                        }
                    }
                }
                if (middle != null && middle.mySide == selectedPlayer.mySide)
                {
                    result.RemoveAt(result.Count - 1);
                }
            }
            Vector2Int temp2 = new Vector2Int(row + 1, column - 1);        // RIGHT Diagonal
            if (temp2.x >= 0 && temp2.x <= 3 && temp2.y >= 0 && temp2.y <= 3)
            {
                result.Add(temp2);
                Player middle = playerOnCell[temp2.x, temp2.y];
                if (middle != null && middle.mySide != selectedPlayer.mySide)
                {
                    Vector2Int Furthertemp = new Vector2Int(row + 2, column - 2);        // LEFT Diagonal
                    if (Furthertemp.x >= 0 && Furthertemp.x <= 3 && Furthertemp.y >= 0 && Furthertemp.y <= 3)
                    {
                        Player Third = playerOnCell[Furthertemp.x, Furthertemp.y];
                        if (Third == null)
                        {
                            captureMoves.Add(Furthertemp);
                            return captureMoves;
                        }
                    }
                }
                if (middle != null && middle.mySide == selectedPlayer.mySide)
                {
                    result.RemoveAt(result.Count - 1);
                }
            }
        }
        return result;
    }

    void ShowHighLight(Player selectedPlayer, List<Vector2Int> validates)
    {
        // ORG = 255,81,0,255  // Highlighted = 255,255,0,255
        if (validates.Count > 0)
        {
            for (int i = 0; i < validates.Count; i++)
            {
                cellObj[validates[i].x, validates[i].y].GetComponent<Image>().color = new Color32(255, 255, 0, 255);
                highlightedCells.Add(new Vector2Int(validates[i].x, validates[i].y));
            }
            cellObj[selectedPlayer.myPosition.x, selectedPlayer.myPosition.y].GetComponent<Image>().color = new Color32(255, 255, 0, 255);
            highlightedCells.Add(new Vector2Int(selectedPlayer.myPosition.x, selectedPlayer.myPosition.y));
        }
    }

    void closeHighLight()
    {
        if (highlightedCells.Count > 0)
        {
            for (int i = 0; i < highlightedCells.Count; i++)
            {
                cellObj[highlightedCells[i].x, highlightedCells[i].y].GetComponent<Image>().color = new Color32(255, 81, 0, 255);
            }
        }
    }

    public void OnCellClicked(Cell cell)
    {
        if (player == null)
            return;

        Vector2Int targetPos = cell.cellPos;

        bool isValid = false;

        for (int i = 0; i < validMoves.Count; i++)
        {
            if (validMoves[i] == targetPos)
            {
                isValid = true;
                break;
            }
        }

        if (!isValid)
            return;


        Vector2Int oldPos = player.myPosition;

        // check if it's a capture (jump)
        int rowDiff = Mathf.Abs(targetPos.x - oldPos.x);
        int colDiff = Mathf.Abs(targetPos.y - oldPos.y);

        if (rowDiff == 2 && colDiff == 2)
        {
            // captured piece is in the middle
            int capRow = (oldPos.x + targetPos.x) / 2;
            int capCol = (oldPos.y + targetPos.y) / 2;

            Player captured = playerOnCell[capRow, capCol];
            if (captured != null)
            {
                playerOnCell[capRow, capCol] = null;
                if (captured.mySide == Turn.PlayerA)
                    B_Points++;
                if (captured.mySide == Turn.PlayerB)
                    A_Points++;

                Destroy(captured.gameObject);
            }
        }

        playerOnCell[oldPos.x, oldPos.y] = null;
        playerOnCell[targetPos.x, targetPos.y] = player;

        player.myPosition = targetPos;
        player.transform.SetParent(cell.transform);
        player.transform.localPosition = Vector2.zero;

        closeHighLight();
        validMoves.Clear();
        player = null;

        CheckWinCondition();

        if (!gameOver)
            SwitchTurn();
    }

    void SwitchTurn()
    {
        if (currentTurn == Turn.PlayerA)
            currentTurn = Turn.PlayerB;
        else
            currentTurn = Turn.PlayerA;
    }

    #endregion


    #region  Check Win/Lose/Draw Part

    bool AnyPlayerLeft(Turn turn)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Player p = playerOnCell[i, j];
                if (p != null && p.mySide == turn)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool AnyMovesLeft(Turn turn)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Player p = playerOnCell[i, j];
                if (p != null && p.mySide == turn)
                {
                    List<Vector2Int> moves = GetValidMoves(p);
                    if (moves.Count > 0)
                        return true;
                }
            }
        }
        return false;
    }

    void CheckWinCondition()
    {
        bool A_Player_available = AnyPlayerLeft(Turn.PlayerA);
        bool B_Player_available = AnyPlayerLeft(Turn.PlayerB);

        if (!A_Player_available && B_Player_available)
        {
            DeclareWinner(Turn.PlayerB);
            return;
        }

        if (!B_Player_available && A_Player_available)
        {
            DeclareWinner(Turn.PlayerA);
            return;
        }

        if (!A_Player_available && !B_Player_available)
        {
            Debug.Log("Draw!!");
            gameOver = true;
            return;
        }

        /* if (!AnyMovesLeft(currentTurn))
        {
            Turn winner = currentTurn == Turn.PlayerA ? Turn.PlayerB : Turn.PlayerA;
            DeclareWinner(winner);
        } */

    }

    void DeclareWinner(Turn turn)
    {
        gameOver = true;
        Debug.Log(turn + "Wins!!");
    }


    #endregion 
}

public enum Turn
{
    PlayerA,
    PlayerB
}