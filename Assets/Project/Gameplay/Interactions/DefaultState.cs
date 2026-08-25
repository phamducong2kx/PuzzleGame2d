using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        }
    }

    public void OntapHole(Hole hole)
    {
        //Lấy danh sách sau khi physic.overlapCircleAll
        var listHole = Hole.CheckBoltToHole(hole);

        if (listHole == null) return;

        Hole bgHole = null;
        foreach (var x in listHole)
        {
            if (x.isBackgroundHole)
            {
                bgHole = x;
                listHole.Remove(x);
                break;
            }
        }
        if (Hole.HandleHoleCollider(bgHole, listHole))
        {
            //xoas danh sacsh hole cũ mà bolt đính vào 
            InputHandler.Instance.pickedBolt?.RemoveConnectToHole_OfBolt();

            //đưa vòa danh sách hole mới
            InputHandler.Instance.pickedBolt?.AttachConnectToHole_OfBolt(bgHole, listHole);

            //chuiyen sang trang thai pick doen cho cai dinh o vi tri moi
            PickDownNormal(InputHandler.Instance.pickedBolt);

            //set up lai thm chieu toi null
            InputHandler.Instance.pickedBolt = null;
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
