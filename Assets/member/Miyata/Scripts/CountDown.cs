using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public static float CountDownTime; // カウントダウンタイム
    public Text TextCountDown; // 表示用テキストUI
    // Start is called before the first frame update
    void Start()
    {
        CountDownTime = 60.0F;    // カウントダウン開始秒数をセット
    }

    // Update is called once per frame
    void Update()
    {
        // カウントダウンタイムを整形して表示
        TextCountDown.text = String.Format("Time: {0:00}", CountDownTime);
        // 経過時刻を引いていく
        CountDownTime -= Time.deltaTime;

        if (CountDownTime <= 0.0F)
        {
            //SceneManager.LoadScene ("GameClearScene");
        }
    }
}
