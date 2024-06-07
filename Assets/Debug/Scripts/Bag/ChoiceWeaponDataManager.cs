using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceWeaponDataManager : WeaponBase
{
    int weapon_id;
    public int WeaponId { get { return weapon_id; } }

    int weapon_level;
    public int weaponLevel { get { return weapon_level; } }

    [SerializeField] TextMeshProUGUI detailNameText, reinforceNameText, limitbreakText;
    [SerializeField] GameObject detailWaponBack, reinforceWeaponBack;
    [SerializeField] Image convexWeaponBack;
    [SerializeField] GameObject detailBack;
    public GameObject DetailBack { get { return detailBack; } }

    private void Awake() => detailBack.SetActive(false);

    // ���킪�I�����ꂽ���A������i���̂��߂ɕ���̊�{�f�[�^��ۑ�����
    public void SetChoiceWeaponData(int id, int level)
    {
        weapon_id = id;
        weapon_level = level;
    }

    // �ڍ׉�ʂ̐ݒ�A�����{�^���������ꂽ���ɉ摜�����ς��(����)
    public void SetDetailData(string weapon_name)
    {
        detailNameText.text = weapon_name;
        reinforceNameText.text = weapon_name;
        WeaponModel weaponData = Weapons.GetWeaponData(weapon_id);
        limitbreakText.text = string.Format("��{0}", weaponData.limit_break);
        WeaponSetting(detailWaponBack, weapon_id);     // �o�b�O��ʂ̕���ڍ׉�ʂ̕���摜�ύX
        WeaponSetting(reinforceWeaponBack, weapon_id); // ������ʂ̕���ڍ׉�ʂ̕���摜�ύX
        ChangeOnlyWeaponImage(convexWeaponBack, weapon_id); // �ʃA�C�e���̉摜�ύX
    }

}
