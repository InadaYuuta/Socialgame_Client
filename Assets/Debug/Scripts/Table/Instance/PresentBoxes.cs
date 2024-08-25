using System;
using System.Collections.Generic;

[Serializable]
public class PresentBoxModel
{
    public int present_id;             // �v���[���gID
    public int whole_present_id;       // �S�̃v���[���gID
    public int reward_category;        // ��V�̃J�e�S���[
    public string present_box_reward;  // ��V�̓��e
    public string receive_reason;      // �󂯎�������R
    public int receipt;                // ���
    public string receipt_date;        // ����
    public string display;             // �\������
}

public class PresentBoxes : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists present_boxes(user_id varchar,present_id bigint,whole_present_id bigint,reward_category tinyint,present_box_reward text,receive_reason text,receipt tinyint,receipt_date varchar,display varchar,primary key(user_id,present_id))";
        RunQuery(createQuery);
        //// �C���f�b�N�X�쐬 TODO:����T�[�o�[���Ń����[�h�J�e�S���[���C���f�b�N�X�ɂ���ꍇ�̓R�����g�A�E�g����
        //createQuery = "CREATE INDEX IF NOT EXISTS reward_category_index ON present_boxes(reward_category);";
        //RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(PresentBoxModel[] present_box_model, string user_id)
    {
        foreach (PresentBoxModel present_box in present_box_model)
        {
            // �G�X�P�[�v�������s��
            string escapedPresentBoxReward = EscapeString(present_box.present_box_reward);
            string escapedReceiveReason = EscapeString(present_box.receive_reason);
            string escapedDisplay = EscapeString(present_box.display);
            string escapedReceipted = EscapeString(present_box.receipt_date);

            setQuery = "insert or replace into present_boxes(user_id, present_id, whole_present_id, reward_category, present_box_reward, receive_reason, receipt, receipt_date, display) values(" +
                       "\"" + user_id + "\", " +
                       present_box.present_id + ", " +
                       present_box.whole_present_id + ", " +
                       present_box.reward_category + ", " +
                       "\"" + escapedPresentBoxReward + "\", " +
                       "\"" + escapedReceiveReason + "\", " +
                       present_box.receipt + ", " +
                       "\"" + escapedReceipted + "\", " +
                       "\"" + escapedDisplay + "\")";
            RunQuery(setQuery);
        }
    }

    // ���R�[�h�̍X�V����
    public static void UpdateDate(PresentBoxModel[] present_box_model, string user_id)
    {
        foreach (PresentBoxModel present_box in present_box_model)
        {
            // �G�X�P�[�v�������s��
            string escapedDisplay = EscapeString(present_box.display);
            string escapedReceipted = EscapeString(present_box.receipt_date);
            setQuery = string.Format("UPDATE present_boxes SET whole_present_id = {1}, receipt = {2}, receipt_date = \"{3}\", display = \"{4}\" WHERE user_id = \"{0}\" AND present_id = {5} AND receipt = 0",user_id,present_box.whole_present_id,present_box.receipt,escapedReceipted,escapedDisplay,present_box.present_id);
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
            PresentBoxModel.whole_present_id = int.Parse(dr["whole_present_id"].ToString());
            PresentBoxModel.reward_category = int.Parse(dr["reward_category"].ToString());
            PresentBoxModel.present_box_reward = dr["present_box_reward"].ToString();
            PresentBoxModel.receive_reason = dr["receive_reason"].ToString();
            PresentBoxModel.receipt = int.Parse(dr["receipt"].ToString());
            PresentBoxModel.receipt_date = dr["receipt_date"].ToString();
            PresentBoxModel.display = dr["display"].ToString();
            PresentBoxList.Add(PresentBoxModel);
        }
        return PresentBoxList.ToArray();
    }

    // �w�肳�ꂽ�v���[���g�f�[�^���擾
    public static PresentBoxModel GetPresentBoxData(int present_id)
    {
        PresentBoxModel PresentBoxModel = new();
        getQuery = string.Format("select * from present_boxes where present_id={0}", present_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            PresentBoxModel.present_id = int.Parse(dr["present_id"].ToString());
            PresentBoxModel.whole_present_id = int.Parse(dr["whole_present_id"].ToString());
            PresentBoxModel.reward_category = int.Parse(dr["reward_category"].ToString());
            PresentBoxModel.present_box_reward = dr["present_box_reward"].ToString();
            PresentBoxModel.receive_reason = dr["receive_reason"].ToString();
            PresentBoxModel.receipt = int.Parse(dr["receipt"].ToString());
            PresentBoxModel.receipt_date = dr["receipt_date"].ToString();
            PresentBoxModel.display = dr["display"].ToString();
        }
        return PresentBoxModel;
    }
}