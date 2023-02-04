using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioSource[] musicTracks;
    private int currentTrack = 0;
    private AudioSource chosenOne;
	public int musicLayers;

    private void Start()
    {
        PlayMusicLayers();
    }

    public void PlayMusicLayers()
    {
        if (musicTracks.Length == 0) return;

	for(int i = 0; i < musicLayers; i++){
	musicTracks[i].mute = true;
	}

	for(int i = 0; i < musicLayers; i++){
	chosenOne = musicTracks[Random.Range(0,musicTracks.Length)];
	chosenOne.mute = false;
	chosenOne.Play();
	}
	
    }

	public void Update()
	{
		if(!chosenOne.isPlaying) PlayMusicLayers();		
	}
}
