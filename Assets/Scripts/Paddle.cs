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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * ScreenWith;
        Vector2 paddlePos = new Vector2(mousePosInUnits, transform.position.y);
        paddlePos.x = Mathf.Clamp(mousePosInUnits, minX, maxX);
        transform.position = paddlePos;
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
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