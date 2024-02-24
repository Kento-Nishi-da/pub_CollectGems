using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManeger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TitleButton();
        }
    }

    public void TitleButton()
    {
        print("ƒ{ƒ^ƒ“‚ð‰Ÿ‚µ‚½");
        SceneManager.LoadScene("Title");
    }
}
