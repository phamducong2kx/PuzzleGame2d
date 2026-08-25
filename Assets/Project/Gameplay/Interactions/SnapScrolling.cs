using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SnapScrolling : MonoBehaviour, IEndDragHandler, IDragHandler
{

    [Header("UI Elements")]

    [SerializeField] private RectTransform contentPanel;
    public List<Transform> chapters;


    [SerializeField] private float snapSpeed = 10f; // Tốc độ hút vào tâm

    //chi so nayt kha quan trong day
    public int targetChapterIndex = 0;

    private bool isDragging = false;


    void Start()
    {

    }


    void Update()
    {


        if (!isDragging)
        {
            //toa do globla cua viewport
            var currentX_viewport = gameObject.transform.position.x;
            //   Debug.Log("tagretindex hien tai la " + targetChapterIndex);
            var currentX_chapter = chapters[targetChapterIndex].transform.position.x;

            float minDistance = currentX_viewport - currentX_chapter;
            float targetLocalX = contentPanel.localPosition.x + minDistance;
            Vector3 currentPos = contentPanel.localPosition;
            currentPos.x = Mathf.MoveTowards(currentPos.x, targetLocalX, Time.deltaTime * snapSpeed);

            // 5. Cập nhật vị trí mới cho Panel
            contentPanel.localPosition = currentPos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    // Hàm này tự động kích hoạt NGAY KHI người dùng vừa thả tay ra
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;


        //toa do glaobla x cua viewport
        var currentX_viewport = gameObject.transform.position.x;

        //kahia bao max
        float minDistance = float.MaxValue;
        float distance = 0;

        //tim khoang cahc ngan nhat tu tam viewport toi tam cua ca item
        for (int i = 0; i < chapters.Count; i++)
        {
            distance = Mathf.Abs(chapters[i].position.x - currentX_viewport);

            if (distance <= minDistance)
            {
                minDistance = distance;
                targetChapterIndex = i;

            }
            //refresh danh sach level data va 2 nut hien thi previous and next



        }
        //tim chapter tuong ung voi targetChapterindex
        var chapter = chapters[targetChapterIndex].GetComponent<ChapterICon>();
        if (chapter == null)
        {
            Debug.LogError("Chapter == null , loi");
        }
        else
        {
            OnChangeChapter(chapter.chapterID);
        }



    }

    private void OnChangeChapter(int chapterID)
    {
        //thay doi cai nut 
        UIManager.Instance.pannelLevelSelect.SetupActiceButtonChangePage(targetChapterIndex, chapters.Count);

        //thay doi danh sách chapter 
        UIManager.Instance.pannelLevelSelect.RefreshButtonIcon(chapterID);
    }

    //set up chapter co id tuong ung lam trung tam
    public void SetupChapter_Center(int chapterId)
    {
        //pphai xem la no coid la bao nhieu 
        for (int i = 0; i < chapters.Count; ++i)
        {
            //lay component chapter
            var chapter = chapters[i].GetComponent<ChapterICon>();
            if (chapter.chapterID == chapterId)
            {
                targetChapterIndex = i;
                return;
            }

        }

    }

    public void SetUPListItem()
    {
        var list = contentPanel.GetComponentsInChildren<ChapterICon>();
        foreach (var x in list)
        {
            chapters.Add(x.transform);
        }
    }
}
