using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the checkpoint.");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            SoundManagerScript.PlaySound("hitCheckpoint");
        }
    }
}
