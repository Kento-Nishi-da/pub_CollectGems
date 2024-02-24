using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureBox : MonoBehaviour
{
    Transform gauge;

    [SerializeField] GameObject gemPrefab;

    // Start is called before the first frame update
    void Start()
    {
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

            Instantiate(gemPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        
    }
}
