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

    // オプションが選択された時
    public void ChoiceOption()
    {
        homeManager.GetHomeData();
        currentMode = HomeMode.Option;
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
    }

    // 図鑑が選択された時
    public void ChoicePictureBook()
    {
        homeManager.GetHomeData();
        currentMode = HomeMode.PictureBook;
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
    }

    // ホームが選択された時
    public void ChoiceHome()
    {
        homeManager.GetHomeData();
        currentMode = HomeMode.Home;
        shopCanvas.SetActive(false);
        bagCanvas.SetActive(false);
    }

    // ショップが選択された時
    public void ChoiceShop()
    {
        homeManager.GetHomeData();
        currentMode = HomeMode.Shop;
        shopCanvas.SetActive(true);
        bagCanvas.SetActive(false);
    }

    // バッグが選択された時
    public void ChoiceBag()
    {
        homeManager.GetHomeData();
        currentMode = HomeMode.Bag;
        bagCanvas.SetActive(true);
        shopCanvas.SetActive(false);
    }

    public void SeasonPass()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
    }
}
