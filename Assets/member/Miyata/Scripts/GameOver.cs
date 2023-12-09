using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOver : MonoBehaviour
{
    [SerializeField] StartManager sm;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //当たったゲームオブジェクトがGroundタグだったとき
        if (collision.gameObject.CompareTag("Player"))
        {
            sm.FinishGame();
        }
    }
}
