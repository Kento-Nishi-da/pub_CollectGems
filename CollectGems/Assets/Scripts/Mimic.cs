using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mimic : MonoBehaviour
{
    Transform gauge;
    PlayerManager pm;
    GameManager gm;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        gauge = transform.GetChild(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gauge.localScale.x < 0)
        {
            Vector3 tmp = gauge.localScale;
            tmp.x = 0;
            gauge.localScale = tmp;

            animator.SetTrigger("Open");
            pm.PlayerDamage();
            gm.SEPlay(gm.seMimic);
            gm.MessageDisplay("しまった！ミミックだ！", true);

            Invoke("DestroyObject", 2.0f);
            
        }

    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
