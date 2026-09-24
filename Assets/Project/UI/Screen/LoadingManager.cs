using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Image loadingBar;
    public TextMeshProUGUI progressText;
    void Start()
    {

    }
    public async Task SetupLoading()
    {
        //load firebase
        await GameConfigManager.Instance.FireBaseRemoteConfig.SetupBegin();

        //tải catalog
        await GameConfigManager.Instance.addressableManager.LoadCatalogUpdate();

        //tải bundle về cache nếu có 
        await GameConfigManager.Instance.addressableManager.DownloadBundleAssetAsyncToCache(AddressableLabels.PRELOAD, (float x) =>
        {
            loadingBar.fillAmount = x;
            progressText.text = $"{Math.Round(x * 100, 0)}%...";
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
