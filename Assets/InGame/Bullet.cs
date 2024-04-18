using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour, IPunObservable
{
    public enum BulletState
    {
        STANDBY,
        BUSY,
    }

    public enum BulletUserType
    {
        PLAYER,
        ENEMY,
    }

    [SerializeField]
    private int hp = 1;

    [SerializeField]
    private int atk = 1;

    public int Atk => atk;

    private BulletState state;
    public BulletState State => state;

    private Vector3 vector;

    private BulletUserType bulletUserType;

    void Awake()
    {
        state = BulletState.STANDBY;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == BulletState.BUSY) this.transform.Translate(vector);
    }

    public void Shot(BulletUserType user, Vector3 position, Vector3 vec)
    {
        bulletUserType = user;
        state = BulletState.BUSY;
        vector = vec;
        this.gameObject.transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"bullet hit <{other.gameObject.tag}>");
        if(other.gameObject.CompareTag("Bullet"))
        {
            return;
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(bulletUserType == BulletUserType.ENEMY) return;
        }
        if(other.gameObject.CompareTag("Player"))
        {
            if(bulletUserType == BulletUserType.PLAYER) return;
        }
        state = BulletState.STANDBY;
        this.gameObject.transform.position = new Vector3(100.0f, 100.0f, 0.0f);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Transformの値をストリームに書き込んで送信する
            stream.SendNext(transform.localPosition);
        }
        else
        {
            // 受信したストリームを読み込んでTransformの値を更新する
            var recievePosition = (Vector3)stream.ReceiveNext();
            transform.localPosition = new Vector3(recievePosition.x, recievePosition.y, recievePosition.z);
            transform.rotation = new Quaternion(0.5f, 0.0f, 0.0f, 0.0f);
        }
    }
}
