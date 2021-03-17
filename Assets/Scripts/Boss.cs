using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] Transform bulletSpawnSpot;
    [SerializeField] Bullet bulletPrefab;

    Color color;
    Transform player;

    public delegate void BossDied();
    public static event BossDied onBossDied;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        color = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        InvokeRepeating("ShootPlayer",2f,2f);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,Vector2.zero, Time.deltaTime * 3f);
    }

    void ShootPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        Bullet bulletGO = Instantiate(bulletPrefab, bulletSpawnSpot.position, Quaternion.identity) as Bullet;
        bulletGO.Init(dir, 3f, color, 0.4f,false);
    }

    protected override void Die()
    {
        if (onBossDied != null)
        {
            onBossDied();
        }
        base.Die();
    }

    public override int GetMaxHealth()
    {
        return 2000;
    }

    public override int GetCoins()
    {
        return 10;
    }
}
