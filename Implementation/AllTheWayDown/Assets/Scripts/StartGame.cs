using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPointerClickHandler
{
    public Sprite playImage;
    public Sprite lockImage;
    public void OnPointerClick(PointerEventData eventData)
    {
        var stage = PlayerPrefs.GetInt("stage", 0);
        switch (gameObject.name)
        {
            case "stage0": 
                MenuManager.inst.startGame(0);
                break;
            case "stage1":
                if (stage >= 1)
                {
                    MenuManager.inst.startGame(1);
                }
                break;
        }
    }

    private void Start()
    {
        var stage = PlayerPrefs.GetInt("stage", 0);
        switch (gameObject.name)
        {
            case "stage1":
                if (stage < 1)
                {
                    gameObject.GetComponent<Image>().sprite = lockImage;
                }
                else
                {
                    gameObject.GetComponent<Image>().sprite = playImage;
                }
                break;
        }
    }
}