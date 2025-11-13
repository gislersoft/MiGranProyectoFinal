using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAudioOnStart3DSoundController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioSource audio = GetComponent<AudioSource>();
		audio.loop = true;
		AudioSource.PlayClipAtPoint(audio.clip, transform.position);
	}
}
