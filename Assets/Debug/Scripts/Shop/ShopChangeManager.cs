using UnityEngine;
using UnityEngine.UI;

public class ShopChangeManager : MonoBehaviour
{
    enum ShopState { Payment, Stamina, Fragment }
    ShopState currentState = ShopState.Payment;

    [SerializeField] GameObject paymentShopPanel;
    [SerializeField] GameObject staminaShopPanel;
    [SerializeField] GameObject fragmentShopPanel;

    [SerializeField] Image paymentButton, staminaButton, fragmentButton;

    Color choiceColor = new Color(255 / 255f, 165 / 255f, 0 / 255f);     // �I�����W
    Color unChoiceColor = new Color(135 / 255f, 206 / 255f, 250 / 255f); // ���F

    void Start()
    {
        ActivePanel(false, false, false);
        ChangeImageColor(unChoiceColor, unChoiceColor, unChoiceColor);
    }

    void Update()
    {
        DisplayPanel();
    }

    // �p�l����SetActive��ς���
    void ActivePanel(bool paymentFlag, bool staminaFlag, bool fragmentFlag)
    {
        if (paymentShopPanel == null || staminaShopPanel == null || fragmentShopPanel == null) { return; }
        paymentShopPanel.SetActive(paymentFlag);
        staminaShopPanel.SetActive(staminaFlag);
        fragmentShopPanel.SetActive(fragmentFlag);
    }

    // �p�l���C���[�W�̐F��ς���
    void ChangeImageColor(Color paymentColor, Color staminaColor, Color fragmentColor)
    {
        paymentButton.color = paymentColor;
        staminaButton.color = staminaColor;
        fragmentButton.color = fragmentColor;
    }

    // �\������p�l����ς���
    void DisplayPanel()
    {
        switch (currentState)
        {
            case ShopState.Payment:
                ActivePanel(true, false, false);
                ChangeImageColor(choiceColor, unChoiceColor, unChoiceColor);
                break;
            case ShopState.Stamina:
                ActivePanel(false, true, false);
                ChangeImageColor(unChoiceColor, choiceColor, unChoiceColor);
                break;
            case ShopState.Fragment:
                ActivePanel(false, false, true);
                ChangeImageColor(unChoiceColor, unChoiceColor, choiceColor);
                break;
        }
    }

    // �ʉ݃V���b�v�̃{�^�����I�����ꂽ��
    public void ChoicePaymentShop() => currentState = ShopState.Payment;
    // �X�^�~�i�V���b�v(�񕜉��)���I�����ꂽ��
    public void ChoiceStaminaShop() => currentState = ShopState.Stamina;
    // ������V���b�v���I�����ꂽ��
    public void ChoiceFragmentShop() => currentState = ShopState.Fragment;
}
