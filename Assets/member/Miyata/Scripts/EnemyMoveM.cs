using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveM : MonoBehaviour
{
    public  float enemySpeed; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rb = this.transform.GetComponent<Rigidbody2D>();
        const float power = 20;
        rb.AddForce(Vector3.right * ((enemySpeed - rb.velocity.x) * power));
    }
}
