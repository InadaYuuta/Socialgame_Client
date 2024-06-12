using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceWeaponDataManager : WeaponBase
{
    int weapon_id;
    public int WeaponId { get { return weapon_id; } }

    int weapon_level;
    public int weaponLevel { get { return weapon_level; } }

    [SerializeField] TextMeshProUGUI detailNameText, reinforceNameText, evolutionNameText, detailLimitBreakText, reinforceLimitBreakText, evolutionLimitBreakText;
    [SerializeField] GameObject detailWaponBack, reinforceWeaponBack, evolutionWeaponBack;
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
    public void SetDetailData(int id)
    {
        WeaponModel weaponData = Weapons.GetWeaponData(id);
        string name = WeaponMaster.GetWeaponMasterData(id).weapon_name;
        // ���O
        detailNameText.text = name;
        reinforceNameText.text = name;
        evolutionNameText.text = name;

        // ���E�˔j���
        detailLimitBreakText.text = string.Format("��{0}", weaponData.limit_break);
        reinforceLimitBreakText.text = string.Format("��{0}", weaponData.limit_break);
        evolutionLimitBreakText.text = string.Format("��{0}", weaponData.limit_break);

        // �摜
        WeaponSetting(detailWaponBack, id);     // �o�b�O��ʂ̕���ڍ׉�ʂ̕���摜�ύX
        WeaponSetting(reinforceWeaponBack, id); // ������ʂ̕���ڍ׉�ʂ̕���摜�ύX
        WeaponSetting(evolutionWeaponBack, id); // ������ʂ̕���ڍ׉�ʂ̕���摜�ύX
        ChangeOnlyWeaponImage(convexWeaponBack, id); // �ʃA�C�e���̉摜�ύX
    }

}
