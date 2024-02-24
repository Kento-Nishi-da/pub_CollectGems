using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick03 : MonoBehaviour
{
    public GameObject TrianglePrefab;
    //public float timer = 1.5f;
    [SerializeField] bool isAvtive;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        startPos = transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {

        if (isAvtive)
        {
            transform.GetChild(0).position += new Vector3(0, 0.01f, 0);
        }
        else
        {
            transform.GetChild(0).position = startPos;
        }
        //Destroy(gameObject, timer)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Player")
        {
            //Instantiate(TrianglePrefab, transform.position, transform.rotation);
            transform.GetChild(0).gameObject.SetActive(true);
            isAvtive = true;
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            isAvtive = false;
        }
    }
}
