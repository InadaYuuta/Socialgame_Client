using UnityEngine;
using UnityEngine.UI;

public class WeaponBase : MonoBehaviour
{
    protected void WeaponSetting(GameObject weapon, int weaponId)
    {
        Image weaponImage = weapon.transform.GetChild(0).GetComponent<Image>();
        weaponImage.sprite = Resources.Load<Sprite>(string.Format("WeaponImage/w{0}", weaponId.ToString())); // Resources�t�H���_�̒��̓���̉摜���擾���ē����
        Outline outline = weapon.GetComponent<Outline>();
        int rarity = GetNthDigitNum(weaponId, 7);
        switch (rarity)
        {
            case 1: // Comon
                outline.effectColor = Color.blue;
                break;
            case 2: // Rare 
                outline.effectColor = Color.red;
                break;
            case 3: // SRare
                outline.effectColor = Color.yellow;
                break;
            default:
                break;
        }
    }

    // �w�肵�������̎w��̌��̐��l��Ԃ� �Q�l�T�C�ghttps://santerabyte.com/c-sharp-get-nth-digit-num/
    protected int GetNthDigitNum(int num, int digit)
    {
        int currentDigitNum = 1;
        num = System.Math.Abs(num);
        while (num > 0)
        {
            if (currentDigitNum == digit) return num % 10;
            num /= 10;
            currentDigitNum++;
        }
        return 0;
    }
}
