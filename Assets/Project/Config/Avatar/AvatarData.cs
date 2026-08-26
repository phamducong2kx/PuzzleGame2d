using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "AvartarData", menuName = "ScrewPuzzle/Avatar")]
public class AvatarData : ScriptableObject
{
    public List<AvatarInfo> listAvatarInfo = new List<AvatarInfo>();
}

[Serializable]
public class AvatarInfo
{
    public string id;
    public Sprite sprite;
}