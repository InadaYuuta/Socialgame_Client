using UnityEngine;

// セーブデータクラス
[System.Serializable]
public class SaveData
{
    public int version;
    public int fragmentNum;
    public string[] newWeapons;
    public string[] gacha_result;
}
