using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundAudioClip
{
    public SoundManager.Sound sound;
    public AudioClip audioClip;
}

public class SoundManager : MonoBehaviour
{
    //https://www.youtube.com/watch?v=QL29aTa7J5Q

    public SoundAudioClip[] soundAudioClipArray;
    public enum Sound{
        PlayerMove,
        PlayerDie,
        EnemyDetect,
        EnemyMove,
        ButtonPress,

    }
    private  Dictionary<Sound, float> soundTimerDictionary;
    private GameObject oneShotGameObject;
    private AudioSource oneShotAudioSource;
    void Initialize()
    {
        //initiliaze dictionary
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0.0f;
        soundTimerDictionary[Sound.EnemyMove] = 0.0f;
    }
    public SoundManager instance;
    private void Start() {
        if (instance == null)
            instance = this;

        Initialize();
    }
    public void PlaySound(Sound sound, Vector3 position)
    {
        //for 3d sounds
        if (CanPlaySound(sound))
        {   GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            //make sure the audiosource clip is set, as play doesnt take a parameter
            audioSource.clip = GetAudioClip(sound);
            //here we can do some edits to the audio
            audioSource.maxDistance = 100f;
            //audioSource.minDistance
            audioSource.spatialBlend = 0.9f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();

            //we only use this once, can destroy now. can't create one instanc ebefore of the positioning
            //wait until it finishes playing to destroy
            //do object pooling potentially to improve performance
            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }
   public void PlaySound(Sound sound)
   {
        if (CanPlaySound(sound))
        {   
            if (oneShotGameObject == null)
            {
                 oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource= oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }

   }
   bool CanPlaySound(Sound sound)
   {
        switch (sound)
        {
            //most of the time, we can play sounds
            default:
                return true;
            //we can't spam sound while the player is moving, or it won't work
             case Sound.PlayerMove:
            //do we have that sound?
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    //delay between each time
                    float playerMoveTimerMax = 1f;
                    //if the last time plus the delay is less than the current time (how much time has passed), we can play it
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        //updating the dictionary with the current time
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return true; 
            //same deal as the player moving
            case Sound.EnemyMove:
                //do we have that sound?
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    //delay between each time
                    float playerMoveTimerMax = 1.0f;
                    //if the last time plus the delay is less than the current time (how much time has passed), we can play it
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        //updating the dictionary with the current time
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return true;
            
        }
   }
   AudioClip GetAudioClip(Sound sound)
   {
    foreach (SoundAudioClip soundAudioClip in soundAudioClipArray)
    {
        if (soundAudioClip.sound == sound)
        {
            return soundAudioClip.audioClip;
        }
    }
    Debug.LogError("Sound " + sound +" not found!");
    return null;
   }
}
