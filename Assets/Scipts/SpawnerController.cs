using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnerController : MonoBehaviour
{
    [SerializeField] ShapeController[] AllShapes;
    [SerializeField] Image[] shapeImages = new Image[4];

    ShapeController[] shapeAlign=new ShapeController[4];


    private void Awake()
    {
        AllDoNull();
    }
    public ShapeController CreateToShape()
    {
        ShapeController Shapes = null;
        Shapes = GetToAlignShape();
        Shapes.gameObject.SetActive(true);
        Shapes.transform.position = transform.position;


        if(Shapes!= null)
        {
            return Shapes;
        }
        else
        {
            print("dizi boþ");
            return null;
        }
        
    
    }

    

    void AllDoNull()
    {
        for(int i = 0; i < shapeAlign.Length; i++)
        {
            shapeAlign[i] = null;
        }

        AllAlignFull();
    }
   
    void AllAlignFull()
    {
        for(int i = 0;i < shapeAlign.Length;i++)
        {
            if (shapeAlign[i] == null)
            {
                shapeAlign[i] = Instantiate(CreateRandomShape(), transform.position, Quaternion.identity) as ShapeController;
                shapeAlign[i].gameObject.SetActive(false);
                shapeImages[i].sprite = shapeAlign[i].shapess;
            }
        }

        StartCoroutine(ShapeImageOpen());
    }

    IEnumerator ShapeImageOpen()
    {
        for(int i = 0;i<shapeImages.Length ; i++)
        {
            shapeImages[i].GetComponent<CanvasGroup>().alpha = 0;
            shapeImages[i].GetComponent<RectTransform>().localScale = Vector3.zero;
        }

        yield return new WaitForSeconds(0.1f);

        int sayac = 0;

        while(sayac < shapeImages.Length)
        {
            shapeImages[sayac].GetComponent<CanvasGroup>().DOFade(1f, .6f);
            shapeImages[sayac].GetComponent<RectTransform>().DOScale(1f, 0.6f).SetEase(Ease.OutBack);
            sayac++;
            yield return new WaitForSeconds(0.4f);
        }

        
    }
    ShapeController CreateRandomShape()
    {
        int randomShapes = Random.Range(0, AllShapes.Length);

        if (AllShapes[randomShapes])
        {
            return AllShapes[randomShapes];
        }
        else
        {
            return null;
        }
    }

    ShapeController GetToAlignShape()
    {
        ShapeController alignShape = null;

        if (shapeAlign[0])
        {
            alignShape= shapeAlign[0];
        }
        
        for(int i = 1;i < shapeAlign.Length;i++) 
        {
            shapeAlign[i - 1] = shapeAlign[i];
            shapeImages[i-1].sprite= shapeAlign[i-1].shapess;
        }

        shapeAlign[shapeAlign.Length - 1] = null;

        AllAlignFull();

        return alignShape;
    }
}
