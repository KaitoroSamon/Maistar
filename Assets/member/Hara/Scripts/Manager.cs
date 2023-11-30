using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����蔻�茟�؃N���X
/// </summary>
public class Manager : MonoBehaviour
{
    [SerializeField] Director director = null;
    //�����蔻�茟�ؗp�̖�ƓI�̃��X�g
    static List<Col> arrows = new List<Col>();
    static List<Col> targets = new List<Col>();
    //�폜�p�̖�ƓI�̃��X�g
    static List<Col> removeArrows = new List<Col>();
    static List<Col> removeTargets = new List<Col>();
    static List<Col> hitTargets = new List<Col>();
    int arrowsNum = 0;
    private void Update()
    {
        if (arrows.Count > 0)
        {
            //�S�Ă̓I�ɑ΂��AarrowsNum �Ԃ̖�Ƃ̓����蔻�茟��
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].CheckHit(arrows[arrowsNum]))
                {
                    targets[i].OnHit();
                    arrows[arrowsNum].OnHit();
                }
            }
            arrowsNum++;
        }
        if (removeArrows.Count > 0)
        {
            foreach (Col c in removeArrows)
                arrows.Remove(c);

            removeArrows.Clear();
        }
        if (removeTargets.Count > 0)
        {
            foreach (Col c in removeTargets)
                targets.Remove(c);

            removeTargets.Clear();
        }
        if (hitTargets.Count > 0)
        {
            int total = 0;
            foreach (Col c in hitTargets)
            {
                targets.Remove(c);
                total += ((Target)c).point;
            }
            //�X�R�A�𔽉f
            director.Point(total);
            hitTargets.Clear();
        }

        //��̃J�E���g�����Z�b�g
        if (arrowsNum >= arrows.Count) arrowsNum = 0;

    }

    public static void AddArrows(Col c)
    {
        arrows.Add(c);
    }

    public static void AddTargets(Col c)
    {
        targets.Add(c);
    }

    public static void RemoveArrows(Col c)
    {
        removeArrows.Add(c);
    }

    public static void RemoveTargets(Col c)
    {
        removeTargets.Add(c);
    }

    public static void HitTargets(Col c)
    {
        hitTargets.Add(c);
    }
}