using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MasterUpdateManager : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {

    }

    public void PushMasterUodateButton()
    {
        List<IMultipartFormSection> loginForm = new List<IMultipartFormSection>(); // WWWFormÇÃêVÇµÇ¢Ç‚ÇËï˚
        string maserVersion = SaveManager.Instance.GetMasterDataVersion().ToString();
        loginForm.Add(new MultipartFormDataSection("mv", maserVersion));

        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_CHECK_URL, null, null));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_GET_URL, null, null));

        Dictionary<int, ItemCategoryModel> models = ItemCategories.GetItemCategory();
        foreach (var element in models)
        {
            Debug.Log(element);
        }
    }
}
