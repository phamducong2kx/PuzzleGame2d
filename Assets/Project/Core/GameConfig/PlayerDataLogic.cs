using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDataLogic : MonoBehaviour
{
    public AvatarData avatarDatabase;
    //check xme 1 level da unlock hay chua
    public bool CheckLevelUnlock(PlayerData data, int idlevel)
    {

        var levelCantim = data.levelProgresses[idlevel - 1];
        return levelCantim.isUnlock;
    }

    //check xme 1 level da playing hay chua
    public bool CheckLevelPlaying(PlayerData data, int idlevel)
    {
        //tim liem level
        var levelCantim = data.levelProgresses[idlevel - 1];

        return levelCantim.isPlaying;
    }

    //check xem level da pass hay chua
    public bool CheckLevelPass(PlayerData data, int idLevel)
    {
        return data.levelProgresses[idLevel - 1].isPass;
    }

    //check xem level nay co may sao
    public int GetNumberStarOfLevel(PlayerData data, int idLevel)
    {
        return data.levelProgresses[idLevel - 1].star;
    }

    //thêm skill vào danh sách list skill pregress;se dung o hamset up moi skill
    public void AddSkillToList(string idSkill)
    {


        //danh sasch skill
        var skillProgress = SaveManager.Data.listSkill;

        Debug.Log("danh sachs hien tai cua skill la " + skillProgress.Count);

        //tim kiem skillthoe id;
        var skill = skillProgress.FirstOrDefault(x => x.idSkill.Equals(idSkill));
        if (skill != null)
        {
            Debug.Log("skill nay da ton tai ");
        }
        else
        {
            Debug.Log("skill nay chua  ton tai ");
            //tao 1 skill moi va them vao
            var a = new SkillProgress();
            a.idSkill = idSkill;
            a.amount = 10;
            SaveManager.Data.listSkill.Add(a);
            SaveManager.SaveData();
            Debug.Log("da luu xogn skill");


        }




    }

    //tim kiem skillProgress trong danh sách skill cua data
    public SkillProgress GetSkillProgress(string skillid)
    {
        var skillProgress = SaveManager.Data.listSkill;
        var skill = skillProgress.FirstOrDefault(x => x.idSkill.Equals(skillid));
        return skill;
    }

    //update cooldown skill sau khi dung
    public void UpdateCoolDown_AfterUsing(string idSkill, float cooldownNow)
    {
        //tim kiem skill theo id
        var skill = SaveManager.Data.listSkill.FirstOrDefault(x => x.idSkill.Equals(idSkill));
        if (skill == null)
        {
            Debug.Log("khong ton tai skill co id nhu nay");
        }
        else
        {

            skill.cooldownRemaining = cooldownNow;
            SaveManager.SaveData();
        }
    }

    //update amoutn skill sau khi sai
    public void UpdateAmountSkill(string idSkill, int numberChange)
    {
        //tim kiem skill theo id
        var skill = SaveManager.Data.listSkill.FirstOrDefault(x => x.idSkill.Equals(idSkill));
        if (skill == null)
        {
            Debug.Log("khong ton tai skill co id nhu nay");
        }
        else
        {
            skill.amount = skill.amount + numberChange;

            SaveManager.SaveData();
        }
    }

    public void UpdateAmount_AfterBuySkillInShoppeSkill(string idSkill)
    {
        //tim kiem skill theo id
        var skill = SaveManager.Data.listSkill.FirstOrDefault(x => x.idSkill.Equals(idSkill));
        if (skill == null)
        {
            Debug.Log("khong ton tai skill co id nhu nay");
        }
        else
        {
            skill.amount += 1;

            SaveManager.SaveData();
        }
    }

    public float GetCoint(PlayerData data)
    {
        return data.coint;
    }

    //thay avatar cho player
    public void ChangeAvatar(PlayerData data, string idAvatar)
    {
        data.currentIdAvatar = idAvatar;
        SaveManager.SaveData();
    }

    //lay idcurrent avatar
    public string GetIdCurrentAvatar(PlayerData data)
    {
        return data.currentIdAvatar;

    }

    //tim sprite tu id avatar
    public Sprite GetSpriteFormIdAvatar(string idAvatar)
    {
        var avatar = avatarDatabase.listAvatarInfo.FirstOrDefault(x => x.id.Equals(idAvatar));
        return avatar.sprite;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
