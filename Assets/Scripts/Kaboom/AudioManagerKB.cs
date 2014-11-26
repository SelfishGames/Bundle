using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManagerKB : MonoBehaviour
{
    public List<AudioSource> soundEffectsList;

    public GameObject soundFXObject,
        musicObject;

    [HideInInspector]
    public AudioSource[] soundFXList,
        musicList;

    // Use this for initialization
    void Start()
    {
        soundFXList = soundFXObject.GetComponents<AudioSource>();
        musicList = musicObject.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(AudioSource SFX in soundFXList)
        {
            SFX.volume = PlayerPrefs.GetFloat("prefSFXVol") / 100;
        }

        foreach(AudioSource music in musicList)
        {
            music.volume = PlayerPrefs.GetFloat("prefMusicVol") / 100;
        }
    }

    public void DropperSlide()
    {
        if (soundFXList[0].isPlaying)
            soundFXList[0].Stop();

        soundFXList[0].Play();
    }
}
