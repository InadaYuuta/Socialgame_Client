using TMPro;
using UnityEngine;

public class DisplayPresentNum : MonoBehaviour
{
    [SerializeField] GameObject circleImage;
    [SerializeField] TextMeshProUGUI presentsNumText;

    string textStr = "";
    string maxDisplayPresentsStr = "99+";

    int totalPresentsNum = 0;
    int maxDisplayPresents = 99;

    PresentBoxManager presentBoxManager;

    private void Awake()
    {
        presentBoxManager = GetComponent<PresentBoxManager>();
    }

    void Start()
    {
        totalPresentsNum = presentBoxManager.TotalPresentsNum;
    }

    void Update()
    {
        totalPresentsNum = presentBoxManager.TotalPresentsNum;
        DisplayText();
    }

    // �\������v���[���g�̐����e�L�X�g�ɔ��f�A99�ȏ�̃v���[���g������ꍇ99+�ƕ\�L
    void SetDisplayText()
    {
        if (totalPresentsNum >= maxDisplayPresents)
        {
            textStr = maxDisplayPresentsStr;
        }
        else { textStr = totalPresentsNum.ToString(); }
        presentsNumText.text = textStr;
    }

    // �e�L�X�g��\��
    void DisplayText()
    {
        if (totalPresentsNum > 0)
        {
            SetDisplayText();
            circleImage.SetActive(true);
        }
        else { circleImage.SetActive(false); }
    }
}
