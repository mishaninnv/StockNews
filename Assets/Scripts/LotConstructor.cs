using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LotConstructor : MonoBehaviour
{
    public Image avatar, lotImage;
    public TextMeshProUGUI level, count, playerName, lotName, price;

    public List<AssetReference> list = new List<AssetReference>();

    public void Initialize(LoadLots.LotInformation lotInformation)
    {
        GetLotTexture(lotInformation.lotName,
             sprite => { lotImage.sprite = sprite; });

        GetAvatarTexture(lotInformation.avatar,
            error => { Debug.Log("Error: " + error); },
            texture2d =>
            {
                var sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height),
                    new Vector2(.5f, .5f));
                avatar.sprite = sprite;
            });

        level.SetText(lotInformation.level);
        count.SetText(lotInformation.count);
        playerName.SetText(lotInformation.playerName);
        lotName.SetText(lotInformation.lotName);
        price.SetText(lotInformation.price);
    }

    private void GetLotTexture(string assetName, Action<Sprite> onSuccess)
    {
        StartCoroutine(GetLotSpriteCoroutine(assetName, onSuccess));
    }
    
    private static IEnumerator GetLotSpriteCoroutine(string assetName, Action<Sprite> onSuccess)
    {
        var sprite = Addressables.LoadAssetAsync<Sprite>(assetName);
        yield return sprite;
        if (sprite.Status == AsyncOperationStatus.Succeeded)
        {
            onSuccess(sprite.Result);
        }
    }

    private void GetAvatarTexture(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        StartCoroutine(GetAvatarTextureCoroutine(url, onError, onSuccess));
    }

    private static IEnumerator GetAvatarTextureCoroutine(string url, Action<string> onError,
        Action<Texture2D> onSuccess)
    {
        var unityWebRequest = UnityWebRequestTexture.GetTexture(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ProtocolError ||
            unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            onError(unityWebRequest.error);
        }
        else
        {
            if (unityWebRequest.downloadHandler is DownloadHandlerTexture downloadHandlerTexture)
                onSuccess(downloadHandlerTexture.texture);
        }
    }
}