using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPlane : MonoBehaviour
{
    // Tất cả holes trên background
    public List<Hole> backgroundHoles = new List<Hole>();


    private void Start()
    {

    }
    public void AddBackgroundHole(Hole hole)
    {
        hole.SetAsBackgroundHole();
        backgroundHoles.Add(hole);
    }

    public void SetupHoleBackground()
    {
        var listHole = GetComponentsInChildren<Hole>();
        foreach (var hole in listHole)
        {
            backgroundHoles.Add(hole);
            hole.isBackgroundHole = true;
        }
    }
}
