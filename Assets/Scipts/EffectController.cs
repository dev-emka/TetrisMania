using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public ParticleSystem[] AllEffects;
    public ParticleSystem[] AllEffectss;

    private void Start()
    {
        AllEffects = GetComponentsInChildren<ParticleSystem>();
    }

    public void EffectPlay()
    {
        foreach(ParticleSystem effect in AllEffects)
        {
            effect.Stop();
            effect.Play();
        }
    }

    public void GameOverStarEffect()
    {
        foreach(ParticleSystem ef in AllEffectss)
        {
            ef.Stop();
            ef.Play();
            Debug.Log("star");
        }
    }
}
