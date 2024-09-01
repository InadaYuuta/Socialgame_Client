using TMPro;
using UnityEngine;

public class DisplayTotalTargetNum : MonoBehaviour
{
    [SerializeField] GameObject circleImage;
    [SerializeField] TextMeshProUGUI targetsNumText;

    string textStr = "";
    string maxDisplayTargetsStr = "99+";

    protected int totalTargetNum = 0;
    int maxDisplayPresents = 99;

    // �\������v���[���g�̐����e�L�X�g�ɔ��f�A99�ȏ�̃v���[���g������ꍇ99+�ƕ\�L
    protected void SetDisplayText()
    {
        if (totalTargetNum >= maxDisplayPresents)
        {
            textStr = maxDisplayTargetsStr;
        }
        else { textStr = totalTargetNum.ToString(); }
        targetsNumText.text = textStr;
    }

    // �e�L�X�g��\��
    protected void DisplayText()
    {
        if (totalTargetNum > 0)
        {
            SetDisplayText();
            circleImage.SetActive(true);
        }
        else { circleImage.SetActive(false); }
    }
}
