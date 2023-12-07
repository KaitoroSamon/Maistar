using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����蔻��N���X
/// </summary>
public class Col : MonoBehaviour
{
    //�����蔻�蔼�a
    public float radius;
    //�ʒu����
    public Vector3 offset;
    /// <summary>
    /// �����蔻��
    /// </summary>
    /// <param name="col">���؃R���C�_�[</param>
    /// <returns>����</returns>
    public bool CheckHit(Col col)
    {
        return (transform.position + offset - col.transform.position + col.offset).sqrMagnitude
            < (radius + col.radius) * (radius + col.radius);
    }
    /// <summary>
    /// �Փˎ��ɌĂ΂��֐�
    /// </summary>
    public virtual void OnHit()
    {
        print(gameObject.name);
    }

    /// <summary>
    /// �j��A�j���[�V����
    /// </summary>
    /// <returns></returns>
    protected IEnumerator DestroyCoroutine()
    {
        //�}�e���A���擾
        Material mat = GetComponentInChildren<SpriteRenderer>().material;
        float t = 0;
        //�V�F�[�_�𗘗p�����j��A�j���[�V����
        while (t < 1)
        {
            mat.SetFloat("_Threshold", t);
            t += Time.deltaTime * 2;
            yield return null;
        }

        Destroy(gameObject);
    }
}