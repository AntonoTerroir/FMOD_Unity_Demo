using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODSoundManager : MonoBehaviour
{
    private static FMODSoundManager instance = null;
    public static FMODSoundManager sharedInstance
    {

        //Accesseur automatique
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<FMODSoundManager>();
            }
            return instance;
        }
    }

    public GameObject listenerCamera;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        musMainLoopInstance = FMODUnity.RuntimeManager.CreateInstance(musMainLoop);
        musMainLoopInstance.setParameterByName("music_state_param", 0);
        musMainLoopInstance.start();

        ambMainLoopInstance = FMODUnity.RuntimeManager.CreateInstance(ambMainLoop);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(ambMainLoopInstance, listenerCamera.GetComponent<Transform>(), listenerCamera.GetComponent<Rigidbody>());
        ambMainLoopInstance.start();
    }

    public void SetMusicState(float value)
    {
        musMainLoopInstance.setParameterByName("music_state_param", value);
    }

    [Header("UI")]

    public FMODUnity.EventReference genericClickSound;
    public FMODUnity.EventReference genericHoverSound;

    [Header("AMB")]

    public FMODUnity.EventReference ambMainLoop;
    private FMOD.Studio.EventInstance ambMainLoopInstance;

    public FMODUnity.EventReference ambFireSPT;

    [Header("MUS")]

    public FMODUnity.EventReference musMainLoop;
    private FMOD.Studio.EventInstance musMainLoopInstance;

    [Header("SFX")]

    public FMODUnity.EventReference sfxFSEvent;
}
