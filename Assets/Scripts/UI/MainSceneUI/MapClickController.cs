using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapClickController : MonoBehaviour
{
    [SerializeField]
    private ScrollRect myScrollRect; // ��ũ�ѹ� ������Ʈ
    public float distanceFactor = 0.1f; // �Ÿ� ���� ���

    public void Start()
    {
        MapValueChanged(Vector2.zero);
    }

    public void MapValueChanged(Vector2 value)
    {
        for (int i = 0; i < myScrollRect.content.childCount; i++)
        {

        }
    }
}
