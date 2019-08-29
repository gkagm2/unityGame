using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LoadFromFileExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAssetBundle());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator StartAssetBundle()
    {
        var fileStream = new FileStream(Path.Combine(Application.streamingAssetsPath, "myasset"), FileMode.Open, FileAccess.Read);
        var bundleLoadRequest = AssetBundle.LoadFromStreamAsync(fileStream);
        yield return bundleLoadRequest;

        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if(myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }
        var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("Y");
        yield return assetLoadRequest;

        GameObject prefab = assetLoadRequest.asset as GameObject;
        Instantiate(prefab);

        myLoadedAssetBundle.Unload(false);
    }
}
