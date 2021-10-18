using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;

    public void OnClickPlayButton()
    {
        menu.SetActive(false);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("music_state_param", 0.5f);
    }
}
