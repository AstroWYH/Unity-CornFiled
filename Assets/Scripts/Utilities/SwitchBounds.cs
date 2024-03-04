using Cinemachine;
using UnityEngine;

public class SwitchBounds : MonoBehaviour
{
    private void Start()
    {
        SwitchConfinerShape();
    }

    //��δ���Ŀ���ǣ������Ƶ�Bounds��ı߽�ʱ���������������ټ����ƶ�������ȥ���ʵ�ʱ���ֹͣ
    //ע�⽫Bounds��pylogon collider 2d��Is Trigger�򿪣���������ᱻ����ȥ
    private void SwitchConfinerShape()
    {
        PolygonCollider2D confinerShape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = confinerShape;
        //Call this if the bounding shape's points change at runtime
        confiner.InvalidatePathCache();
    }
}
