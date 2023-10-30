using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Yandex : MonoBehaviour

{
    [DllImport("__Internal")]
    private static extern void Hello();
    [DllImport("__Internal")]
    private static extern void GiveMePlayerData();


    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private RawImage _photo;
    // Start is called before the first frame update
    public void HelloButton()
    {
        Hello();
    }
    public void GiveMeDataButton()
    {
        GiveMePlayerData();
    }
    public void SetName(string name)
    {
        _nameText.text = name;
    }
    public void SetPhoto(string url)
    {
        StartCoroutine(DownloadImage(url));
    }
    IEnumerator DownloadImage(string mediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
            _photo.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}
