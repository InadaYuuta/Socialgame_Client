using UnityEngine;

public class ReinforceManager : MonoBehaviour
{
    [SerializeField] GameObject reinforcePanel;
    LimitBreakManager limitBreakManager;
    LevelUpManager levelUpManager;

    ChoiceWeaponDataManager weaponData;

    private void Awake()
    {
        limitBreakManager = GetComponent<LimitBreakManager>();
        levelUpManager = GetComponent<LevelUpManager>();
        weaponData = FindObjectOfType<ChoiceWeaponDataManager>();
    }

    // パネル表示非表示 -----
    public void OnClickOpenPanelButton()
    {
        if (reinforcePanel == null) { return; }
        reinforcePanel.SetActive(true);
        levelUpManager.SetLevelUpWeaponParameter(weaponData.WeaponId);
        limitBreakManager.SetLimitBreakWeaponParameter(weaponData.WeaponId);
    }

    public void OnClickClosePanelButton()
    {
        if (reinforcePanel == null) { return; }
        reinforcePanel.SetActive(false);
    }
}
