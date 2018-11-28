using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour {


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player entered the hazard.");
        //SoundManagerScript.PlaySound("death");
        //SoundManagerScript.PlaySound("playerDies");
        PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
        //player.Respawn();
        player.KillPlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the hazard.");
            //SoundManagerScript.PlaySound("death");
            //SoundManagerScript.PlaySound("playerDies");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            //player.Respawn();
            player.KillPlayer();
            
        }
        else
        {
        Debug.Log("Something other than player entered the hazard.");

        }
    }
}
