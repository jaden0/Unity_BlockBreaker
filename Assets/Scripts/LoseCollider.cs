using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoseCollider : MonoBehaviour
{
    private GameObject[] balls;
    private int overlapColliders;
    private ContactFilter2D contactFileter;

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {   if (collisionObject.tag == "ball")
        {
            collisionObject.gameObject.SetActive(false);
            Destroy(collisionObject.gameObject);
            balls = GameObject.FindGameObjectsWithTag("ball");
            Debug.Log("balls is an array of length: " + balls.Length);
            if (balls.Length == 0)
            {
                SceneManager.LoadScene("Game Over");
            }
        }
    }
}
