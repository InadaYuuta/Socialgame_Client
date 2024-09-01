using UnityEngine;

public class UpdateConditionOfAchievement : MonoBehaviour
{
    [SerializeField] int pullGachaCount = 0;  // �K�`������������
    [SerializeField] int loginCount = 0;      // ���O�C������
    [SerializeField] int getWeaponCount = 0;  // ���v����擾��
    [SerializeField] int totalLevelCount = 0; // ���v���탌�x��
    [SerializeField] int evolutionCount = 0;  // ���v����i����

    public int PullGachaCount { get { return pullGachaCount; } }
    public int LoginCount { get { return loginCount; } }
    public int GetWeaponCount { get { return getWeaponCount; } }
    public int TotalLevelCount { get { return totalLevelCount; } }
    public int EvolutionCount { get { return evolutionCount; } }

    void Start() => GetAchievementCount();

    void Update() => GetAchievementCount();

    // �������Ă��镐��̍��v���x�����擾
    int GetTotalLevel()
    {
        int totalLevel = 0;

        var targets = Weapons.GetWeaponDataAll();

        foreach (var target in targets)
        {
            totalLevel += target.level;
        }

        return totalLevel;
    }

    // �������Ă��镐��̍��v�i����(���ݐi������Ă������)���擾
    int GetTotalEvolution()
    {
        int totalEvolution = 0;

        var targets = Weapons.GetWeaponDataAll();

        foreach (var target in targets)
        {
            var evolution = target.evolution;
            if (evolution > 0)
            {
                totalEvolution++;
            }
        }

        return totalEvolution;
    }

    // ���݂̒B���󋵂��擾
    void GetAchievementCount()
    {
        pullGachaCount = GachaLogs.GetGacaLogDataAll().Length;
        loginCount = Users.Get().login_days;
        getWeaponCount = pullGachaCount;      // TODO: ����̕���̓�����@���K�`���݂̂Ȃ̂ő���A����N�G�X�g��v���[���g�Ŏ󂯎��悤�ɂ���Ȃ珈����ύX
        totalLevelCount = GetTotalLevel();
        evolutionCount = GetTotalEvolution();
    }
}
