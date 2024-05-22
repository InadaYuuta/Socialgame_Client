using System;
using System.Collections.Generic;

[Serializable]
public class MissionsModel
{
    public int mission_id;       // �~�b�V����ID
    public int achieved;         // �B��
    public int receipt;          // ���
    public int progress;         // �i��
    public string term;          // ����
    public string validity_term; // �B����
}

public class Missions : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists missions(user_id varchar,mission_id bigint,achieved tinyint,receipt tinyint,progress smallint,term varchar,validity_term varchar,primary key(user_id,mission_id))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(MissionsModel[] missions_model, string user_id)
    {
        foreach (MissionsModel mission in missions_model)
        {
            setQuery = "insert or replace into missions(user_id,mission_id ,achieved ,receipt ,progress ,term ,validity_term) values(\"" + user_id + "\"," + mission.mission_id + "," + mission.achieved + "," + mission.receipt + "," + mission.progress + "\"," + mission.term + "\"," + mission.validity_term + ")";
            RunQuery(setQuery);
        }
    }

    // �S�Ẵv���[���g�{�b�N�X�f�[�^���擾
    public static MissionsModel[] GetMissionDataAll()
    {
        List<MissionsModel> MissionList = new();
        getQuery = "select * from missions";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            MissionsModel MissionModel = new();
            MissionModel.mission_id = int.Parse(dr["mission_id"].ToString());
            MissionModel.achieved = int.Parse(dr["achieved"].ToString());
            MissionModel.receipt = int.Parse(dr["receipt"].ToString());
            MissionModel.progress = int.Parse(dr["progress"].ToString());
            MissionModel.term = dr["term"].ToString(); // TODO: ����~�b�V�����֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            MissionModel.validity_term = dr["validity_term"].ToString(); // TODO: ����~�b�V�����֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            MissionList.Add(MissionModel);
        }
        return MissionList.ToArray();
    }

    // �w�肳�ꂽ�i���㕐��f�[�^���擾
    public static MissionsModel[] GetPresentBoxData(int mission_id)
    {
        List<MissionsModel> MissionList = new();
        getQuery = string.Format("select * from missions where mission_id={0}", mission_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            MissionsModel MissionModel = new();
            MissionModel.mission_id = int.Parse(dr["mission_id"].ToString());
            MissionModel.achieved = int.Parse(dr["achieved"].ToString());
            MissionModel.receipt = int.Parse(dr["receipt"].ToString());
            MissionModel.progress = int.Parse(dr["progress"].ToString());
            MissionModel.term = dr["term"].ToString(); // TODO: ����~�b�V�����֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            MissionModel.validity_term = dr["validity_term"].ToString(); // TODO: ����~�b�V�����֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            MissionList.Add(MissionModel);
        }
        return MissionList.ToArray();
    }
}