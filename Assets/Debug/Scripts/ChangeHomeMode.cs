using UnityEngine;

public class ChangeHomeMode : MonoBehaviour
{
    enum HomeMode { Option, Home, Bag, PictureBook, Shop }
    HomeMode currentMode = HomeMode.Home;

    [SerializeField] GameObject shopCanvas, bagCanvas;

    HomeManager homeManager;

    void Awake()
    {
        currentMode = HomeMode.Home;
        shopCanvas.SetActive(false);
        bagCanvas.SetActive(false);
        homeManager = FindObjectOfType<HomeManager>();
    }

    // �I�v�V�������I�����ꂽ��
    public void ChoiceOption()
    {
        homeManager.GetHomeData();
        currentMode = HomeMode.Option;
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
    }

    // �}�ӂ��I�����ꂽ��
    public void ChoicePictureBook()
    {
        homeManager.GetHomeData();
        currentMode = HomeMode.PictureBook;
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
    }

    // �z�[�����I�����ꂽ��
    public void ChoiceHome()
    {
        homeManager.GetHomeData();
        currentMode = HomeMode.Home;
        shopCanvas.SetActive(false);
        bagCanvas.SetActive(false);
    }

    // �V���b�v���I�����ꂽ��
    public void ChoiceShop()
    {
        homeManager.GetHomeData();
        currentMode = HomeMode.Shop;
        shopCanvas.SetActive(true);
        bagCanvas.SetActive(false);
    }

    // �o�b�O���I�����ꂽ��
    public void ChoiceBag()
    {
        homeManager.GetHomeData();
        currentMode = HomeMode.Bag;
        bagCanvas.SetActive(true);
        shopCanvas.SetActive(false);
    }

    public void SeasonPass()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("��������\��!\n�������!"));
    }
}
