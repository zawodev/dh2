using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMenager : MonoBehaviour {

    public static AudioMenager audioMenager;
    public Sound[] sounds;
    public Sound[] musics;
    private void Awake() {

        if (audioMenager != null) {
            Destroy(gameObject);
            return;
        }
        else audioMenager = this;
        DontDestroyOnLoad(gameObject);
        //SOUNDS
        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;

            sound.source.loop = sound.loop;
        }
        //MUSICS
        foreach (Sound music in musics) {
            music.source = gameObject.AddComponent<AudioSource>();
            music.source.clip = music.clip;

            music.source.volume = music.volume;
            music.source.pitch = music.pitch;

            music.source.loop = music.loop;
        }
    }
    public void PlaySound(string soundName) {
        Sound sound = Array.Find(sounds, s => s.name == soundName);

        if (sound != null)
            sound.source.Play();
        else 
            Debug.LogWarning("Sound: " + name + "does not exist bro");
    }
    public void PlayMusic(string musicName) {
        Sound music = Array.Find(musics , m => m.name == musicName);

        if (music != null)
            music.source.Play();
        else
            Debug.LogWarning("Music: " + name + " does not exist bro");
    }
}
