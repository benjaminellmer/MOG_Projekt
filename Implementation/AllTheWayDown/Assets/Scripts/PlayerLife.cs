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
            Die();
        }
        
    }

    /* TODO: Check if player collided with water, die on collision
    private void OnCollisionEnter(Collision other)
    {
        throw new NotImplementedException();
    }
    */

    bool PlayerFell()
    {
        return transform.position.y < -5;
    }

    void Die()
    {
        Invoke(nameof(ReloadLevel), 1f);
        playerDead = true;
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
