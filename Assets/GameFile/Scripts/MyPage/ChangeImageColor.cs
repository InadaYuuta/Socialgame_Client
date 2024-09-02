using UnityEngine;
using UnityEngine.UI;

public class ChangeImageColor : MonoBehaviour
{
    [SerializeField] Color reinforceColor, evolutionColor, unSelectColor;

    public enum ChangeMode { UNSELECT = 0, REINFORCE, EVOLUTION }

    // �w��̃C���[�W�̐F��ύX����A��{�I�ɂ͑I���ł��邩�ǂ����̐F�̈Ⴂ��ς���
    public void ChangeTargetColor(Image target, ChangeMode changeMode)
    {
        switch (changeMode)
        {
            case ChangeMode.UNSELECT:
                target.color = unSelectColor;
                break;
            case ChangeMode.REINFORCE:
                target.color = reinforceColor;
                break;
            case ChangeMode.EVOLUTION:
                target.color = evolutionColor;
                break;
            default:
                break;
        }

    }
}
