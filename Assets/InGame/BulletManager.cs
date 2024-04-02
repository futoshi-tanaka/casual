using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;

    private List<Bullet> bulletList;

    // Start is called before the first frame update
    void Start()
    {
        bulletList = new List<Bullet>();
        for(int i = 0; i < 100; i++)
        {
            bulletList.Add(Instantiate(bullet));
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate (0.0f ,0.1f, 0.0f);
    }
}
