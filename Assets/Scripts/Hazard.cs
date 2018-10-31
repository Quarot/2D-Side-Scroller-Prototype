using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the hazard.");
            //SoundManagerScript.PlaySound("death");
            //SoundManagerScript.PlaySound("playerDies");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.Respawn();
            //
            //SoundManagerScript.PlaySound("playerDies");
        }
        else
        {
        Debug.Log("Something other than player entered the hazard.");

        }
    }
}
