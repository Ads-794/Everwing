using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform spawnSpot;
    [SerializeField] Bullet bullet;
    float speed = 10f;
    float radius;
    Color color;

    public delegate void GainCoin();
    public event GainCoin onGainCoin;

    public delegate void PlayerDied();
    public event PlayerDied onPlayerDied;
    // Start is called before the first frame update
    void Start()
    {
        radius = transform.localScale.x / 2;

        color = GetComponent<SpriteRenderer>().color;

        InvokeRepeating("shootBullets", 0.5f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        if (transform.position.x + radius>= GameManager.topRight.x && moveHorizontal > 0 )
        {
            moveHorizontal = 0;            
        }
        if (transform.position.x - radius<= GameManager.bottomLeft.x && moveHorizontal < 0 )
        {
            moveHorizontal = 0;            
        }
        transform.Translate(moveHorizontal * Vector2.right);        
    }

    void shootBullets()
    {
        Bullet bulletGO = Instantiate (bullet);
        bulletGO.transform.position = spawnSpot.position;
               
        //bulletGO.Init(5f, GetComponent<SpriteRenderer>().color, 0.25f);  
        bulletGO.Init(Vector2.up, 8f, color, 0.25f, true );        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Coin")
        {
            if (onGainCoin != null)
            {
                onGainCoin();
            }
            Destroy(other.gameObject);
        }else if (other.transform.tag == "Enemy")
        {
            if (onPlayerDied != null)
            {   
                onPlayerDied();                
            }
        }else if (other.transform.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (!bullet.isFromPlayer)
            {
                if (onPlayerDied != null)
                {
                    onPlayerDied();
                }                
                Destroy(other.gameObject);
            }
        }
    }
}
