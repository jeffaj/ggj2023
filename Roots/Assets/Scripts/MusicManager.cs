using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    #region Inspector

    [SerializeField]
	public List<AudioClip> LevelMusic;
	public List<AudioClip> MenuMusic;

    #endregion

    private static MusicManager _instance;

    public static MusicManager Instance => _instance;

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

    private void PlayMusic(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 1);
    }

    // chooses a random clip from the list
    private void PlayMusic(List<AudioClip> clips)
    {
        var idx = Random.Range(0, clips.Count);
        PlayMusic(clips[idx]);
    }

    // example audio
    public void PlayLevelMusic()
    {
        PlayMusic(LevelMusic);
    }
}
