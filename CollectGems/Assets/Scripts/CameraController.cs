using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //�J�������v���C���[��ǂ�������
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�̓������J����������
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(
            playerPos.x, playerPos.y, transform.position.z);
    }
}
