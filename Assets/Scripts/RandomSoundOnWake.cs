using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundOnWake : MonoBehaviour
{
    public List<AudioClip> audioClips;
    private AudioSource thisAudioSource;
    // Start is called before the first frame update

    void Awake()
    {
        thisAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //Tocar um som aleatório
        var audioClip = audioClips[Random.Range(0, audioClips.Count)];
        thisAudioSource.PlayOneShot(audioClip);

    }


}
