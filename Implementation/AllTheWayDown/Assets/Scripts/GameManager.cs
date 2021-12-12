using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public static GameManager inst;

    private void Awake()
    {
        inst = this;
    }

    public void IncScore()
    {
        ++score;
    }
}
