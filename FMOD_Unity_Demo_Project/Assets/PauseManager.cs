using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public FMODUnity.EventReference pauseSnapshotRef;
    public FMOD.Studio.EventInstance pauseSnapshotInstance;
    private bool isPaused = false;
    public GameObject pauseUI;

    private void Start()
    {
        pauseSnapshotInstance = FMODUnity.RuntimeManager.CreateInstance(pauseSnapshotRef);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pauseSnapshotInstance.start();
                isPaused = true;
                pauseUI.SetActive(true);
            }

            else
            {
                pauseSnapshotInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                isPaused = false;
                pauseUI.SetActive(false);
            }
        }
    }
}
