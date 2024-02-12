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

    // �I�v�V�������I�����ꂽ��
    public void ChoiceOption()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
        currentMode = HomeMode.Option;
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

    // �o�b�O���I�����ꂽ��
    public void ChoiceBag()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
        currentMode = HomeMode.Bag;
    }
}
