using UnityEngine;
using UnityEngine.UI;

public class Glass : MonoBehaviour
{
    public Sprite fullSprite;
    public bool isFilled = false;

    public void FillWithWater()
    {
        if (isFilled) return;
        isFilled = true;

        // ������ ������ �������
        GetComponent<Image>().sprite = fullSprite;
        Debug.Log("������ ���������� �����");
    }
}
