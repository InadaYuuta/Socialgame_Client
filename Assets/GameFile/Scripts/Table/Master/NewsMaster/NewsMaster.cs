using System;
using System.Collections.Generic;

[Serializable]
public class NewsMasterModel
{
    public int news_id;          // ���m�点ID
    public int news_category;    // ���m�点�J�e�S���[
    public string news_name;     // ���m�点��
    public string news_content;  // ���m�点�̓��e
    public int display_priority; // �\���D��x
    public string created;       // �쐬��
    public string period_end;    // �I������
}

public class NewsMaster : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists news_masters(news_id bigint,news_category tinyint,news_name text,news_content text,display_priority smallint,created varcher,period_end varcher,primary key(news_id))";
        RunQuery(createQuery);
        // �C���f�b�N�X�쐬
        createQuery = "CREATE INDEX IF NOT EXISTS news_category_index ON news_masters(news_category);";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(NewsMasterModel[] news_model)
    {
        foreach (NewsMasterModel news in news_model)
        {
            // �G�X�P�[�v�������s��
            string escapedNewsName = EscapeString(news.news_name);
            string escapedNewsContent = EscapeString(news.news_content);
            string escapedNewsCreated = EscapeString(news.created);
            string escapedNewsEnd = EscapeString(news.period_end);

            setQuery = "insert or replace into news_masters(news_id, news_category, news_name, news_content, display_priority, created, period_end) " +
                   "values(" + news.news_id + ", " + news.news_category + ", \"" + escapedNewsName + "\", \"" + escapedNewsContent + "\", " +
                   news.display_priority + ", \"" + escapedNewsCreated + "\", \"" + escapedNewsEnd + "\")";
            RunQuery(setQuery);
        }
    }

    // �S�Ẵ~�b�V�����f�[�^���擾
    public static NewsMasterModel[] GetNewsDataAll()
    {
        List<NewsMasterModel> newsMasterList = new();
        getQuery = "select * from news_masters";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            NewsMasterModel newsMasterModel = new();
            newsMasterModel.news_id = int.Parse(dr["news_id"].ToString());
            newsMasterModel.news_category = int.Parse(dr["news_category"].ToString());
            newsMasterModel.news_name = dr["news_name"].ToString();
            newsMasterModel.news_content = dr["news_content"].ToString();
            newsMasterModel.display_priority = int.Parse(dr["display_priority"].ToString());
            newsMasterModel.created = dr["created"].ToString(); // TODO: ���エ�m�点�֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            newsMasterModel.period_end = dr["period_end"].ToString(); // TODO: ���エ�m�点�֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            newsMasterList.Add(newsMasterModel);
        }
        return newsMasterList.ToArray();
    }

    // �w�肳�ꂽ�~�b�V�����f�[�^���擾
    public static NewsMasterModel GetNewsData(int news_id)
    {
        NewsMasterModel newsMasterModel = new();
        getQuery = string.Format("select * from news_masters where news_id={0}", news_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            newsMasterModel.news_id = int.Parse(dr["news_id"].ToString());
            newsMasterModel.news_category = int.Parse(dr["news_category"].ToString());
            newsMasterModel.news_name = dr["news_name"].ToString();
            newsMasterModel.news_content = dr["news_content"].ToString();
            newsMasterModel.display_priority = int.Parse(dr["display_priority"].ToString());
            newsMasterModel.created = dr["created"].ToString(); // TODO: ���エ�m�点�֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            newsMasterModel.period_end = dr["period_end"].ToString(); // TODO: ���エ�m�点�֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
        }
        return newsMasterModel;
    }

    // �w�肳�ꂽ�J�e�S���[�̃~�b�V�����f�[�^���擾
    public static NewsMasterModel[] GetNewsCategory(int category)
    {
        List<NewsMasterModel> newsMasterList = new();
        getQuery = string.Format("select * from news_masters where news_category={0}", category);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            NewsMasterModel newsMasterModel = new();
            newsMasterModel.news_id = int.Parse(dr["news_id"].ToString());
            newsMasterModel.news_category = int.Parse(dr["news_category"].ToString());
            newsMasterModel.news_name = dr["news_name"].ToString();
            newsMasterModel.news_content = dr["news_content"].ToString();
            newsMasterModel.display_priority = int.Parse(dr["display_priority"].ToString());
            newsMasterModel.created = dr["created"].ToString(); // TODO: ���エ�m�点�֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            newsMasterModel.period_end = dr["period_end"].ToString(); // TODO: ���エ�m�点�֘A�����Ƃ��ɓ����Ŏ擾�ł��郁�\�b�h��ǉ�����
            newsMasterList.Add(newsMasterModel);
        }
        return newsMasterList.ToArray();
    }
}