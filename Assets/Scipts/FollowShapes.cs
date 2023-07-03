using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShapes : MonoBehaviour
{
    ShapeController FollowShape=null;

    bool isItDown = false;

    public Color color = new Color(1f,1f,1f,.4f);

    public void CreateFallowShape(ShapeController realShape,GridManager grid)
    {
        if (!FollowShape)
        {
            FollowShape = Instantiate(realShape, realShape.transform.position, realShape.transform.rotation) as ShapeController;
            FollowShape.name = "FollowShape";
            SpriteRenderer[] AllSprite=FollowShape.GetComponentsInChildren<SpriteRenderer>();
            
            foreach (SpriteRenderer sr in AllSprite) { 
            
                sr.color = color;
            }
        }else
        {
            FollowShape.transform.position=realShape.transform.position;
            FollowShape.transform.rotation=realShape.transform.rotation;
        }

        isItDown = false;

        while(!isItDown)
        {
            FollowShape.MoveToDown();
            if (!grid.IsGridValid(FollowShape))
            {
                FollowShape.MoveToUp();
                isItDown=true;
            }
        }

    }

    public void ResetFollowShapes()
    {
        Destroy(FollowShape.gameObject);
    }
}
