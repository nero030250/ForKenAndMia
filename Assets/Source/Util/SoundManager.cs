using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : UISingleton <SoundManager> {
	public void Play (string sound) {
		GetGameObject (sound).GetComponent <AudioSource> ().Play ();
	}

	public void Stop (string sound) {
		GetGameObject (sound).GetComponent <AudioSource> ().Stop ();
	}
}