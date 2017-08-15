using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TitleMusic : MonoBehaviour {
    public static TitleMusic music;
    public List<AudioClip> audioClips;
    public bool lastSongWasTitle;

    // Use this for initialization
    void Start() {
        // Set Don't destroy if this is the first time GameManager is Instantiated
        if (music == null)
        {
            DontDestroyOnLoad(gameObject);
            music = this;
        }
        // If the current object is not the same as the one referenced destroy it
        else if (music != this)
        {
            Destroy(gameObject);
        }

        AudioSource audio = GetComponent<AudioSource>(); ;
        MusicInfo m = GameObject.Find("Main Camera").GetComponent<MusicInfo>();
        audio.clip = audioClips[m.songIndex];
        audio.Play();

        SceneManager.activeSceneChanged += SceneChanged;
    }
    
    void SceneChanged(Scene prevScene, Scene activeScene)
    {
        AudioSource audio = GetComponent<AudioSource>();
        MusicInfo m = GameObject.Find("Main Camera").GetComponent<MusicInfo>();
        Debug.Log("Changing Song: " + m.songIndex + " " + m.isTitleSong);
        if (!m.isTitleSong || !lastSongWasTitle)
        {
            Debug.Log("Changing Song: " + m.songIndex + " " + m.isTitleSong);
            audio.Stop();
            audio.clip = audioClips[m.songIndex];
            audio.Play();
        }

        lastSongWasTitle = m.isTitleSong;
    }
}
