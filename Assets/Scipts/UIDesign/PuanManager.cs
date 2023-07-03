using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
public class PuanManager : MonoBehaviour
{
    [SerializeField]GameObject kingImage;
    SoundController soundController;
    GameManager gameManager;
    public int Skor,Level;
    public Text SkorTxt,LevelTxt;

    public int LineAtLevel = 5;

    bool islevelSkip;
    

    void Start()
    {
        gameManager=GameObject.FindObjectOfType<GameManager>();
        soundController=GameObject.FindObjectOfType<SoundController>();
        Skor =0;
        
    }

    private void Update()
    {

        if (Skor > PlayerPrefs.GetInt("heighScore"))
        {
            kingImage.SetActive(true);
            
        }
        else
        {
            kingImage.SetActive(false);
        }

        Level =Mathf.RoundToInt((Skor / 100) + (1));
       
        if (Level>Convert.ToInt32(LevelTxt.text))
        {
            LevelSkipEffect();
            LevelSkip();
            soundController.LevelSkipEffect();
        }

        LevelTxt.text =Level.ToString();
        SkorTxt.text = "Score"+"\n"+Skor.ToString();
    }

    public void ScoreReset()
    {
        PlayerPrefs.SetInt("heighScore", 0);
    }

    void LevelSkip()
    {
        

        if (gameManager.SpawnTime > 0.08f)
        {
            if (Level < 10)
            {
                gameManager.SpawnTime -= 0.035f;
            }
            else if (Level > 10)
            {
                gameManager.SpawnTime -= 0.01f;
            }
        }

    }

    void LevelSkipEffect()
    {
        LevelTxt.GetComponent<RectTransform>().DOScale(2.5f, 0.7f).SetLoops(2, LoopType.Yoyo);
    }
    
}
