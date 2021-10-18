using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStateTrigger : MonoBehaviour
{
    public float enterValue = 1f;

    // Valeur du param�tre correspondant au mat�riau de base
    public float exitValue = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("music_state_param", enterValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("music_state_param", exitValue);
        }
    }
}
