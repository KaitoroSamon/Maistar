using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�N���X
/// </summary>
public class Target : Col
{
    [SerializeField] Parts effect = null;
    public int point { get; set; }
    Rigider rigid;
    Coroutine c;
    float jump;
    private void Start()
    {
        rigid = GetComponent<Rigider>();
        point = Random.Range(0, 10) + 1;

        //pint�ɔ�Ⴕ�čŒ�5�ő�30�Œ��˂�
        jump = point * 3;
        if (jump < 5) jump = 5;

        Manager.AddTargets(this);

        rigid.AddForce(Vector3.left * point / 2);
    }

    void Update()
    {
        //�n�ʂɒ������璵�˂�
        if (rigid.isGround) rigid.AddForce(new Vector3(0, jump));
        //��苗�����ꂽ��폜
        if (Camera.main.transform.position.x - transform.position.x > 30)
        {
            if (c == null) c = StartCoroutine(RemoveList());
        }
    }

    IEnumerator RemoveList()
    {
        Manager.RemoveTargets(this);
        //Manager�N���X�ł̃G���[�΍��1�t���[���x�点��
        yield return null;
        Destroy(gameObject);
    }

    public override void OnHit()
    {
        //�����蔻��폜
        Manager.HitTargets(this);
        //�����_����]
        transform.GetChild(0).Rotate(0, 0, Random.Range(0, 360));
        //�j��A�j���[�V����
        StartCoroutine(DestroyCoroutine());
        //�j��p�[�e�B�N��
        Parts p = Instantiate(effect, transform.position, transform.rotation, transform);
        p.particleSpeed = point;
    }

}