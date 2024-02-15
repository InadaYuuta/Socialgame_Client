using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    // セーブファイルの名前
    const string FileName = "/savedata.dat";
    // セーブデータのデフォルト値
    static readonly int DefaultVersion = 0;
    static readonly string[] DefaultNewWeapons = { "", "", "", "", "", "", "", "", "", "" };
    static readonly int DefaultFragmentNum = 0;
    // ->今後データの追加をするならここ

    FileStream file;
    BinaryFormatter bf;
    string filePath;

    void Awake()
    {
        // シングルトンにする
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    // ファイル更新共通準備
    void InitFileSave()
    {
        bf = new();
        if (filePath == null)
        {
            filePath = Application.persistentDataPath + FileName;
        }
        file = File.Create(filePath);
    }

    // ファイルロード共通準備
    void InitFileLoad()
    {
        bf = new();
        file = File.Open(filePath, FileMode.Open);
    }

    // ファイルクローズ処理
    void CloseFile()
    {
        file.Close();
        file = null;
    }

    // ファイル存在チェック
    public bool SaveDataCheck()
    {
        // ファイルがあればtrue
        if (File.Exists(filePath)) { return true; }
        return false;
    }

    // 新規データ生成
    public void CreateSaveData()
    {
        try
        {
            InitFileSave();

            // セーブデータを生成
            SaveData data = new();
            data.version = DefaultVersion;
            data.newWeapons = DefaultNewWeapons;
            data.fragmentNum = DefaultFragmentNum;
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // fileStreamを使用したら必ず最後にcloseする
            if (file != null) { file.Close(); }
        }
    }

    // TODO: 一括セーブBulkSaveを実装する

    // バージョンセーブ
    public void SetMasterDataVersion(int version)
    {
        try
        {
            InitFileSave();

            SaveData data = new();
            data.version = version;
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { file.Close(); }
        }
    }

    public void SetNewWeapons(string[] newWeapons)
    {
        try
        {
            InitFileSave();

            SaveData data = new();
            data.newWeapons = newWeapons;
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { file.Close(); }
        }
    }

    public void SetFragmentItem(int fragmentItem)
    {
        try
        {
            InitFileSave();

            SaveData data = new();
            data.fragmentNum = fragmentItem;
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { file.Close(); }
        }
    }

    public void SetResultWeapons(string[] gacha_result)
    {
        try
        {
            InitFileSave();

            SaveData data = new();
            data.gacha_result = gacha_result;
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { file.Close(); }
        }
    }

    // バージョンロード
    public int GetMasterDataVersion()
    {
        int version = DefaultVersion;
        try
        {
            InitFileLoad();

            // セーブデータ読み込み
            SaveData data = bf.Deserialize(file) as SaveData;
            version = data.version;
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { file.Close(); }
        }
        return version;
    }

    public string[] GetNewWeapons()
    {
        string[] newWeapons = DefaultNewWeapons;
        try
        {
            InitFileLoad();

            // セーブデータ読み込み
            SaveData data = bf.Deserialize(file) as SaveData;
            newWeapons = data.newWeapons;
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { file.Close(); }
        }
        return newWeapons;
    }

    public int GetFragmentItem()
    {
        int fragmentItem = DefaultFragmentNum;
        try
        {
            InitFileLoad();

            // セーブデータ読み込み
            SaveData data = bf.Deserialize(file) as SaveData;
            fragmentItem = data.fragmentNum;
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { file.Close(); }
        }
        return fragmentItem;
    }

    public string[] GetWeaponsResult(int count)
    {
        string[] weaponModel = new string[count];

        try
        {
            InitFileLoad();

            // セーブデータ読み込み
            SaveData data = bf.Deserialize(file) as SaveData;
            weaponModel = data.gacha_result;
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { file.Close(); }
        }
        return weaponModel;
    }
}
