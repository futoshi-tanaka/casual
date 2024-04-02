using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int hp = 100;

    [SerializeField]
    private int atk = 10;

    public int Atk => atk;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate (0.0f ,0.1f, 0.0f);
    }
}
