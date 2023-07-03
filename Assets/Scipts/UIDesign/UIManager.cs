using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool isGameStop = false;
    public GameObject pausePanel,GameOverScreen,kingImage;

    [SerializeField] GameObject replaybtn,homeBtn;
    [SerializeField] SoundController soundController;
    [SerializeField] Slider MusicVolumeSet, FxVolumeSet;

    GameManager gameManager;
    PuanManager puanManager;
    EffectController effectController;

    public Text DeadScoreTxt;
    public Text DeadLevelTxt;
    public Text HeighScoreTxt;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        puanManager = FindObjectOfType<PuanManager>();
        effectController = GameObject.FindObjectOfType<EffectController>();
    }

    private void Start()
    {
        GameOverScreen.SetActive(false);

        if(pausePanel)
            pausePanel.SetActive(false);
        MusicVolumeSet.value = PlayerPrefs.GetFloat("musicvolume");
        FxVolumeSet.value = PlayerPrefs.GetFloat("fxvolume");

      
    }

    private void Update()
    {

        Debug.Log(isGameStop);
        if (gameManager.gameOver)
            GameIsOver();
    }

    public void GameIsOver()
    {
        GameOverScreen.SetActive(true);
        DeadScoreTxt.text = "Score : "+puanManager.Skor.ToString();
        DeadLevelTxt.text = "Level : "+puanManager.Level.ToString();
        if (puanManager.Skor > PlayerPrefs.GetInt("heighScore"))
        {
            PlayerPrefs.SetInt("heighScore", puanManager.Skor);
            HeighScoreTxt.text = "New Heigh Score : " + PlayerPrefs.GetInt("heighScore").ToString();
            effectController.GameOverStarEffect();
            kingImage.SetActive(true);
            kingImage.GetComponent<RectTransform>().DOScale(1.5f, .4f).SetLoops(2,LoopType.Yoyo);

        }
        else if(puanManager.Skor < PlayerPrefs.GetInt("heighScore"))
        {
            
            HeighScoreTxt.text = "Heigh Score : "+PlayerPrefs.GetInt("heighScore").ToString();
        }
        
    }


    public void ContinueGame()
    {
        if (isGameStop)
        {
            PlayerPrefs.SetFloat("fxvolume", FxVolumeSet.value);
            PlayerPrefs.SetFloat("musicvolume", MusicVolumeSet.value);
            isGameStop = !isGameStop;
            gameManager.PauseMode = isGameStop;
            PauseScreenEffect();
        }

    }
    public void PauseOpenOff()
    {
        if (gameManager.gameOver)
            return;

        isGameStop = !isGameStop;
        gameManager.PauseMode = isGameStop;
        if(pausePanel)
        {
            PauseScreenEffect();

            
           // Time.timeScale = (isGameStop) ? 0 : 1f;

        }


        if (!isGameStop)
        {
            FxVolumeSet.value = PlayerPrefs.GetFloat("fxvolume");
            MusicVolumeSet.value = PlayerPrefs.GetFloat("musicvolume");

        }
    }

    private void PauseScreenEffect()
    {
        if (isGameStop)
        {
            pausePanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            pausePanel.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosY(0, .5f).OnComplete(() =>
            {
                pausePanel.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().DOAnchorPos(new Vector2(345, -250), 0.5f).SetEase(Ease.OutBack);
                pausePanel.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().DOAnchorPos(new Vector2(735, -250), 0.5f).SetEase(Ease.OutBack);

            });
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
            pausePanel.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosY(1000, .5f).OnComplete(() =>
            {
                pausePanel.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -250), 0.5f).SetEase(Ease.OutBack);
                pausePanel.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, -250), 0.5f).SetEase(Ease.OutBack).OnComplete(() => { pausePanel.SetActive(false); });

            });

            
        }

    }



    public void RePlayGame()
    {
        soundController.BtnEffect();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReplayEffect()
    {
        replaybtn.GetComponent<RectTransform>().DORotate(new Vector3(0,0,180), 0.6f);
      
    }
    
    public void HomeBtnEffect()
    {
        homeBtn.GetComponent<RectTransform>().DOScale(1.5f, 0.6f);
      
    }
    public void ReplayEffectUp()
    {
        replaybtn.GetComponent<RectTransform>().DORotate(new Vector3(0,0,0), .6f);
      
    }
    
    public void HomeBtnEffectUp()
    {
        homeBtn.GetComponent<RectTransform>().DOScale(1f, .6f);
      
    }

    public void MenuBtn()
    {
        soundController.BtnEffect();
        SceneManager.LoadScene(0);
    }

    
}
