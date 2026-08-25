using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DailyRewardExport : EditorWindow
{

    private string outputFolder = "Assets/Project/Config/DailyReward";
    //cấu hình dữ liệu 
    [MenuItem("Tools/DailyReward/export daily reward")]
    public static void ShowWindow()
    {
        GetWindow<DailyRewardExport>("xuat dang sachs daily reward sang 1 cai asset");
    }


    private void OnGUI()
    {
        GUILayout.Label("Export list daily reward to file asset", EditorStyles.boldLabel);
        GUILayout.Space(8);

        outputFolder = EditorGUILayout.TextField("Ouput folder", outputFolder);






        if (GUILayout.Button("Export now", GUILayout.Height(30)))
        {
            Export();
        }

    }


    private void Export()
    {
        //tao 1 instance cua scriptableobject
        var x = CreateInstance<DailyRewardConfig>();
        x.dailyRewards = AddDailyreward();





        //nếu chưa có đường dẫn thư mục thì tạo đường dẫn thư mục này
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        //đặt tên cho file leveldata sẽ tạo trong đg dẫn thư muc
        string fileName = $"{outputFolder}/Daily_Reward.asset";

        //tạo file asset
        AssetDatabase.CreateAsset(x, fileName);

        //lưu lại
        AssetDatabase.SaveAssets();

        //refreah laij thuw muc object
        AssetDatabase.Refresh();

    }


    //danh sách các suej kiện,
    public List<DailyReward> AddDailyreward()
    {
        List<DailyReward> dailyRewards = new List<DailyReward>();
        var day1 = new DailyReward()
        {
            day = 1,
            listRewardItem = new List<Item_DailyReward>()
            {
                new Item_DailyReward() {idItem = "1", amount = 50},

            }
        };
        var day2 = new DailyReward()
        {
            day = 2,
            listRewardItem = new List<Item_DailyReward>()
            {
                new Item_DailyReward() {idItem = "1",amount = 100},
                new Item_DailyReward() {idItem = "2",amount = 1},

            }
        };
        var day3 = new DailyReward()
        {
            day = 3,
            listRewardItem = new List<Item_DailyReward>()
            {
                new Item_DailyReward() {idItem = "1",amount = 200},
                new Item_DailyReward() {idItem = "3",amount = 1},

            }
        };
        var day4 = new DailyReward()
        {
            day = 4,
            listRewardItem = new List<Item_DailyReward>()
            {
                new Item_DailyReward() {idItem = "1",amount =300},
                new Item_DailyReward() {idItem = "4",amount = 1},

            }
        };
        var day5 = new DailyReward()
        {
            day = 5,
            listRewardItem = new List<Item_DailyReward>()
            {
                new Item_DailyReward() {idItem = "1",amount = 400},
                new Item_DailyReward() {idItem = "5",amount = 1},

            }
        };
        var day6 = new DailyReward()
        {
            day = 6,
            listRewardItem = new List<Item_DailyReward>()
            {
               new Item_DailyReward() {idItem = "1",amount = 500},
               new Item_DailyReward() {idItem = "4",amount = 1},
               new Item_DailyReward() {idItem = "5",amount = 1},

            }
        };
        var day7 = new DailyReward()
        {
            day = 7,
            listRewardItem = new List<Item_DailyReward>()
            {
               new Item_DailyReward() {idItem = "1",amount = 1000},
               new Item_DailyReward() {idItem = "2",amount = 1},
               new Item_DailyReward() {idItem = "3",amount = 1},
               new Item_DailyReward() {idItem = "4",amount = 1},
               new Item_DailyReward() {idItem = "5",amount = 1},

            }
        };
        dailyRewards.Add(day1);
        dailyRewards.Add(day2);
        dailyRewards.Add(day3);
        dailyRewards.Add(day4);
        dailyRewards.Add(day5);
        dailyRewards.Add(day6);
        dailyRewards.Add(day7);
        return dailyRewards;
    }

}

