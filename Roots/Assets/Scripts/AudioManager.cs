using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    #region Inspector

    [SerializeField]
    public AudioClip drill;

    #endregion

    private static AudioManager _instance;

    public static AudioManager Instance => _instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError($"There can only be one {nameof(Game)}");
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    private void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 1);
    }

    // example audio
    public void PlayDrill()
    {
        PlayOneShot(drill);
    }
}
