using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wall : MonoBehaviour
{
    [SerializeField] private EdgeCollider2D _edgeCollider;
    [SerializeField] private LineRenderer _lineRender;

    private void Awake()
    {
        var LeftTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        var LeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var RightBottom = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        var RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        LeftTop.z = LeftBottom.z = RightBottom.z = RightTop.z = 0;
        var colliderPoints = new Vector2[5];
        colliderPoints[0] = new Vector2(LeftTop.x,LeftTop.y);
        colliderPoints[1] = new Vector2(LeftBottom.x,LeftBottom.y);
        colliderPoints[2] = new Vector2(RightBottom.x,RightBottom.y);
        colliderPoints[3] = new Vector2(RightTop.x,RightTop.y);
        colliderPoints[4] = new Vector2(LeftTop.x, LeftTop.y);
        _edgeCollider.points = colliderPoints;
        var linePositions = new Vector3[] { LeftTop, LeftBottom, RightBottom, RightTop, LeftTop };
        _lineRender.positionCount = linePositions.Length;
        _lineRender.SetPositions(linePositions);
    }
}