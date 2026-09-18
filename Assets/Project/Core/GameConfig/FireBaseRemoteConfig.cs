using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using System.Threading.Tasks;
using UnityEngine;


public class FireBaseRemoteConfig : MonoBehaviour
{

    void Start()
    {

    }

    public async Task SetupBegin()
    {
        //check xem thiết bị user đủ điều kiện chạy firrebase hay không
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            await LoadFirebaseConfigAsync(FirebaseKey.PACKAGESHOPS);
        }
        else
        {
            Debug.Log("ko du diu kien chay firebase");
        }
    }

    private async Task LoadFirebaseConfigAsync(string key)
    {
        //cấu hình thời gian cache
        ConfigSettings configSetting = new ConfigSettings();
        //#if UNITY_EDITOR
        //treen untiy editor
        configSetting.MinimumFetchIntervalInMilliseconds = 0;
        //#else
        // configSetting.MinimumFetchIntervalInMilliseconds = 3600000;

        //tren dien thoai .tinh theo ms , 1h co 3600000 ms, mắc định là 12h
        // configSetting.MinimumFetchIntervalInMilliseconds = 3600000;
        //ap dung cau hinh vao remote cònig

        await FirebaseRemoteConfig.DefaultInstance.SetConfigSettingsAsync(configSetting);




        //  await FirebaseRemoteConfig.DefaultInstance.SetConfigSettingsAsync(configSetting);

        //fetch du lieu
        try
        {
            //so sánh tỉa dữ liệu , nếu khác dữ liệu hay không khác dữ liệu đều trả về thành công
            await FirebaseRemoteConfig.DefaultInstance.FetchAndActivateAsync();
            Debug.Log("Fetch remote thanh cong");
        }
        catch (Exception ex)
        {
            Debug.Log("loi " + ex.Message);
        }

        var dataRamBefore = GameConfigManager.Instance.itemLogic.listPackages;
        //  Debug.Log("chuoi json hien tai la " + dataRamBefore);

        //conver json sang string , 
        string jsonString = FirebaseRemoteConfig.DefaultInstance.GetValue(FirebaseKey.PACKAGESHOPS).StringValue;
        Debug.Log("chuoi json hien tai la " + jsonString);

        //ghi de du lieu neu no != null
        if (!string.IsNullOrEmpty(jsonString))
        {
            JsonUtility.FromJsonOverwrite(jsonString, GameConfigManager.Instance.itemLogic);
            JsonUtility.FromJsonOverwrite(jsonString, dataRamBefore);
            Debug.Log("Đã cập nhật SO hiện tại");
        }
    }
}