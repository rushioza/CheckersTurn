using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int cellPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void OnMouseDown()
    {
        BoardManager.insta.OnCellClicked(this);
    }
}
