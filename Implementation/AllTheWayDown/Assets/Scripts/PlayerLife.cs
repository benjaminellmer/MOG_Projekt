using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private bool playerDead = false;

    void Update()
    {
        if (!playerDead && PlayerFell())
        {
            Die(false);
        }
        
    }
    
    /*
     * Just an additional check, in case of problems with the water.
     * In the normal case it should restart, because the player touches the water
     */
    bool PlayerFell()
    {
        return transform.position.y < -15;
    }

    void Die(bool instant)
    {
        GameManager.inst.FreezeTiles();
        if (instant)
        {
            ReloadLevel();
        }
        else
        {
            Invoke(nameof(ReloadLevel), 1f);
        }

        playerDead = true;
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Die(true);
        }
    }
}
