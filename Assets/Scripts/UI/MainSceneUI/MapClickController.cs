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
        //MapValueChanged(Vector2.zero);
    }

    public void MapValueChanged(Vector2 value)
    {
        for (int i = 0; i < myScrollRect.content.childCount; i++) //childCount = 9
        {
            Vector2 posiotion = myScrollRect.content.GetChild(i).position; // Stages�� ���� W Data : -1550, H Data : 110
            Vector2 viewCenter = myScrollRect.viewport.position; // viewport�� ���� ���� : 17

            float distance = Vector2.Distance(posiotion, viewCenter); // �Ÿ� ���
            //float scale = 
        }
    }
}
