using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 dir;
    float speed;
    public bool isFromPlayer;
    
    public void Init(Vector2 myDir, float mySpeed, Color myColor, float scale, bool _isFromPlayer)
    {        
        dir = myDir;    
        speed = mySpeed;

        GetComponent<SpriteRenderer>().color = myColor;

        //transform.localPosition = Vector2.zero;
        transform.localScale *= scale;
        isFromPlayer = _isFromPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir*speed*Time.deltaTime);

        if (transform.position.y >= GameManager.topRight.y)
        {
            Destroy(gameObject);
        }
    }

    public int GetDamage()
    {
        return 30;
    }
}
