using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;

    [SerializeField]
    private float initShotInterval = 1f;

    [SerializeField]
    private float initShotPointInterval = 1f;
    public float InitShotPointInterval => initShotPointInterval;

    [SerializeField]
    private int hp = 1;

    [SerializeField]
    private Vector3 acc;
    private Vector3 vel;

    [SerializeField]
    private Rigidbody2D regidBody2D;

    // 弾の発射位置
    [SerializeField]
    private GameObject bulletPoint;

    private List<Bullet> bullets;

    // 弾の発射間隔
    private float shotInterval;

    // 弾の発射可能回数
    private int shotPoint;
    public int ShotPoint => shotPoint;

    // 弾の発射回数リロード時間
    private float shotPointInterval;
    public float ShotPointInterval => shotPointInterval;

    void Awake()
    {
        // プレイヤー初期化
        vel = new Vector3(0.0f, 0.0f, 0.0f);
        shotInterval = 0;

        // 弾の生成
        bullets = new List<Bullet>();
        for(var i = 0; i < 100; i++){
            bullets.Add(Instantiate(bullet, new Vector3(100.0f, 100.0f, 0.0f), quaternion.identity));
        }
    }

    void Update()
    {
        shotInterval -= Time.deltaTime;
        UpdateShotPoint();
        InputKey();
        Move();
        Destroy();
    }

    private void InputKey()
    {
        // 弾の発射
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Shot();
        }

        // 左に移動
        if (Input.GetKey(KeyCode.A))
        {
            if (acc.x > 0) acc.x = 0;

            acc += new Vector3(-0.001f, 0.0f, 0.0f);
        }
        // 右に移動
        if (Input.GetKey(KeyCode.D))
        {
            if (acc.x < 0) acc.x = 0;

            acc += new Vector3(0.001f, 0.0f, 0.0f);
        }
        acc.x = Mathf.Clamp(acc.x, -0.01f, 0.01f);
    }

    private void UpdateShotPoint()
    {
        shotPointInterval -= Time.deltaTime;
        if(shotPointInterval > 0) return;
        shotPointInterval = initShotPointInterval;
        shotPoint++;
        shotPoint = math.min(shotPoint++, 10);
    }

    private void Shot()
    {
        if(shotInterval > 0) return;
        if(shotPoint <= 0) return;
        shotPoint--;
        shotPoint = math.max(shotPoint, 0);
        var bullet = bullets.Find(x => x.State == Bullet.BulletState.STANDBY);
        if(bullet == null)
        {
            Debug.Log("弾切れ");
            return;
        }
        bullet.Shot(bulletPoint.transform.position, new Vector3(0.0f, 0.05f, 0.0f));
        shotInterval = initShotInterval;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        vel.x = 0.0f;
        acc.x = 0.0f;
    }
}
