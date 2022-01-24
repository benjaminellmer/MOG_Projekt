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
        var stage = PlayerPrefs.GetInt("stage", 1);
        switch (gameObject.name)
        {
            case "stage1": 
                MenuManager.inst.startGame(0);
                break;
            case "stage2":
                if (stage >= 2)
                {
                    MenuManager.inst.startGame(2);
                }
                break;
        }
    }

    private void Update()
    {
        var stage = PlayerPrefs.GetInt("stage", 1);
        switch (gameObject.name)
        {
            case "stage2":
                if (stage >= 2)
                {
                    gameObject.GetComponent<Image>().sprite = playImage;
                }
                else
                {
                    gameObject.GetComponent<Image>().sprite = lockImage;
                }
                break;
        }
    }
}