
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class MenuSoundManager : MonoBehaviour
{
    [SerializeField] Image SoundImage, FxImage;
    [SerializeField] Sprite SoundOnSprite, SoundOffSprite, FxOnSprite, FxOffSprite;

    [SerializeField] AudioClip[] musicClips;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip FxBtnEffect;
    [SerializeField] AudioSource FxBtnSource;
    AudioClip randomClip;

    bool MusicOnorOff;
    bool FxOnorOff;



    private void Start()
    {


        if (PlayerPrefs.HasKey("fxonoroff"))
        {
            PlayerPrefs.SetString("fxonoroff", "true");

        }

        if (PlayerPrefs.HasKey("musiconoroff"))
        {
            PlayerPrefs.SetString("musiconoroff", "true");

        }


        if (PlayerPrefs.GetString("musiconoroff") == "true")
        {
            MusicOnorOff = true;
        }
        else
        {
            MusicOnorOff = false;
        }

        if (PlayerPrefs.GetString("fxonoroff") == "true")
        {
            FxOnorOff = true;
        }
        else
        {
            FxOnorOff = false;
        }
        
        randomClip = RandomClip(musicClips);

        BackgroundMusic(randomClip);
    }

    private void Update()
    {

        SoundImage.sprite = (MusicOnorOff) ? SoundOnSprite : SoundOffSprite;
        FxImage.sprite = (FxOnorOff) ? FxOnSprite : FxOffSprite;


        FxBtnSource.volume = PlayerPrefs.GetFloat("fxvolume");
        musicSource.volume = PlayerPrefs.GetFloat("musicvolume");

        if (PlayerPrefs.GetString("musiconoroff") == "true")
        {
            MusicOnorOff = true;
        }
        else
        {
            MusicOnorOff = false;
        }

        if (PlayerPrefs.GetString("fxonoroff") == "true")
        {
            FxOnorOff = true;
        }
        else
        {
            FxOnorOff = false;
        }
    }

    AudioClip RandomClip(AudioClip[] clips)
    {
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        return randomClip;
    }

    public void BackgroundMusic(AudioClip musicClip)
    {
        if (!musicClip || !musicSource || !MusicOnorOff)
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
                randomClip = RandomClip(musicClips);
                BackgroundMusic(randomClip);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }
    public void ButtonfxEffect()
    {
        if (FxOnorOff)
        {
            FxBtnSource.clip = FxBtnEffect;
            FxBtnSource.Stop();
            FxBtnSource.Play();
        }
        
    }


    public void FxOnorOffBtn()
    {
        FxOnorOff = !FxOnorOff;
        if (FxOnorOff)
        {
            PlayerPrefs.SetString("fxonoroff", "true");
        }
        else
        {
            PlayerPrefs.SetString("fxonoroff", "false");
        }
    }
    public void MusicOnorOffBtn()
    {
        MusicOnorOff = !MusicOnorOff;

        if (MusicOnorOff)
        {
            PlayerPrefs.SetString("musiconoroff", "true");
        }
        else
        {
            PlayerPrefs.SetString("musiconoroff", "false");
        }
        MusicUpdate();
    }
}
