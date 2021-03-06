using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rd = GetComponent<Rigidbody2D>();
        float xForce = Random.Range (-50f, 50f);
        float yForce = Random.Range (50f, 150f);
        Vector2 force = new Vector2 (xForce, yForce);

        rd.AddForce(force);        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y + transform.localScale.y < GameManager.bottomLeft.y)
        {
            Destroy(gameObject);            
        }        
    }
}
