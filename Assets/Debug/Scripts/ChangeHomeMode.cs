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

    // ホームが選択された時
    public void ChoiceHome()
    {
        shopCanvas.SetActive(false);
        currentMode = HomeMode.Home;
    }

    // ショップが選択された時
    public void ChoiceShop()
    {
        shopCanvas.SetActive(true);
        currentMode = HomeMode.Shop;
    }
}
