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

    // 表示するプレゼントの数をテキストに反映、99個以上のプレゼントがある場合99+と表記
    void SetDisplayText()
    {
        if (totalPresentsNum >= maxDisplayPresents)
        {
            textStr = maxDisplayPresentsStr;
        }
        else { textStr = totalPresentsNum.ToString(); }
        presentsNumText.text = textStr;
    }

    // テキストを表示
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
