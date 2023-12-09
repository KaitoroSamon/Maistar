using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// �Q�[���Ǘ���
/// </summary>
public class Director : MonoBehaviour
{
    [SerializeField, Tooltip("")] GameObject target = null;
    [SerializeField, Tooltip("")] Text scoreText = null;
    [SerializeField, Tooltip("")] Text timeText = null;
    public UnityEvent toResult; //���ʕ\���C�x���g
    public static float cameraDirection = 0; //�J��������������
    int score = 0;
    int nowScore = 0;
    float timer = 0;
    float time = 0;
    float countDown = 0;
    Coroutine scoreCoroutine = null;
    int mode = 0;

    void Update()
    {
        if (mode == 1)
        {
            //�I����
            time += Time.deltaTime;
            if (time >= timer)
            {
                time = 0;
                RandomTime();
                InstTarget();
            }

            //�J�E���g�_�E��
            countDown -= Time.deltaTime;
            if ((int)countDown <= 0)
            {
                toResult.Invoke();
            }
            timeText.text = (int)countDown + "";
        }

    }
    void GameStart()
    {
        time = 0;
        nowScore = score = 0;
        countDown = 30;
        RandomTime();
        InstTarget();
        Point(0);
    }
    void RandomTime()
    {
        timer = Random.Range(1, 5);
    }
    void InstTarget()
    {
        //�J��������̏ꏊ�œI����
        Instantiate(target,
            Camera.main.transform.position + Vector3.right * 35,
            Quaternion.identity);
    }

    public void Point(int point)
    {
        if (mode != 1) return;
        score += point * 10;
        if (scoreCoroutine == null)
            scoreCoroutine = StartCoroutine(Scorerer());
    }
    //�X�R�A���ω�����R���[�`��
    IEnumerator Scorerer()
    {
        while (nowScore <= score)
        {
            scoreText.text = "Score:" + nowScore++;
            yield return null;
        }
        scoreCoroutine = null;
    }
    //�C���X�y�N�^����Q��
    public void ChangeMode(int mode)
    {
        this.mode = mode;
        switch (mode)
        {
            case 0:
                break;

            case 1:
                GameStart();
                break;

            case 2:
                break;
        }
    }
}