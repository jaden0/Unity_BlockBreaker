using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    bool hasStarted = false;
    [SerializeField] public float ScreenWith = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] AudioClip bounceSound;
    GameSession theGameSession;
    Ball theBall;
    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * ScreenWith;
        Vector2 paddlePos = new Vector2(mousePosInUnits, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
        }
    }

    private float GetXPos()
    {
        if (theGameSession.IsAutoplayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            //return Input.mousePosition.x / Screen.width * ScreenWith;
            return Camera.main.ScreenToWorldPoint(Input.mousePosition)[0];
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
             AudioSource.PlayClipAtPoint(bounceSound, Camera.main.transform.position);
        }
            
    }
}