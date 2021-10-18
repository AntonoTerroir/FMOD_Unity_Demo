using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODCharacterLocomotion : MonoBehaviour
{
    public string materialParamName = "";
    public float currentMaterialValue = 0f;
    public FMODUnity.EventReference footstepsEvent;
    

    public void PlayFootstepsEvent()
    {
        FMOD.Studio.EventInstance footsteps = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footsteps, GetComponent<Transform>(), GetComponent<Rigidbody>());
        footsteps.setParameterByName(materialParamName, currentMaterialValue);
        footsteps.start();
        footsteps.release();
    }
}
