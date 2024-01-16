using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StaminaGageManager : UsersBase
{
    int displayArea;
    int currentStamina;
    int maxStamina;
    RectTransform gage;

    void Awake()
    {
        base.Awake();
        gage = GetComponent<RectTransform>();
    }

    void Start()
    {
        maxStamina = usersModel.max_stamina;
        currentStamina = usersModel.last_stamina * 2;
    }

    void Update()
    {
        SetDisplayValue();
    }

    // •\Ž¦‚·‚é”ÍˆÍ‚ðŒˆ‚ß‚é
    void SetDisplayValue()
    {
        maxStamina = usersModel.max_stamina;
        currentStamina = usersModel.last_stamina * 2;
        displayArea = maxStamina - currentStamina / 2;
        gage.sizeDelta = new Vector2(currentStamina, gage.sizeDelta.y);
        gage.anchoredPosition = new Vector2(-displayArea, -20);
    }
}
