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
        for (int i = 0; i < myScrollRect.content.childCount; i++) //childCount = 9
        {
            // scrollview�� Width�� ��������
            float scrollViewWidth = myScrollRect.GetComponent<RectTransform>().rect.width;





            //==============Stage�� ��ġ���� �����ͼ� �Ÿ� ����ϱ�=================
            Vector2 posiotion = myScrollRect.content.GetChild(i).position; // Stages�� ���� W Data : -1550, H Data : 110
            Vector2 viewCenter = myScrollRect.viewport.position; // viewport�� ���� ���� : 17

            // ==============�Ÿ� ��� �� ������ ����=================
            Vector2 SectionInDistance = myScrollRect.transform.


            float distance = Vector2.Distance(posiotion, viewCenter); // �Ÿ� ���
            float scale = Mathf.Clamp(1f, 0.1f, distance * distanceFactor); // �Ÿ� ��� ������ ���, �ּҰ� 0.1f�� ����
            myScrollRect.content.GetChild(i).transform.localScale = Vector3.one * scale; // ������ ����
        }
    }
}
