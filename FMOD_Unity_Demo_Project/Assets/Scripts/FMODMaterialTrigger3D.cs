using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMaterialTrigger3D : MonoBehaviour
{
    // Script à attacher à un collider posé au dessus du matériau particulier
    // Par exemple le matériau du sol de base est la terre
    // Il y a quelques endroits en pierre
    // On pose ce collider au dessus des surfaces de pierre
    
    // Valeur du paramètre correspondant au matériau particulier
    public float enterMaterial = 1f;

    // Valeur du paramètre correspondant au matériau de base
    public float exitMaterial = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<FMODCharacterLocomotion>().currentMaterialValue = enterMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<FMODCharacterLocomotion>().currentMaterialValue = exitMaterial;
        }
    }
}
