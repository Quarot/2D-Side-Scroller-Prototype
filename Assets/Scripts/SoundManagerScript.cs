using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    public static AudioClip playerJump, playerDeath, checkpointGet;
    static AudioSource audioSrc;

	// Use this for initialization
	void Start () {
		playerJump = Resources.Load<AudioClip> ("jump");
        playerDeath = Resources.Load<AudioClip>("playerDeath4");
        checkpointGet = Resources.Load<AudioClip>("checkpoint");
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "doJump":
                audioSrc.PlayOneShot(playerJump);
                //Debug.Log("Playing jump sound effect.");
                break;
            case "playerDies":
                Debug.Log("Starting death sound effect.");
                audioSrc.PlayOneShot(playerDeath);
                //Debug.Log("Completed death sound effect.");
                break;
            case "hitCheckpoint":
                audioSrc.PlayOneShot(checkpointGet);
                //Debug.Log("Playing jump sound effect.");
                break;
        }
    }
}
