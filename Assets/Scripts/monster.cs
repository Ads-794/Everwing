using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : Enemy
{
    float monsterSpeed = 2f;


    // Update is called once per frame
    void Update()
    {   
        transform.Translate(Vector2.down * monsterSpeed * Time.deltaTime);  

        if (transform.position.y + transform.localScale.y <= GameManager.bottomLeft.y)
        {
            Destroy(gameObject);
        }      
    }

    public void SetSpeed(float speed)
    {
        monsterSpeed = speed;
    }

    public override int GetMaxHealth()
    {
        return 100;
    }

    public override int GetCoins()
    {
        return 3;
    }
}
