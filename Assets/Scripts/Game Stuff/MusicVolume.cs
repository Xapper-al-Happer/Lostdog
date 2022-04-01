using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicVolume : MonoBehaviour
{
    public FloatValue music;
    private AudioSource audioComponent;
    private static MusicVolume audioInstance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (audioInstance == null)
        {
            audioInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioComponent = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        audioComponent.volume = music.RuntimeValue;
    }
}
