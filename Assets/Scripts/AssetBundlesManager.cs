using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;


public class AssetBundlesManager : Singleton<AssetBundlesManager>
{
    public string assetBundleName;
    public string assetBundleURL;

    private AssetBundle ab;

    private IEnumerator LoadAssets()
    {
        AssetBundleCreateRequest abcr;
        string path = Path.Combine(Application.streamingAssetsPath, assetBundleName);
        abcr = AssetBundle.LoadFromFileAsync(path);
        yield return abcr;
        ab = abcr.assetBundle;
        Debug.Log(ab == null ? "Failed to load Asset Bundle" : "Asset Bundle loaded");
    }

    private IEnumerator LoadAssetsFromURL()
    {
        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleURL);
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("User-Agent", "DefaultBrowser");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            ab = DownloadHandlerAssetBundle.GetContent(uwr);
        }
        Debug.Log(ab == null ? "Failed to download Asset Bundle" : "Asset Bundle downloaded");
    }

    public Sprite GetSprite(string assetName)
    {
        return ab.LoadAsset<Sprite>(assetName);
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(assetBundleURL))
        {
            StartCoroutine(LoadAssets());
        }
        else
        {
            StartCoroutine(LoadAssetsFromURL());
        }
    }
}