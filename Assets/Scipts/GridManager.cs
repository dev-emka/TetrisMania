using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Transform GridPrefabs;
    PuanManager puanManager;

    public int lineFull;
    public int Height = 10;
    public int Weight = 10;
    Transform[,] TheGrid;

    public EffectController[] effectController=new EffectController[4];

    private void Awake()
    {
        TheGrid=new Transform[Weight,Height];
    }

    private void Start()
    {
        puanManager=GameObject.FindObjectOfType<PuanManager>();
        CreateToGrid();
    }

    bool InGrid(int x,int y)
    {
        return(x >= 0 && x < Weight && y >= 0);
    }

    public bool IsGridValid(ShapeController shape)
    {
        foreach(Transform child in shape.transform) 
        {
            Vector2 pos=VectorToInt(child.position);
            
            if (!InGrid((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (pos.y < Height)
            {
                if (IsSquareFull((int)pos.x, (int)pos.y, shape))
                {
                    return false;
                }

            }
        }

        return true;
    }

    public bool IsGridRightValid(ShapeController shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos= VectorToInt(child.position);
            if (!InGrid((int)pos.x,(int)pos.y))
            {
                return false;
            }

            if(pos.x < Weight)
            {
                if (IsSquareFull((int)pos.x, (int)pos.y, shape))
                {
                    return false;
                }
            }
        }

        return true;
    }

    bool IsSquareFull(int x, int y,ShapeController shape)
    {
        return (TheGrid[x,y] != null && TheGrid[x,y].parent!=shape.transform);
    }
    void CreateToGrid()
    {
        if(GridPrefabs != null)
        {
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Weight; w++)
                {
                    Transform grids = Instantiate(GridPrefabs, new Vector3(w, h, 0), Quaternion.identity);
                    grids.name = "w " + w.ToString() + "," + "h" + h.ToString();
                    grids.parent = this.transform;
                }
            }
        }
        
    }

    public void GetToShapeinGrid(ShapeController shape)
    {
        if(shape== null)
        {
            return;
        }

        foreach(Transform child in shape.transform)
        {
            Vector2 pos = VectorToInt(child.position);
            TheGrid[(int)pos.x,(int)pos.y] = child;
        }
    }
    
    bool IsTheLineComp(int y)
    {
        for(int x = 0; x < Weight; ++x)
        {
            if (TheGrid[x, y] == null)
            {
                return false;
            }


        }

        return true;
    }

    void TheLineDelete(int y)
    {
        for(int x=0; x < Weight; ++x)
        { 

            if (TheGrid[x, y]!=null)
            {
                Destroy(TheGrid[x, y].gameObject);
            }
            TheGrid[x, y] = null;
        }
        puanManager.Skor +=20;
        
    }

    public IEnumerator TheAllLineDelete()
    {
        lineFull =0;

        for(int x=0;x<Height; ++x)
        {
            if (IsTheLineComp(x))
            {
                LinesEffectStart(lineFull, x);
                lineFull++;
            }
        }

        yield return new WaitForSeconds(.5f);

        for (int y = 0;y < Height; y++)
        {
            if (IsTheLineComp(y))
            {
                TheLineDelete(y);
                SkipToBottomAllLine(y+1);
                yield return new WaitForSeconds(.2f);
                y--;
            }
        }
        
    }

    void LinesEffectStart(int MuchLine,int y)
    {
        //if (effectController)
        //{
        //    effectController.transform.position=new Vector3(0,y,0);
        //    effectController.EffectPlay();
        //}

        if (effectController[MuchLine])
        {
            effectController[MuchLine].transform.position=new Vector3(0,y,0);
            effectController[MuchLine].EffectPlay();
        }
    }
    void SkipToBottomLine(int y)
    {
        for (int x = 0; x < Weight; ++x)
        {
            if (TheGrid[x, y] != null)
            {
                TheGrid[x, y - 1] = TheGrid[x, y];
                TheGrid[x, y] = null;
                TheGrid[x, y - 1].position += Vector3.down;

            }
        }
    }
    void SkipToBottomAllLine(int startY)
    {
        for(int i=startY; i < Height; ++i)
        {
            SkipToBottomLine(i);
        }
    }

    public bool CheckItForGameOver(ShapeController shape)
    {
        foreach(Transform child in shape.transform)
        {
            if (child.transform.position.y >= Height - 1)
            {
                return true;
            }

        }

        return false;
    }


    

    Vector2 VectorToInt(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
}
