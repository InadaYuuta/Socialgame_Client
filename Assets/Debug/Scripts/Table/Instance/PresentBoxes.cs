using System;
using System.Collections.Generic;

[Serializable]
public class PresentBoxModel
{
    public int present_id;             // �v���[���gID
    public int reward_category;        // ��V�̃J�e�S���[
    public string present_box_reward;  // ��V�̓��e
    public string receive_reason;      // �󂯎�������R
    public int receipt;                // ���
    public string display;             // �\������
}

public class PresentBoxes : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists present_boxes(user_id varchar,present_id bigint,reward_category tinyint,present_box_reward text,receive_reason varchar,receipt tinyint,display varchar,primary key(user_id,present_id))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(PresentBoxModel[] present_box_model, string user_id)
    {
        foreach (PresentBoxModel present_box in present_box_model)
        {
            setQuery = "insert or replace into present_boxes(user_id,reward_category,present_box_reward,receive_reason,receipt,display) values(\"" + user_id + "\"," + present_box.present_id + "," + present_box.reward_category + "\"," + present_box.present_box_reward + "\"," + present_box.receive_reason +"," + present_box.receipt + "\"," + present_box.display + ")";
            RunQuery(setQuery);
        }
    }

    // �S�Ẵv���[���g�{�b�N�X�f�[�^���擾
    public static PresentBoxModel[] GetPresentBoxDataAll()
    {
        List<PresentBoxModel> PresentBoxList = new();
        getQuery = "select * from present_boxes";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            PresentBoxModel PresentBoxModel = new();
            PresentBoxModel.present_id = int.Parse(dr["present_id"].ToString());
            PresentBoxModel.reward_category = int.Parse(dr["reward_category"].ToString());
            PresentBoxModel.present_box_reward = dr["present_box_reward"].ToString();
            PresentBoxModel.receive_reason = dr["receive_reason"].ToString();
            PresentBoxModel.receipt = int.Parse(dr["receipt"].ToString());
            PresentBoxModel.display = dr["display"].ToString(); // TODO: ����v���[���g�֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            PresentBoxList.Add(PresentBoxModel);
        }
        return PresentBoxList.ToArray();
    }

    // �w�肳�ꂽ�i���㕐��f�[�^���擾
    public static PresentBoxModel[] GetPresentBoxData(int present_id)
    {
        List<PresentBoxModel> PresentBoxList = new();
        getQuery = string.Format("select * from present_boxes where present_id={0}", present_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            PresentBoxModel PresentBoxModel = new();
            PresentBoxModel.present_id = int.Parse(dr["present_id"].ToString());
            PresentBoxModel.reward_category = int.Parse(dr["reward_category"].ToString());
            PresentBoxModel.present_box_reward = dr["present_box_reward"].ToString();
            PresentBoxModel.receive_reason = dr["receive_reason"].ToString();
            PresentBoxModel.receipt = int.Parse(dr["receipt"].ToString());
            PresentBoxModel.display = dr["display"].ToString(); // TODO: ����v���[���g�֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            PresentBoxList.Add(PresentBoxModel);
        }
        return PresentBoxList.ToArray();
    }
}