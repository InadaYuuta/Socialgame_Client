using System;
using System.Collections.Generic;

[Serializable]
public class MissionMasterModel
{
    public int mission_id;                // �~�b�V����ID
    public int next_mission_id;           // ���̃~�b�V����ID
    public string mission_name;           // �~�b�V������
    public string mission_content;        // �~�b�V�����̓��e
    public int mission_category;          // �~�b�V�����̃J�e�S���[
    public int reward_category;           // ��V�̃J�e�S���[
    public string mission_reward;         // �~�b�V�����̕�V
    public string achievement_condition;  // �B������(�Ɛ��l)
    public string period_end;             // �I������
}

public class MissionMaster : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists mission_masters(mission_id bigint,next_mission_id bigint,mission_name text,mission_content text,mission_category tinyint,reward_category tinyint,mission_reward text,achievement_condition text,period_end varchar,primary key(mission_id))";
        RunQuery(createQuery);
        // �C���f�b�N�X�쐬
        createQuery = "CREATE INDEX IF NOT EXISTS mission_category_index ON mission_masters(mission_category);";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(MissionMasterModel[] mission_master_model)
    {
        foreach (MissionMasterModel mission_master in mission_master_model)
        {
            setQuery = "insert or replace into mission_masters(mission_id,next_mission_id,mission_name,mission_content,mission_category ,reward_category ,mission_reward ,achievement_condition ,period_end ) values(\"" + mission_master.mission_id + "," + mission_master.next_mission_id + "\"," + mission_master.mission_name + "\"," + mission_master.mission_content + "\"," + mission_master.mission_category + "," + mission_master.reward_category + "\"," + mission_master.mission_reward + "\"," + mission_master.achievement_condition + "\"," + mission_master.period_end + ")";
            RunQuery(setQuery);
        }
    }

    // �S�Ẵ~�b�V�����f�[�^���擾
    public static MissionMasterModel[] GetMissionMasterDataAll()
    {
        List<MissionMasterModel> MissionMasterList = new();
        getQuery = "select * from mission_masters";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            MissionMasterModel MissionMasterModel = new();
            MissionMasterModel.mission_id = int.Parse(dr["mission_id"].ToString());
            MissionMasterModel.next_mission_id = int.Parse(dr["next_mission_id"].ToString());
            MissionMasterModel.mission_name = dr["mission_name"].ToString();
            MissionMasterModel.mission_content = dr["mission_content"].ToString();
            MissionMasterModel.mission_category = int.Parse(dr["mission_category"].ToString());
            MissionMasterModel.reward_category = int.Parse(dr["reward_category"].ToString());
            MissionMasterModel.mission_reward = dr["mission_reward"].ToString();
            MissionMasterModel.achievement_condition = dr["achievement_condition"].ToString();
            MissionMasterModel.period_end = dr["period_end"].ToString(); // TODO: ����~�b�V�����֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            MissionMasterList.Add(MissionMasterModel);
        }
        return MissionMasterList.ToArray();
    }

    // �w�肳�ꂽ�~�b�V�����f�[�^���擾
    public static MissionMasterModel GetMissionMasterData(int mission_id)
    {
            MissionMasterModel MissionMasterModel = new();
        getQuery = string.Format("select * from mission_masters where mission_id={0}", mission_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            MissionMasterModel.mission_id = int.Parse(dr["mission_id"].ToString());
            MissionMasterModel.next_mission_id = int.Parse(dr["next_mission_id"].ToString());
            MissionMasterModel.mission_name = dr["mission_name"].ToString();
            MissionMasterModel.mission_content = dr["mission_content"].ToString();
            MissionMasterModel.mission_category = int.Parse(dr["mission_category"].ToString());
            MissionMasterModel.reward_category = int.Parse(dr["reward_category"].ToString());
            MissionMasterModel.mission_reward = dr["mission_reward"].ToString();
            MissionMasterModel.achievement_condition = dr["achievement_condition"].ToString();
            MissionMasterModel.period_end = dr["period_end"].ToString(); // TODO: ����~�b�V�����֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
        }
        return MissionMasterModel;
    }
}