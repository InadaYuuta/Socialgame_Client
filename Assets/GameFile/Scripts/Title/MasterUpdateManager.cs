using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MasterUpdateManager : MonoBehaviour
{
    public void PushMasterUodateButton()
    {
        List<IMultipartFormSection> masterForm = new List<IMultipartFormSection>(); // WWWFormÇÃêVÇµÇ¢Ç‚ÇËï˚
        string maserVersion = SaveManager.Instance.GetMasterDataVersion().ToString();
        masterForm.Add(new MultipartFormDataSection("mv", maserVersion));

        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_CHECK_URL, masterForm, null));
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.MASTER_GET_URL, null, null));
    }
}
