using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChapterICon : MonoBehaviour
{
    public int chapterID;
    [SerializeField] private TextMeshProUGUI textChapter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(int index)
    {
        chapterID = index;
        textChapter.text = $"Chapter {index}";

    }
}
