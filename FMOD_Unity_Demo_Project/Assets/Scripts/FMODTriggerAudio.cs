using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ce script permet de lancer un son
//Soit au lancement du jeu
//Soit à la destruction de l'objet
//Soit à activation / désactivation

public class FMODTriggerAudio : MonoBehaviour
{
    public FMODUnity.EventReference eventRef;

    public bool playOnStart = false;
    public bool playOnDestroy = false;
    public bool playOnEnable = false;
    public bool playOnDisable = false;

    private void Start()
    {
        if (playOnStart)
        {
            PlayOneShot();
        }
    }

    public void PlayOneShot()
    {
        // PlayOneShotAttached() jouera un son en fonction de l'objet spécifié
        // utile lorsque l'on ne veut pas forcément jouer un son dans l'espace 3D
        // à la différence de PlayOneShot(), qui demande un point dans l'espace 3D
        FMODUnity.RuntimeManager.PlayOneShotAttached(eventRef.Path, gameObject);
    }

    private void OnDestroy()
    {
        if (playOnDestroy)
        {
            PlayOneShot();
        }
    }

    private void OnEnable()
    {
        if (playOnEnable)
        {
            PlayOneShot();
        }
    }

    private void OnDisable()
    {
        if (playOnDisable)
        {
            PlayOneShot();
        }
    }
}
