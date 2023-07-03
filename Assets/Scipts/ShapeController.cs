using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    [SerializeField] bool isBeRotate;

    public Sprite shapess;

    GridManager gridManager;
    GameManager gameManager;
    Vector2 location;
    Vector3 Range;
    
    private void Awake()
    {
        gridManager = GetComponent<GridManager>();
        gameManager = GetComponent<GameManager>();
    }
   

    public void MoveToDown()
    {
        transform.Translate(Vector3.down,Space.World);
    }
    
    public void MoveToUp()
    {
        transform.Translate(Vector3.up,Space.World);
    }
    
    public void MoveToLeft()
    {
        transform.Translate(Vector3.left,Space.World);
    }
    
    public void MoveToRight()
    {
        transform.Translate(Vector3.right,Space.World);
    }
    
    public void RotateToLeft()
    {
        if(isBeRotate)
        {
            transform.Rotate(0, 0,-90);
            
        }
    }
    public void RotateToRight()
    {
        if(isBeRotate)
        {
            transform.Rotate(0, 0,90);

        }
    }

    

}
