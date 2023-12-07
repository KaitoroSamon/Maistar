using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �p�[�e�B�N���N���X
/// </summary>
public class Parts : MonoBehaviour
{
    [SerializeField, Tooltip("������")] Transform obj = null;
    [Tooltip("���[�v")]
    public bool loop = false;
    [Tooltip("�d��")]
    public bool isGravity = false;
    [Tooltip("�d�͒l")]
    public float gravity = -0.1f;
    [Tooltip("���˂邩")]
    public bool bounds = false;
    [Tooltip("�ő�p�[�e�B�N����")]
    public int maxParticles = 20;
    [Tooltip("�G�t�F�N�g�̗]��")]
    public float effectTime = 1;
    [Tooltip("�p�[�e�B�N���̗]��")]
    public float particlesLifeTime = 1;
    [Tooltip("�p�[�e�B�N���̑��x")]
    public float particleSpeed = 1;
    [Tooltip("�p�[�e�B�N���̏������_����]")]
    public bool randomRotate = false;
    [Tooltip("�p�[�e�B�N���̉�]")]
    public float particleRotate = 0;
    [Tooltip("�p�[�e�B�N�������ŏ��p�x")]
    public float minTimeInst = 0.01f;
    [Tooltip("�p�[�e�B�N���̓����_���T�C�Y")]
    public bool randomSize = false;

    P[] ps;
    int tale = 0;
    float time = 0;
    float timer = 0;
    float effectTimer = 0;

    /// <summary>
    /// ���Ɏg���p�[�e�B�N���̈ʒu��Ԃ�
    /// </summary>
    int Get
    {
        get
        {
            tale++;
            return tale % maxParticles;
        }
    }

    void Start()
    {
        ps = new P[maxParticles];
        for (int i = 0; i < maxParticles; i++)
        {
            float size = (randomSize ? Random.value : 1);
            Instantiate(obj, //�����I�u�W�F
                transform.position,//�ʒu�@�@��]��
           (randomRotate ? Quaternion.Euler(0, 0, Random.Range(0, 360)) : Quaternion.identity),
                transform)//�e�I�u�W�F
                .localScale = new Vector3(size, size);

            ps[i] = new P(Random.value + particleSpeed);
        }
        tale = maxParticles / 10;
        //1���͊J�n
        for (int i = 0; i < tale; i++)
            transform.GetChild(i).gameObject.SetActive(true);
        time = Random.Range(0, 0.2f);
    }

    void Update()
    {
        InstParticles();

        Transform t;
        for (int i = 0; i < transform.childCount; i++)
        {
            //�q�I�u�W�F�̒�����A�N�e�B�u�Ȃ̂�T��
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                t = transform.GetChild(i);

                ps[i].time += Time.deltaTime;
                if (ps[i].time > particlesLifeTime)
                {
                    ps[i].time = 0;
                    t.gameObject.SetActive(false);
                    continue;
                }

                if (isGravity) ps[i].direY += gravity;
                //�p�[�e�B�N���̈ړ�
                t.position += new Vector3(ps[i].direX * Time.deltaTime, ps[i].direY * Time.deltaTime);
                //�p�[�e�B�N���̉�]
                t.Rotate(0, 0, particleRotate * Time.deltaTime);
                if (bounds)
                    if (t.position.y <= 0)
                    {
                        t.position = new Vector3(t.position.x, 0, 0);
                        ps[i].direY *= -1;
                    }
            }
        }

    }

    /// <summary>
    /// �p�[�e�B�N������
    /// </summary>
    void InstParticles()
    {
        effectTimer += Time.deltaTime;
        if (!loop && effectTimer >= effectTime)//�G�t�F�N�g�̎���
        {
            //�p�[�e�B�N���������Ȃ�҂�
            foreach (Transform t in transform)
                if (t.gameObject.activeSelf) return;
            //�p�[�e�B�N���Ȃ��Ȃ�폜
            Destroy(gameObject);
        }
        else if (loop || tale < maxParticles)//�����܂ł͐���
        {
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                //���̐����܂ł̎��Ԃ������_���Őݒ�
                time = Random.Range(minTimeInst, 0.2f);
                int num = Get;
                transform.GetChild(num).gameObject.SetActive(true);
                transform.GetChild(num).position = transform.position;
            }
        }
    }

    /// <summary>
    /// �p�[�e�B�N���̈ړ������Ǝ������Ǘ�
    /// </summary>
    class P
    {
        public float direX = 0;
        public float direY = 0;
        public float time = 0;
        public P(float speed)
        {
            direX = Random.Range(-1, 1f) * speed;
            direY = Random.Range(-1, 1f) * speed;
            time = 0;
        }
    }
}