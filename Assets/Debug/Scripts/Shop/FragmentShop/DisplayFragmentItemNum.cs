using TMPro;
using UnityEngine;

public class DisplayFragmentItemNum : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fragmentText;
    int fragmentItemNum = 0;

    void Awake()
    {
        fragmentItemNum = Items.GetItemData(30001).item_num;
        fragmentText.text = fragmentItemNum.ToString();
    }

    void Update()
    {
        fragmentItemNum = Items.GetItemData(30001).item_num;
        fragmentText.text = fragmentItemNum.ToString();
    }
}
