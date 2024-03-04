using Cinemachine;
using UnityEngine;

public class SwitchBounds : MonoBehaviour
{
    private void Start()
    {
        SwitchConfinerShape();
    }

    //这段代码目的是，人物移到Bounds框的边界时，摄像机不会跟着再继续移动到外面去，适当时候会停止
    //注意将Bounds的pylogon collider 2d的Is Trigger打开，否则人物会被挤出去
    private void SwitchConfinerShape()
    {
        PolygonCollider2D confinerShape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = confinerShape;
        //Call this if the bounding shape's points change at runtime
        confiner.InvalidatePathCache();
    }
}
