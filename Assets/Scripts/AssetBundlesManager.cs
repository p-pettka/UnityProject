using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;


public class AssetBundlesManager : Singleton<AssetBundlesManager>
{
    public string assetBundleName;
    public string assetBundleURL;
    public string assetBundleLocalFileName;
    public string abVersionURL;
    public uint abVersion;
    public uint ab2Version;

    private AssetBundle ab;
    private AssetBundle ab2;

    private IEnumerator LoadAssets(string name, Action<AssetBundle> bundle)
    {
        AssetBundleCreateRequest abcr;
        string path = Path.Combine(Application.streamingAssetsPath, name);
        abcr = AssetBundle.LoadFromFileAsync(path);
        yield return abcr;
        bundle.Invoke(abcr.assetBundle);
        Debug.LogFormat(abcr.assetBundle == null ? "Failed to Load Asset Bundle : {0}" : "Asset Bundle {0} loaded", name);
    }

    private IEnumerator LoadAssetsFromURL()
    {
        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleURL, abVersion,0);
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
        Debug.Log("Downloaded bytes : " + uwr.downloadedBytes);
    }

    private IEnumerator LoadAssetsFromLocalURL()
    {
        string assetBundleLocalURL = ("file:///"+Application.streamingAssetsPath+"/"+assetBundleLocalFileName);
        UnityWebRequest uwrLocal = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleLocalURL, ab2Version, 0);
        uwrLocal.SetRequestHeader("Content-Type", "application/json");
        uwrLocal.SetRequestHeader("User-Agent", "DefaultBrowser");
        yield return uwrLocal.SendWebRequest();
        if (uwrLocal.isNetworkError || uwrLocal.isHttpError)
        {
            Debug.Log(uwrLocal.error);
        }
        else
        {
            ab2 = DownloadHandlerAssetBundle.GetContent(uwrLocal);
        }
        Debug.Log(ab2 == null ? "Failed to download Asset Bundle from local URL" : "Asset Bundle downloaded from local URL");
        Debug.Log("Downloaded bytes : " + uwrLocal.downloadedBytes);
    }

    private IEnumerator GetABVersion()
    {
        UnityWebRequest uwr = UnityWebRequest.Get(abVersionURL);
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("User-Agent", "DefaultBrowser");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        Debug.Log(uwr.downloadHandler.text);
        abVersion = uint.Parse(uwr.downloadHandler.text);
    }

    public Sprite GetSprite(string assetName)
    {
        return ab.LoadAsset<Sprite>(assetName);
    }

    public void LoadScene(int sceneNumber)
    {
        string[] scenePath = ab2.GetAllScenePaths();
        SceneManager.LoadScene(scenePath[sceneNumber]);
    }

    private IEnumerator Start()
    {
        yield return StartCoroutine(GetABVersion());
        yield return StartCoroutine(LoadAssets(assetBundleName, result => ab = result));
        yield return StartCoroutine(LoadAssetsFromURL());
        yield return StartCoroutine(LoadAssetsFromLocalURL());
    }

    /*private void Start()
    {
        if (string.IsNullOrEmpty(assetBundleURL))
        {
            StartCoroutine(LoadAssets());
        }
        else
        {
            StartCoroutine(LoadAssetsFromURL());
        }
    }*/
}