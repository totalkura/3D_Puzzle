public interface IInteractable
{
    void Interact(); //��ȣ�ۿ�� �߻��� �ൿ ������� SetBool(True)
    string GetPrompt(); // UI�� ǥ���� �ؽ�Ʈ (��: "F - �� ����") ���� ��� return "�� ����" �Ǵ� if����� "���� ����ִ�."/"���� �����ִ�."
}