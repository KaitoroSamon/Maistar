using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ���������N���X
/// </summary>
public class Rigider : MonoBehaviour
{
    [SerializeField, Tooltip("�d�͒l���C���X�y�N�^����ݒ�ł���悤��")]
    float pyhs = -1f;
    [SerializeField, Tooltip("�ő�����x")] float maxVelocity = 50;
    public Vector3 velocity;
    //�n�ʂɒ��������ǂ���
    public bool isGround { get { return transform.position.y <= 0; } }

    void Update()
    {
        //�󒆂̎��d�͂�������
        if (velocity.y > 0 || transform.position.y != 0)
        {
            //�ő�����x�ȉ��Ȃ�ړ��ʉ��Z
            if (Mathf.Abs(velocity.y) <= maxVelocity)
                velocity.y += pyhs;

            if (transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
                velocity.y = 0;
            }

        }

        //�ړ�
        transform.position += velocity * Time.deltaTime;
    }

    public void AddForce(Vector3 alpha)
    {
        velocity += alpha;
    }
}