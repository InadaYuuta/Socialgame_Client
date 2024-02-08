using UnityEngine;

public class ChangeHomeMode : MonoBehaviour
{
    enum HomeMode { Option, Home, Bag, PictureBook, Shop }
    HomeMode currentMode = HomeMode.Home;

    [SerializeField] GameObject shopCanvas;

    void Awake()
    {
        currentMode = HomeMode.Home;
        shopCanvas.SetActive(false);
    }

    // �z�[�����I�����ꂽ��
    public void ChoiceHome()
    {
        shopCanvas.SetActive(false);
        currentMode = HomeMode.Home;
    }

    // �V���b�v���I�����ꂽ��
    public void ChoiceShop()
    {
        shopCanvas.SetActive(true);
        currentMode = HomeMode.Shop;
    }
}
