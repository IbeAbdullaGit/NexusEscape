using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorPlayerSounds : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference _footsteps;
    private FMOD.Studio.EventInstance footsteps;

    private void Start()
    {
        footsteps = FMODUnity.RuntimeManager.CreateInstance(_footsteps);
        footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
    }
    public void PlayFootsteps()
    {
        //FMODUnity.RuntimeManager.PlayOneShotAttached(_footsteps, gameObject);
        if (footsteps.isValid())
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(footsteps, gameObject.transform);

            footsteps.setParameterByName("Softness", 0);
            footsteps.setParameterByName("Volume", 1f);
            footsteps.start();
        }
    }

    public void PlaySilentFootstep()
    {
        if (footsteps.isValid())
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(footsteps, gameObject.transform);
            Debug.Log("footstep");
            

            footsteps.setParameterByName("Softness", 1);
            footsteps.setParameterByName("Volume", 0.85f);
            footsteps.start();
        }
    }
}
