using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] children = new GameObject[4];

    [SerializeField]
    Sprite[] sprites = new Sprite[7];

    ShapeController[] shapesAlign = new ShapeController[4];

    SpawnerController controller;
    GridManager gridManager;
    UIManager uiManager;
    PuanManager puanManager;
    FollowShapes followShapes;
    SoundController soundController;
    EffectController effectController;
    

    [Range(0.01f, 1f)]
    public float SpawnTime;

    float SpawnRange;

    [Range(0.01f, 1f)]
    [SerializeField]
    float HorizontalMoveTime;

    float HorizontalMoveRange;
    
    [Range(0.01f, 1f)]
    [SerializeField]
    float VerticalMoveTime;

    float VerticalMoveRange;
    
    ShapeController activeShape;
    Vector3 range;
    

    public bool gameOver,PauseMode;

    
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();    
    }
    private void Start()
    {
        

        controller=GameObject.FindObjectOfType<SpawnerController>();
        gridManager = GameObject.FindObjectOfType<GridManager>();
        puanManager = GameObject.FindObjectOfType<PuanManager>();
        followShapes= GameObject.FindObjectOfType<FollowShapes>();
        soundController = GameObject.FindObjectOfType<SoundController>();
        effectController = GameObject.FindObjectOfType<EffectController>();
        

        if (controller)
        {
            if (activeShape==null)
            {
                activeShape=controller.CreateToShape();
            }
        }


    }

    
    private void Update()
    {
        if (!gridManager || !controller|| !activeShape ||gameOver||uiManager.isGameStop||PauseMode )
        {
            return;
        }
        
        
        
            
        PlayingGame();
    }
    private void LateUpdate()
    {
        if (followShapes)
        {
            followShapes.CreateFallowShape(activeShape, gridManager);
        }
    }
    private void PlayingGame()
    {
        if (Input.GetKey("right") && HorizontalMoveRange < Time.time || Input.GetKeyDown("right"))
        {
            HorizontalMoveRange = Time.time + HorizontalMoveTime;
            activeShape.MoveToRight();

            soundController.ShapeSoundEffect();
            if (!gridManager.IsGridValid(activeShape))
            {
                activeShape.MoveToLeft();
            }
            

        }
        
        if (Input.GetKey("left") && HorizontalMoveRange < Time.time || Input.GetKeyDown("left"))
        {
            HorizontalMoveRange = Time.time + HorizontalMoveTime;
            activeShape.MoveToLeft();
            soundController.ShapeSoundEffect();
            if (!gridManager.IsGridValid(activeShape))
            {
                activeShape.MoveToRight();
            }
            

        }
        if(Input.GetKey("down")&&Time.time>VerticalMoveRange||Time.time>SpawnRange)
        {
            VerticalMoveRange = Time.time + VerticalMoveTime;
            
            SpawnRange = Time.time + SpawnTime;

            if (activeShape)
            {
                activeShape.MoveToDown();

                if (!gridManager.IsGridValid(activeShape))
                {
                    
                    if (gridManager.CheckItForGameOver(activeShape))
                    {
                        activeShape.MoveToUp();
                        soundController.GameOverEffectSound();
                        
                        gameOver = true;
                    }
                    else
                    {
                        GridIsValid();
                        
                    }
                    
                }
            }
        }
        if (Input.GetKey("down"))
        {
            soundController.ShapeSoundEffectDown();
        }

        if (Input.GetKeyDown("up"))
        {
            activeShape.RotateToRight();

            if(!gridManager.IsGridValid(activeShape))
            {
                activeShape.MoveToLeft();
            }


            foreach (Transform child in activeShape.transform)
            {
                Vector2 pos = VectorToInt(child.position);

                if (pos.y <= 0)
                {
                    activeShape.MoveToUp();
                }
            }



            foreach (Transform child in activeShape.transform)
            {
                Vector2 pos = VectorToInt(child.position);

                if (pos.x <= 0)
                {
                    activeShape.MoveToRight();
                }

            }

            foreach (Transform child in activeShape.transform)
            {
                Vector2 pos = VectorToInt(child.position);

                if (pos.x >= 10)
                {
                    activeShape.MoveToLeft();
                }

            }
        }
    }

    private void GridIsValid()
    {
        soundController.ShapeIsGridValid();
        activeShape.MoveToUp();
        gridManager.GetToShapeinGrid(activeShape);
        
        if (followShapes)
        {
            followShapes.ResetFollowShapes();
        }

        if (controller)
        {
            activeShape = controller.CreateToShape();
            
        }

        StartCoroutine(gridManager.TheAllLineDelete());

        if (gridManager.lineFull > 0)
        {
            SoundController.instance.LinesDelEffect();
        }
        

        puanManager.Skor += 25;
      
    }

    public void UpButton()
    {

        soundController.BtnEffect();
            activeShape.RotateToRight();

            if (!gridManager.IsGridValid(activeShape))
            {
                activeShape.MoveToLeft();
            }


            foreach (Transform child in activeShape.transform)
            {
                Vector2 pos = VectorToInt(child.position);

                if (pos.y <= 0)
                {
                    activeShape.MoveToUp();
                }
            }



            foreach (Transform child in activeShape.transform)
            {
                Vector2 pos = VectorToInt(child.position);

                if (pos.x <= 0)
                {
                    activeShape.MoveToRight();
                }

            }

            foreach (Transform child in activeShape.transform)
            {
                Vector2 pos = VectorToInt(child.position);

                if (pos.x >= 10)
                {
                    activeShape.MoveToLeft();
                }

            }
        
    }

    public void DownButton()
    {

        soundController.BtnEffect();
        VerticalMoveRange = Time.time + VerticalMoveTime;

            SpawnRange = Time.time + SpawnTime;

            if (activeShape)
            {
                activeShape.MoveToDown();

                if (!gridManager.IsGridValid(activeShape))
                {

                    if (gridManager.CheckItForGameOver(activeShape))
                    {
                        activeShape.MoveToUp();
                        gameOver = true;
                        
                    }
                    else
                    {
                        GridIsValid();
                    }

                }
            }
        
    }

    public void LeftButton()
    {

        soundController.BtnEffect();
        HorizontalMoveRange = Time.time + HorizontalMoveTime;
            activeShape.MoveToLeft();

            if (!gridManager.IsGridValid(activeShape))
            {
                activeShape.MoveToRight();
            }
            

        
    }

    public void RightButton()
    {

        soundController.BtnEffect();
        HorizontalMoveRange = Time.time + HorizontalMoveTime;
            activeShape.MoveToRight();

            if (!gridManager.IsGridValid(activeShape))
            {
                activeShape.MoveToLeft();
            }
            

        
    }
    Vector2 VectorToInt(Vector2 vector)
    {
       return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
}
