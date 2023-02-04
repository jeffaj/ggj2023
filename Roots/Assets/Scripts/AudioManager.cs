using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    #region Inspector

    [SerializeField]
	public List<AudioClip> drillClips;
    public List<AudioClip> dirtbreakClips;
	public List<AudioClip> rockdrillClips;
	public List<AudioClip> fuelbreakClips;
	public List<AudioClip> artbreakClips;

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

    // chooses a random clip from the list
    private void PlayOneShot(List<AudioClip> clips)
    {
        var idx = Random.Range(0, clips.Count);
        PlayOneShot(clips[idx]);
    }

    // example audio
    public void PlayDrill()
    {
        PlayOneShot(drillClips);
    }

    public void PlayDirtBreak()
    {
        PlayOneShot(dirtbreakClips);
    }

    public void PlayRockDrill()
    {
        PlayOneShot(rockdrillClips);
    }
    public void PlayFuelBreak()
    {
        PlayOneShot(fuelbreakClips);
    }
    public void PlayArtBreak()
    {
        PlayOneShot(artbreakClips);
    }
}
