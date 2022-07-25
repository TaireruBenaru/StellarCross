using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public FloorEntry[] FloorEntries;
    public DungeonCell[] DungeonCells;
    public int CurrentEntry;

    public int XPos = 40;
    public int YPos = 32;
    public CellList[,] Tiles = new CellList[80,64];

    // Start is called before the first frame update
    void Start()
    {
        int FloorCycles = Random.Range(FloorEntries[CurrentEntry].MinSize, FloorEntries[CurrentEntry].MaxSize);
        Tiles[40, 32] = CellList.Downstairs;
        for (int i = 0; i < FloorCycles; i++)
        {
            Debug.Log("Current Position: (" + XPos + "," + YPos + ")");
            DungeonCell Cell = DungeonCells[(int)Tiles[XPos, YPos]];
            CellList NextCell = Cell.ConnectionProspect[Random.Range(0, Cell.ConnectionProspect.Length)];
            Debug.Log("Next Cell: " + NextCell);
            CellCheck(NextCell);
        }
    }

    void CellCheck(CellList Cell)
    {
        switch (Cell)
        {
            case CellList.UpHallway:
                if(Tiles[XPos, YPos+1] == CellList.None)
                {
                    YPos += 1;
                    Tiles[XPos, YPos] = CellList.UpHallway;
                }
            break;
            case CellList.UpHallwayRightOpening:
                if(Tiles[XPos, YPos+1] == CellList.None && Tiles[XPos+1, YPos+1] == CellList.None)
                {
                    YPos += 1;
                    Tiles[XPos, YPos] = CellList.UpHallwayRightOpening;
                }
            break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
