using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick02 : MonoBehaviour
{
    public GameObject CirclePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Player")
        {
            Instantiate(CirclePrefab, transform.position, transform.rotation);

        }

    }
}
