using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] Text HeighScoreTxt;

    [SerializeField] GameObject PlayBtnn,ExitBtnn,settingsbtn,SettingsScreen;

    [SerializeField] Slider MusicVolumeSet, Fxvolumeset;
    [SerializeField] InputField usernameInput;

    [SerializeField] MenuSoundManager soundManager;
    bool isSettingTab,isEditMode;
    private void Start()
    {
        soundManager = GameObject.FindObjectOfType<MenuSoundManager>();

        if (!PlayerPrefs.HasKey("heighScore"))
        {
            PlayerPrefs.SetInt("heighScore", 0);
        }

        if (!PlayerPrefs.HasKey("fxvolume"))
        {
            PlayerPrefs.SetFloat("fxvolume", 0.3f);
        }
        
        if (!PlayerPrefs.HasKey("musicvolume"))
        {
            PlayerPrefs.SetFloat("musicvolume", 0.15f);
        }
        if (!PlayerPrefs.HasKey("username"))
        {
            PlayerPrefs.SetString("username", "Player");
        }

        usernameInput.interactable = false;
        
        usernameInput.text=PlayerPrefs.GetString("username");
    }
    private void Update()
    {

        if(PlayerPrefs.GetInt("heighScore")==0)
        {
            HeighScoreTxt.text = "Heigh Score : None";
        }else if(PlayerPrefs.GetInt("heighScore") != 0)
        {
            HeighScoreTxt.text = "Heigh Score : " + PlayerPrefs.GetInt("heighScore").ToString();

        }
       
    }

    public void StartBtn()
    {
        soundManager.ButtonfxEffect();

        if(!isSettingTab)
        SceneManager.LoadScene(1);
    }
    public void StartBtnDown()
    {
        if (!isSettingTab)
        PlayBtnn.GetComponent<RectTransform>().DOScale(1.5f, 0.6f);
    }

    public void StartBtnUp()
    {
        if(!isSettingTab)
        PlayBtnn.GetComponent<RectTransform>().DOScale(1f, 0.6f);
    }

    public void ExitBtn()
    {
        soundManager.ButtonfxEffect();

        if (!isSettingTab) 
        Application.Quit();
    }

    public void ExitBtnDown()
    {
        if(!isSettingTab)
            ExitBtnn.GetComponent<RectTransform>().DOScale(1.5f, 0.6f);
    }

    public void ExitBtnUp()
    {
        if(!isSettingTab)
         ExitBtnn.GetComponent<RectTransform>().DOScale(1f, 0.6f);
    }

    public void SettingsButton()
    {
        soundManager.ButtonfxEffect();
        isSettingTab = true;
        SettingsScreen.SetActive(isSettingTab);
        SettingsScreen.GetComponent<CanvasGroup>().DOFade(1, .2f);
        SettingsScreen.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero,0.5f);
        Fxvolumeset.value = PlayerPrefs.GetFloat("fxvolume");
        MusicVolumeSet.value = PlayerPrefs.GetFloat("musicvolume");
       
    }

    public void SettingsBtnDown()
    {
        settingsbtn.GetComponent<RectTransform>().DORotate(new Vector3(0,0,180), 0.5f);
    }

    public void SettingsBtnUp()
    {

        settingsbtn.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), 0.5f);
    }



    public void EditButton()
    {
        soundManager.ButtonfxEffect();
        isEditMode = !isEditMode;
        usernameInput.interactable = isEditMode;

        if (usernameInput.text != PlayerPrefs.GetString("username"))
        {
            PlayerPrefs.SetString("username", usernameInput.text);
        }
    }

    public void BackButton()
    {

        soundManager.ButtonfxEffect();
        PlayerPrefs.SetFloat("fxvolume", Fxvolumeset.value);
        PlayerPrefs.SetFloat("musicvolume", MusicVolumeSet.value);
        SettingsScreen.GetComponent<CanvasGroup>().DOFade(1, .2f);
        SettingsScreen.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f).OnComplete(() =>
        {
            isSettingTab = false;

            SettingsScreen.SetActive(isSettingTab);
            

        });
        
    }
}
