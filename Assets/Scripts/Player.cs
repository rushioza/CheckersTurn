using UnityEngine;

public class Player : MonoBehaviour
{
    public Turn mySide;
    public Vector2Int myPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void OnMouseDown()
    {
        if (BoardManager.insta.currentTurn == mySide)
        {
            Debug.Log("YESS Your Turn!");
        }
        else
        {
            Debug.Log("Not Your Turn!");
        }
    }
}
