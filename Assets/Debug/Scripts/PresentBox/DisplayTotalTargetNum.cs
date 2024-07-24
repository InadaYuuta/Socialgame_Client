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

    // 表示するプレゼントの数をテキストに反映、99個以上のプレゼントがある場合99+と表記
    protected void SetDisplayText()
    {
        if (totalTargetNum >= maxDisplayPresents)
        {
            textStr = maxDisplayTargetsStr;
        }
        else { textStr = totalTargetNum.ToString(); }
        targetsNumText.text = textStr;
    }

    // テキストを表示
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
