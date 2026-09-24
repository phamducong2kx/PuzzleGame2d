using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DefaultState : ISKillState
{
    public void OntapBolt(Bolt bolt)
    {

        if (bolt.isPickedUp)
        {
            //Tham chieu toi bolt dang dc pickup la null;
            InputHandler.Instance.pickedBolt = null;
            //goi ham pickdonw
            PickDownNormal(bolt);
            //am thanh
            AudioManagement.Instance.PlaySound(AddressableLabels.PICKDOWNBOLT, 1, 1);
        }
        else
        {
            //con neu nhu bolt dang chua dc pick up thi phai
            //xem neu cai pikcup tham chieu den bolt hien tai ma khac null thi phaic huyen no sang pickdown
            PickDownNormal(InputHandler.Instance.pickedBolt);

            //sau do thbien thma chieu se thamchieu toi bolt hien tai
            InputHandler.Instance.pickedBolt = bolt;

            //sau do chuyen sang state pick up
            PickUpNormal(bolt);
            AudioManagement.Instance.PlaySound(AddressableLabels.PICKUPBOLT, 1, 1);

        }
    }

    public void OntapHole(Hole hole)
    {
        if (!hole.isBackgroundHole || InputHandler.Instance.pickedBolt == null) return;

        //Lấy danh sách cac collider timf thayas sau khi bắn tia 
        var listHole = GameManager.Instance.holeSystem.CheckConnectBoltToHole(hole);

        if (listHole == null)
        {
            AudioManagement.Instance.PlaySound(AddressableLabels.HOLEWARNING, 1, 2);
            return;
        }

        //tim kiem bg
        var bgHole = listHole.FirstOrDefault(x => x.isBackgroundHole == true);
        listHole.Remove(bgHole);

        if (GameManager.Instance.holeSystem.HandleHoleCollider(bgHole, listHole, 0.04f))
        {
            //xoas danh sacsh hole cũ mà bolt đính vào 
            InputHandler.Instance.pickedBolt?.RemoveConnectToHole_OfBolt();

            //đưa vòa danh sách hole mới
            InputHandler.Instance.pickedBolt?.AttachConnectToHole_OfBolt(bgHole, listHole);

            //chuiyen sang trang thai pick doen cho cai dinh o vi tri moi
            PickDownNormal(InputHandler.Instance.pickedBolt);

            //am thanh
            AudioManagement.Instance.PlaySound(AddressableLabels.PICKDOWNBOLT, 1, 1);

            //set up lai thm chieu toi null
            InputHandler.Instance.pickedBolt = null;


        }
        else
        {
            //am thanh khong thnah cong khi nham voa
            AudioManagement.Instance.PlaySound(AddressableLabels.HOLEWARNING, 1, 2);
            //  Debug.Log("toi chuwahieu saio o dau ae oi");
        }


    }

    public void OntapPlank(Plank plank)
    {
        return;
    }

    public void OnEnterState()
    {

    }

    public void OnExitState()
    {
        SetUpAllBoltPickDownNormal();
    }

    private void PickDownNormal(Bolt bolt)
    {
        if (bolt == null) return;
        bolt.isPickedUp = false;

        EventManager.InvokeBoltPlaced(bolt);


        bolt.spriteRenderer.transform.DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.OutQuad);

        // bolt.spriteRenderer.transform.localScale

        bolt.spriteRenderer.sprite = bolt.boltIdle;
    }
    private void PickUpNormal(Bolt bolt)
    {
        if (bolt == null) return;
        bolt.isPickedUp = true;

        EventManager.InvokeBoltPickedUp(bolt);

        bolt.spriteRenderer.transform.DOMoveY(0.2f, 0.2f).SetEase(Ease.OutQuad).SetRelative();


        bolt.spriteRenderer.sprite = bolt.boltPickUp;
    }

    public void SetUpAllBoltPickDownNormal()
    {
        foreach (var x in LevelLoader.Instance.spawnedBolts)
        {
            PickDownNormal(x);
        }

    }
}
