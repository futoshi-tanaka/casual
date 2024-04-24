using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        STANDBY,
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

    private float changeVecInterval;

    private EnemyState enemyState;
    public EnemyState State => enemyState;

    public int enemyAIShotInterval;

    public float enemyStanbyTimer = 3.0f;

    void Awake()
    {
        // プレイヤー初期化
        vel = new Vector3(0.0f, 0.0f, 0.0f);
        shotInterval = 1;
        changeVecInterval = UnityEngine.Random.Range(0.5f, 2.0f);
        enemyState = EnemyState.STANDBY;

        // 弾の生成
        bullets = new List<Bullet>();
        for(var i = 0; i < 10; i++)
        {
            bullets.Add(Instantiate(bullet, new Vector3(100.0f, 100.0f, 0.0f), quaternion.identity));
        }
    }

    void Update()
    {
        shotInterval -= Time.deltaTime;
        enemyStanbyTimer -= Time.deltaTime;
        if(enemyStanbyTimer <= 0 && enemyState == EnemyState.STANDBY)
        {
            enemyState = EnemyState.ALIVE;
        }
        UpdateShotPoint();
        InputKey();
        Shot();
        Move();
        Defeat();
    }

    private void InputKey()
    {

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
        if(enemyState != EnemyState.ALIVE) return;
        if(shotInterval > 0) return;
        if(shotPoint <= 0) return;
        enemyAIShotInterval--;
        if(enemyAIShotInterval > 0) return;
        shotPoint--;
        shotPoint = math.max(shotPoint, 0);
        var bullet = bullets.Find(x => x.State == Bullet.BulletState.STANDBY);
        if(bullet == null)
        {
            Debug.Log("弾切れ");
            return;
        }
        bullet.Shot(Bullet.BulletUserType.ENEMY, bulletPoint.transform.position, new Vector3(0.0f, -0.05f, 0.0f));
        shotInterval = initShotInterval;
        enemyAIShotInterval = UnityEngine.Random.Range(0, 500);
    }

    private void Move()
    {
        vel.x = Mathf.Clamp(vel.x + acc.x, -0.01f, 0.01f);
        this.transform.position += vel;

        changeVecInterval -= Time.deltaTime;
        if(changeVecInterval > 0) return;
        acc.x = UnityEngine.Random.Range(-0.1f, 0.1f);
        changeVecInterval = UnityEngine.Random.Range(0.5f, 2.0f);
    }

    private void Defeat()
    {
        if(hp <= 0) enemyState = EnemyState.DEFEAT;
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
        acc.x *= -1;
        changeVecInterval = 0.5f;

        if(other.gameObject.CompareTag("Bullet"))
        {
            var bullet = other.gameObject.GetComponent<Bullet>();
            if(bullets.Any(x => x == bullet)) return;
            hp--;
        }
    }
}
