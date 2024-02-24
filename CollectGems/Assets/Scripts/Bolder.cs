using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bolder : MonoBehaviour
{
    public float timer = 3.0f;
    void Start()
    {
        Destroy(gameObject, timer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Player")
        {
            print("ƒ{ƒ^ƒ“‚ð‰Ÿ‚µ‚½");

        }

    }

 
}
