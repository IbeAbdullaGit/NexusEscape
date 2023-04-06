using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSounds : MonoBehaviour
{
    private FMODUnity.EventReference _footsteps;
    private FMOD.Studio.EventInstance footsteps;

    [SerializeField]
    private FMODUnity.EventReference distracted;

    [SerializeField]
    private FMODUnity.EventReference heard;

    [SerializeField]
    private FMODUnity.EventReference spotted;//player spotted

    private void Start()
    {
        //footsteps
        //_footsteps.Path = "event:/Sound Effects/Character/GuardFootstep";
        //_footsteps.Guid = new FMOD.GUID(new System.Guid("{20a12824-4999-4b96-b7b5-be5195076850}"));
        _footsteps = FMODUnity.RuntimeManager.PathToEventReference("event:/Sound Effects/Character/GuardFootstep");
        footsteps = FMODUnity.RuntimeManager.CreateInstance(_footsteps);
        footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
    }
    public void PlayFootsteps()
    {
        //FMODUnity.RuntimeManager.PlayOneShotAttached(_footsteps, gameObject);
        if (footsteps.isValid())
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(footsteps, gameObject.transform);

            footsteps.start();
        }
    }

    public void PlayDistracted()
    {
        //FMODUnity.RuntimeManager.PlayOneShotAttached(distracted, gameObject);
    }

    public void PlayHeard()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(heard, gameObject);
    }


    public void PlaySpotted()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(spotted, gameObject);
    }

}
