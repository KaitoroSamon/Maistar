using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLife : MonoBehaviour
{
    public int el;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(el==0)
        {
            SceneManager.LoadScene ("GameClearScene");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //当たったゲームオブジェクトがGroundタグだったとき
        if (collision.gameObject.CompareTag("Bullet"))
        {
            el--;
        }
    }
}
