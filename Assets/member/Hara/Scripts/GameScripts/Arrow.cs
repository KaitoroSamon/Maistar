using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��R���g���[���[
/// </summary>
public class Arrow : Col
{
    [SerializeField] Parts isGroundEffect = null;
    public float Speed { get; set; }
    //Rigider rigid;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private bool isRigid;
    private bool isGround{ get { return transform.position.y <= 0; } }
    Rect range = new Rect(0, 0, 1, 1);
    bool prevGrounded = true;
    
    

    void Start()
    {
        // Manager.AddArrows(this);
        // rigid.velocity = Vector3.zero;
        // rigid.AddForce(transform.right * Speed);
    }

    public void ShoothingStar(float power)
    {
        Manager.AddArrows(this);
        rigid.velocity = Vector3.zero;
        rigid.AddForce(transform.right * power * Speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Floor"))
        {
            this.gameObject.transform.SetParent(other.gameObject.transform);
            col.isTrigger = true;
            isRigid = false;
            Destroy(rigid);
        }
    }

    void Update()
    {
        if (isRigid)
        {
            if (isGround && !prevGrounded) //���n�����u��
            {
                isRigid = false;
                RemoveList();//�����蔻��폜
                Vector3 pos = transform.position;
                pos.y = 0;
                Parts p = Instantiate(isGroundEffect, pos, Quaternion.identity, transform);
                p.maxParticles = (int)Speed / 2;
                p.particleSpeed = Speed / 10f;
                p.effectTime = 0.01f * Speed;


                Destroy(gameObject, 10);
            }
            prevGrounded = isGround;
        }

        if (rigid != null)
        {
            //�������]���[�V����
            transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg,
                Vector3.forward);
        }
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
