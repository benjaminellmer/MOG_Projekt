using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTouchManager : MonoBehaviour
{

    private Vector3 fp;
    private Vector3 lp;
    private Camera mainCamera;
    private MenuCamera menuCamera;

    private float dragDistance;
    // Start is called before the first frame update
    void Start()
    {
        dragDistance = Screen.height * 15 / 100;
        mainCamera = Camera.main;
        menuCamera = mainCamera.GetComponent<MenuCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase==TouchPhase.Moved)
            {
                lp = touch.position;
            }
            else if(touch.phase==TouchPhase.Ended)
            {
                lp = touch.position;
                if (Mathf.Abs(lp.x - fp.x) > dragDistance)
                {
                    if (lp.x > fp.x)
                    {
                        menuCamera.MoveBy(-30);
                        //left swipe
                    }
                    else
                    {
                        menuCamera.MoveBy(30);
                        //right swipe
                    }
                }
            }
        }
    }
}
