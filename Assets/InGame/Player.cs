using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        ALIVE,
        DEFEAT,
    }

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

    [SerializeField]
    private Button shotButton;

    private List<Bullet> bullets;

    // 弾の発射間隔
    private float shotInterval;

    // 弾の発射可能回数
    private int shotPoint;
    public int ShotPoint => shotPoint;
    private static int shotPointMax = 10;

    // 弾の発射回数リロード時間
    private float shotPointInterval;
    public float ShotPointInterval => shotPointInterval;

    private PlayerState playerState;
    public PlayerState State => playerState;

    Vector3 previousPos, currentPos;

    void Awake()
    {
        // プレイヤー初期化
        playerState = PlayerState.ALIVE;
        vel = new Vector3(0.0f, 0.0f, 0.0f);
        shotInterval = 0;

        // 弾の生成
        bullets = new List<Bullet>();
        for(var i = 0; i < 10; i++)
        {
            bullets.Add(Instantiate(bullet, new Vector3(100.0f, 100.0f, 0.0f), quaternion.identity));
        }

        shotButton.onClick.AddListener(Shot);
    }

    void Update()
    {
        shotInterval -= Time.deltaTime;
        UpdateShotPoint();
        InputKey();
        Move();
        Defeat();
    }

    private void InputKey()
    {
        // スワイプによる移動処理
        var diffDistance = 0.0f;
        if (Input.GetMouseButtonDown(0))
        {
            previousPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            // スワイプによる移動距離を取得
            currentPos = Input.mousePosition;
            diffDistance = (currentPos.x - previousPos.x) / Screen.width;

            // タップ位置を更新
            previousPos = currentPos;
        }

        // 弾の発射
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Shot();
        }

        // 左に移動
        if (Input.GetKey(KeyCode.A) || diffDistance < 0)
        {
            if (acc.x > 0) acc.x = 0;

            acc += new Vector3(-0.001f, 0.0f, 0.0f);
        }
        // 右に移動
        if (Input.GetKey(KeyCode.D) || diffDistance > 0)
        {
            if (acc.x < 0) acc.x = 0;

            acc += new Vector3(0.001f, 0.0f, 0.0f);
        }
        acc.x = Mathf.Clamp(acc.x, -0.01f, 0.01f);
    }

    private void UpdateShotPoint()
    {
        if(shotPointMax == shotPoint)
        {
            shotInterval = 0;
            return;
        }
        shotPointInterval -= Time.deltaTime;
        if(shotPointInterval > 0) return;
        shotPointInterval = initShotPointInterval;
        shotPoint++;
        shotPoint = math.min(shotPoint, shotPointMax);
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
        bullet.Shot(Bullet.BulletUserType.PLAYER, bulletPoint.transform.position, new Vector3(0.0f, 0.05f, 0.0f));
        shotInterval = initShotInterval;
    }

    private void Move()
    {
        vel.x = Mathf.Clamp(vel.x + acc.x, -0.01f, 0.01f);
        this.transform.position += vel;
    }

    private void Defeat()
    {
        if(hp <= 0) playerState = PlayerState.DEFEAT;
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

        if(other.gameObject.CompareTag("Bullet"))
        {
            var bullet = other.gameObject.GetComponent<Bullet>();
            if(bullets.Any(x => x == bullet)) return;
            hp--;
        }
    }
}
