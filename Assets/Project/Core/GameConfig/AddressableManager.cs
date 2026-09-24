using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.ReloadAttribute;

public class AddressableManager : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float timeRemaining;

    private void Awake()
    {
        duration = 4f;
    }
    private void Start()
    {

    }

    //tải catalog remote về cache
    public async Task LoadCatalogUpdate()
    {
        await Addressables.InitializeAsync().Task;
        List<string> catalogUpdate = null;
        try
        {
            var checkHandle = Addressables.CheckForCatalogUpdates(false);
            catalogUpdate = await checkHandle.Task;
            Addressables.Release(checkHandle);
            Debug.Log("đã check đc cattalog trên server");
        }
        catch (Exception ex)
        {
            Debug.Log($"không check dc catalog trên server,  CO loi xay ra : {ex.Message}");
        }
        if (catalogUpdate != null && catalogUpdate.Count > 0)
        {
            var update = Addressables.UpdateCatalogs(catalogUpdate, false);
            await update.Task;
            Addressables.Release(update);
            Debug.Log("Đã tải dc catalog trên server xuống cache");
        }
        else
        {
            Debug.Log("KO kết nối dc mạng để tải catalog hoắc là");
        }
    }

    public async Task DownloadBundleAssetAsyncToCache(string keyLabel, Action<float> setupPregressBar)
    {
        //kiểm tra xem các bundle đã có sẵn trong cache máy người dùng hay chưa
        var totalBytes = await Addressables.GetDownloadSizeAsync(keyLabel).Task;
        if (totalBytes > 0)
        {
            //tải xuông các file về cache
            var downloadResource = Addressables.DownloadDependenciesAsync(keyLabel, false);
            while (!downloadResource.IsDone)
            {
                float pregress = downloadResource.PercentComplete;
                setupPregressBar?.Invoke(pregress);
                await Task.Yield();
            }

            //giai phong bo nho
            if (downloadResource.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Tải xuống asset từ remote thành công");
            }
            Addressables.Release(downloadResource);

            //tair lên ram dữ liệu về âm thanh


            //sau đó return hàm
            return;
        }
        else
        {
            //chay 1 cai courotine nếu nmhuw mà đã có dữ liệu trong cache rồi
            await FakeLoadingAsync(1, setupPregressBar);
            Debug.Log("Dữ liệu trong cache nên ko cần tải hoặc là ko có sự khác biết");
        }


    }



    private async Task FakeLoadingAsync(float delayTime, Action<float> actionTime)
    {
        timeRemaining = 0;
        while (timeRemaining < duration)
        {
            timeRemaining += Time.deltaTime;
            float x = timeRemaining / duration;
            actionTime?.Invoke(x);
            await Task.Yield();
        }

    }

    void Update()
    {

    }
}
