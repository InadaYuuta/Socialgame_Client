using UnityEngine;
using UnityEngine.UI;

public class ChangeImageColor : MonoBehaviour
{
    [SerializeField] Color reinforceColor, evolutionColor, unSelectColor;

    public enum ChangeMode { UNSELECT = 0, REINFORCE, EVOLUTION }

    // 指定のイメージの色を変更する、基本的には選択できるかどうかの色の違いを変える
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
