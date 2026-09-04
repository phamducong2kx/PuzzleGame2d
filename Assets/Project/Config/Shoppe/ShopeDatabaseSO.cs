using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopeDatabbseSO", menuName = "ScrewPuzzle/ShoppeDatabaseSO")]
public class ShopeDatabaseSO : ScriptableObject
{

    //quanr lis danh sách các gói vật phẩm
    public List<PackageShoppe> listPackgeShoppe = new List<PackageShoppe>();



}
//class chứa tohong tin 1 gói vật phẩm
[Serializable]
public class PackageShoppe
{
    public string idPackage;
    public List<ItemShope> listItemId = new List<ItemShope>();
    public int amount;
    public float priceDolar;
    public float priceCoint;
    public Sprite iconOverrite;
    public TypePackageShoppe typePacakgaeShopee;
    public string namePackageType;
    public bool hasBanner;


}

[Serializable]
public class ItemShope
{
    public string idItem;
    public int amount;
}

//loai goi
[Serializable]
public enum TypePackageShoppe
{
    VatPhamXu,
    CungCapXu,
    VatPhamTangCuong,
    NhomsVatPhamSieuKhuyenMai,
    GoiTangLuc,
    GoiBatDauMoi,
    GoiSieuCap,

}
