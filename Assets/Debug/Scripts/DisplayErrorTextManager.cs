using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayErrorTextManager : MonoBehaviour
{
    public static DisplayErrorTextManager Instance { get; private set; }

    [SerializeField] GameObject ErrorCanvas;
    [SerializeField] TextMeshProUGUI errorText;

    string errorCode = "ERRORCODE:";

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
            Destroy(gameObject);
            return;
        }
    }

    private void Start() => CloseErrorCanvas();

    // �G���[���b�Z�[�W��\������@
    public void DisplayError(string errorMessage)
    {
        ErrorCanvas.SetActive(true);
        errorText.text = string.Format("{0}{1}", errorCode, errorMessage);
    }

    // �G���[�L�����o�X���\���ɂ���
    public void CloseErrorCanvas()
    {
        ErrorCanvas.SetActive(false);
    }

    // �^�C�g���ɖ߂�
    public void TitleBack()
    {
        StartCoroutine(WaitCloseErrorCanvas());
        FadeManager.Instance.LoadScene("TestScene");
    }

    // TODO: ���������x�����������悤�ȃR�[�h��ǉ�����

    IEnumerator WaitCloseErrorCanvas()
    {
        yield return new WaitForSeconds(3f);
        ErrorCanvas.SetActive(false);
    }
}
