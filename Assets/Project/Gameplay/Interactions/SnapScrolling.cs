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
    public List<ChapterICon> chapters;


    [SerializeField] private float snapSpeed = 10f; // Tốc độ hút vào tâm

    //chi so nayt kha quan trong day
    public int targetChapterIndex = 0;

    private bool isDragging = false;
    private RectTransform rectTransform;
    private Vector2 targetPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

    }
    void Start()
    {
        targetPosition = contentPanel.anchoredPosition;
    }



    void Update()
    {
        if (!isDragging)
        {
            //cos index chapter rôi, tìm klhoarng cách ngắn nhất từ viewport tới chapter đó
            Vector2 distance = rectTransform.InverseTransformPoint(chapters[targetChapterIndex].transform.position);

            //tinh dc vị trí targetPositoon : là vị trí mà contetnPannel sẽ chạy tới
            targetPosition = new Vector2(contentPanel.anchoredPosition.x - distance.x, contentPanel.anchoredPosition.y);

            //mooix frame vị trí của content sẽ bị nhích 1 đoạn snawpSpeed * time.Deltatime
            contentPanel.anchoredPosition = Vector2.MoveTowards(
             contentPanel.anchoredPosition,
             targetPosition,
             snapSpeed * Time.deltaTime);



        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        float minDist = float.MaxValue;

        for (int i = 0; i < chapters.Count; i++)
        {
            // 1. Tọa độ của chapter so với Viewport
            Vector3 chapterLocalPos = rectTransform.InverseTransformPoint(chapters[i].transform.position);

            if (minDist > Mathf.Abs(chapterLocalPos.x))
            {
                minDist = Mathf.Abs(chapterLocalPos.x);
                targetChapterIndex = i;
            }
        }
        OnChangeChapter(chapters[targetChapterIndex].chapterID);
    }







    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
    }




    void Update2()
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

            if (chapters[i].chapterID == chapterId)
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
            chapters.Add(x);
        }
    }
}
