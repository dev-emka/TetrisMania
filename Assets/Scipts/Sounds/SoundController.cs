using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SoundController : MonoBehaviour
{
    [SerializeField] Image SoundImage,FxImage;
    [SerializeField] Sprite SoundOnSprite,SoundOffSprite,FxOnSprite,FxOffSprite;
    [SerializeField] Text levelText;
    [SerializeField] Slider MusicVolumeSet,FxVolumeSet;
    public static SoundController instance;
    

    [SerializeField] AudioClip[] musicClips; 
    [SerializeField] AudioSource musicSource;
    
    [SerializeField] AudioSource lineEffectSource;
    [SerializeField] AudioClip lineEffectClip;
    
    [SerializeField] AudioSource levelEffectSource;
    [SerializeField] AudioClip levelEffectClip;
     
    [SerializeField] AudioSource GameOverEffectSource;
    [SerializeField] AudioClip GameOverEffectClip;

    [SerializeField] AudioClip btnClip;
    [SerializeField] AudioClip shapeClip;
    [SerializeField] AudioClip shapeClipdown;
    [SerializeField] AudioClip shapeClipValid;

    [SerializeField] AudioSource DownEffectSource;
    [SerializeField] AudioSource GridEffectSource;


    public static bool FxOnorOff=true;
    public static bool MusicOnorOff=true;

    AudioClip randomClip;

    
    private void Awake()
    {
        instance=this;
   
    }

    private void Start()
    {
        randomClip = RandomClip(musicClips);

        BackgroundMusic(randomClip);
    }

    private void Update()
    {
        
       
        levelEffectSource.volume = FxVolumeSet.value;
        
        lineEffectSource.volume=FxVolumeSet.value;
        
        DownEffectSource.volume = FxVolumeSet.value;
       

        musicSource.volume=MusicVolumeSet.value;
        SoundImage.sprite = (MusicOnorOff) ? SoundOnSprite : SoundOffSprite;
        FxImage.sprite = (FxOnorOff) ? FxOnSprite : FxOffSprite;
    }

    AudioClip RandomClip(AudioClip[] clips)
    {
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        return randomClip;
    }

    public void BackgroundMusic(AudioClip musicClip)
    {
        if (!musicClip||!musicSource||!MusicOnorOff)
        {
            return;
        }

        musicSource.clip = musicClip;
        musicSource.Play();
    }

    void MusicUpdate()
    {
        if (musicSource.isPlaying != MusicOnorOff)
        {
            if (MusicOnorOff)
            {
                randomClip=RandomClip(musicClips);
                BackgroundMusic(randomClip);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }
    public void LinesDelEffect()
    {
        if (FxOnorOff)
        {
            lineEffectSource.clip = lineEffectClip;
            lineEffectSource.Stop();
            lineEffectSource.Play();
        }
    }

    public void LevelSkipEffect()
    {
        if (FxOnorOff&&levelEffectSource.isPlaying==false)
        {
            levelEffectSource.clip=levelEffectClip;
            levelText.GetComponent<RectTransform>().DOScale(1.5f, 1f).SetLoops(2,LoopType.Yoyo);
            levelEffectSource.Play();
        }
    }

    public void GameOverEffectSound()
    {
        if (FxOnorOff)
        {
            musicSource.volume = 0.2f;
            GameOverEffectSource.clip = GameOverEffectClip;
            GameOverEffectSource.Play();
        }
    }
    public void FxOnorOffBtn()
    {
        FxOnorOff = !FxOnorOff;
        
    }
    public void MusicOnorOffBtn()
    {
        MusicOnorOff= !MusicOnorOff;
        
        MusicUpdate();
    }

    public void BtnEffect()
    {
        if (FxOnorOff)
        {
            lineEffectSource.clip = btnClip;
            lineEffectSource.Stop();
            lineEffectSource.Play();
        }
    }

    public void ShapeSoundEffect()
    {
        if (FxOnorOff)
        {
            lineEffectSource.clip = shapeClip;
            lineEffectSource.Stop();
            lineEffectSource.Play();
        }
    }
    public void ShapeSoundEffectDown()
    {
        if (FxOnorOff&&!DownEffectSource.isPlaying)
        {
            DownEffectSource.clip = shapeClipdown;
            DownEffectSource.Stop();
            DownEffectSource.Play();
        }
    }
    public void ShapeIsGridValid()
    {
        if (FxOnorOff)
        {
            GridEffectSource.clip = shapeClipValid;
            GridEffectSource.Stop();
            GridEffectSource.Play();
        }
    }
    
}
