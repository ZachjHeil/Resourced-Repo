using UnityEngine.Audio;
using UnityEngine;


public enum SoundType {
    SFX,
    MUSIC,
    VOICE,
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public SoundType soundType;
    
    
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;
    public bool loop;
    
    public AudioSource source;
    

}
