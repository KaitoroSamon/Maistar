using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��R���g���[���[
/// </summary>
public class Arrow : Col
{
    [SerializeField] Parts isGroundEffect = null;
    public float Speed { get; set; }
    Rigider rigid;
    Rect range = new Rect(0, 0, 1, 1);
    bool prevGrounded = true;

    void Start()
    {
        rigid = GetComponent<Rigider>();
        Manager.AddArrows(this);
        rigid.velocity = Vector3.zero;
        rigid.AddForce(transform.right * Speed);
    }

    void Update()
    {
        if (rigid.enabled)
        {
            if (rigid.isGround && !prevGrounded) //���n�����u��
            {
                rigid.enabled = false;
                RemoveList();//�����蔻��폜
                Vector3 pos = transform.position;
                pos.y = 0;
                Parts p = Instantiate(isGroundEffect, pos, Quaternion.identity, transform);
                p.maxParticles = (int)Speed / 2;
                p.particleSpeed = Speed / 10f;
                p.effectTime = 0.01f * Speed;


                Destroy(gameObject, 10);
            }
            prevGrounded = rigid.isGround;
        }

        //�������]���[�V����
        transform.rotation = Quaternion.AngleAxis(
            Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg,
            Vector3.forward);
    }

    void RemoveList()
    {
        Manager.RemoveArrows(this);
    }
    public override void OnHit()
    {
        RemoveList();
        //�����蔻��ł̃G���[�΍�Ƃ���1�b��ɍ폜
        Destroy(gameObject, 0.1f);
    }
}
