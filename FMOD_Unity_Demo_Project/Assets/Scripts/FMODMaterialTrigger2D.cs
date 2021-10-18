using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMaterialTrigger2D : MonoBehaviour
{
    // Script à attacher à un collider posé au dessus du matériau particulier
    // Par exemple le matériau du sol de base est la terre
    // Il y a quelques endroits en pierre
    // On pose ce collider au dessus des surfaces de pierre

    // Valeur du paramètre correspondant au matériau particulier
    public float enterMaterial = 2f;

    // Valeur du paramètre correspondant au matériau de base
    public float exitMaterial = 1f;

    public float currentMaterial = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            currentMaterial = enterMaterial;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            currentMaterial = exitMaterial;
        }
    }

    public void PlayFootstepsEvent(string path)
    {
        FMOD.Studio.EventInstance footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
        footsteps.setParameterByName("Material", currentMaterial);
        footsteps.start();
        footsteps.release();
    }
}
