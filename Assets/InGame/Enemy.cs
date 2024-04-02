using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int hp = 100;
    [SerializeField]
    private int atk = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void Move()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name); 
        if(collision.gameObject.CompareTag("Bullet"))
        {
            var bullet = collision.gameObject.GetComponent<Bullet>();
            hp -= bullet.Atk;
        }
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }  
    }
}
