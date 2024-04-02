using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;

    [SerializeField]
    private float shotInterval = 0.1f;

    [SerializeField]
    private int hp = 1;

    [SerializeField]
    private Vector3 acc;
    private Vector3 vel;

    void Start()
    {
        vel = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
        Move();
        Destroy();
    }

    private void InputKey()
    {
        // 左に移動
        if (Input.GetKey (KeyCode.A)) {
            acc -= new Vector3(0.001f, 0.0f, 0.0f);
        }
        // 右に移動
        if (Input.GetKey (KeyCode.D)) {
            acc -= new Vector3(-0.001f, 0.0f, 0.0f);
        }
        acc.x = Mathf.Clamp(acc.x, -0.01f, 0.01f);
    }

    private void Move()
    {
        vel.x = Mathf.Clamp(vel.x + acc.x, -0.01f, 0.01f);
        this.transform.position += vel;
    }

    private void Destroy()
    {
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
