using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeAudioSnapshot : MonoBehaviour {
    public AudioMixerSnapshot Snapshot;
    public float SecondsToTransition;
	// Use this for initialization
	void Start () {
        Snapshot.TransitionTo(SecondsToTransition);
	}
}
