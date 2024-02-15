using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    // �Z�[�u�t�@�C���̖��O
    const string FileName = "/savedata.dat";
    // �Z�[�u�f�[�^�̃f�t�H���g�l
    static readonly int DefaultVersion = 0;
    static readonly string[] DefaultNewWeapons = { "", "", "", "", "", "", "", "", "", "" };
    static readonly int DefaultFragmentNum = 0;
    // ->����f�[�^�̒ǉ�������Ȃ炱��

    FileStream file;
    BinaryFormatter bf;
    string filePath;

    void Awake()
    {
        // �V���O���g���ɂ���
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

    // �t�@�C���X�V���ʏ���
    void InitFileSave()
    {
        bf = new();
        if (filePath == null)
        {
            filePath = Application.persistentDataPath + FileName;
        }
        file = File.Create(filePath);
    }

    // �t�@�C�����[�h���ʏ���
    void InitFileLoad()
    {
        bf = new();
        file = File.Open(filePath, FileMode.Open);
    }

    // �t�@�C���N���[�Y����
    void CloseFile()
    {
        file.Close();
        file = null;
    }

    // �t�@�C�����݃`�F�b�N
    public bool SaveDataCheck()
    {
        // �t�@�C���������true
        if (File.Exists(filePath)) { return true; }
        return false;
    }

    // �V�K�f�[�^����
    public void CreateSaveData()
    {
        try
        {
            InitFileSave();

            // �Z�[�u�f�[�^�𐶐�
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
            // fileStream���g�p������K���Ō��close����
            if (file != null) { file.Close(); }
        }
    }

    // TODO: �ꊇ�Z�[�uBulkSave����������

    // �o�[�W�����Z�[�u
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

    // �o�[�W�������[�h
    public int GetMasterDataVersion()
    {
        int version = DefaultVersion;
        try
        {
            InitFileLoad();

            // �Z�[�u�f�[�^�ǂݍ���
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

            // �Z�[�u�f�[�^�ǂݍ���
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

            // �Z�[�u�f�[�^�ǂݍ���
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

            // �Z�[�u�f�[�^�ǂݍ���
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
