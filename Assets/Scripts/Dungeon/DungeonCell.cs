using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonCell", menuName = "Dungeon Cell")]
public class DungeonCell : ScriptableObject
{
    public CellList Self;
    public CellList[] ConnectionProspect;
    public CellSize Size;
}

public enum CellList
{
    None,
    LeftHallway,
    RightHallway,
    UpHallway,
    DownHallway,
    Downstairs,
    TIntersection,
    RightSideRoom,
    UpHallwayRightOpening,
    UpHallwayDeadEnd,
    RightHallwayDeadEnd,
}

public enum CellSize
{
    Small,
    Big,
}
