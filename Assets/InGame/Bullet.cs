using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletState
    {
        STANDBY,
        BUSY,
    }

    [SerializeField]
    private int hp = 1;

    [SerializeField]
    private int atk = 1;

    public int Atk => atk;

    private BulletState state;
    public BulletState State => state;

    private Vector3 vector;

    void Awake()
    {
        state = BulletState.STANDBY;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == BulletState.BUSY) this.transform.Translate(vector);
    }

    public void Shot(Vector3 position, Vector3 vec)
    {
        state = BulletState.BUSY;
        vector = vec;
        this.gameObject.transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"bullet hit <{other.gameObject.tag}>");
        // 敵、壁に当たったら初期位置に戻す
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Wall"))
        {
            state = BulletState.STANDBY;
            this.gameObject.transform.position = new Vector3(100.0f, 100.0f, 0.0f);
        }
    }
}
