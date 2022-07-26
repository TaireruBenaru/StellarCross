using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class DungeonGenerator : MonoBehaviour
{
    public FloorEntry[] FloorEntries;
    public DungeonCell[] DungeonCells;
    public int CurrentEntry;

    public Transform MiniMapParent;
    public GameObject MiniMapPrefab;

    public int XPos;
    public int YPos;
    public CellList[,] Tiles = new CellList[XBound,YBound];

    public static int XBound = 32;
    public static int YBound = 48;

    public List<GameObject> MiniMap = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        XPos = 4;
        YPos = YBound/2;

        int FloorCycles = Random.Range(FloorEntries[CurrentEntry].MinSize, FloorEntries[CurrentEntry].MaxSize);
        Tiles[XPos, YPos] = CellList.Downstairs;
        for (int i = 0; i < FloorCycles; i++)
        {
            Debug.Log("Current Position: (" + XPos + "," + YPos + ")");
            DungeonCell Cell = DungeonCells[(int)Tiles[XPos, YPos]];
            CellList NextCell = Cell.ConnectionProspect[Random.Range(0, Cell.ConnectionProspect.Length)];
            Debug.Log("Next Cell: " + NextCell);
            CellCheck(NextCell);
        }

        DrawMinimap();
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
            case CellList.RightSideRoom:
                if(Tiles[XPos, YPos+1] == CellList.None && Tiles[XPos, YPos+2] == CellList.None) //Bottom Part is empty
                {
                    if(Tiles[XPos+1, YPos+1] == CellList.None && Tiles[XPos+1, YPos+2] == CellList.None) //Top Part is empty
                    {
                        YPos += 1;
                        Tiles[XPos, YPos] = CellList.RightSideRoom;
                        Tiles[XPos, YPos+1] = CellList.Reserved;
                        Tiles[XPos+1, YPos] = CellList.Reserved;
                        Tiles[XPos+1, YPos+1] = CellList.Reserved;
                        XPos++;
                        YPos++;
                    }
                }

            break;
        }
    }

    void DrawMinimap()
    {
        for (int i = 0; i < XBound; i++)
        {
            for (int j = 0; j < YBound; j++)
            {
                DungeonCell Cell = DungeonCells[(int)Tiles[i, j]];
                if(Tiles[i, j] != CellList.None)
                {
                    Vector3 Position = new Vector3(i-26, j*16, 0);
                    MiniMap.Add(Instantiate(MiniMapPrefab, Vector3.zero, Quaternion.identity, MiniMapParent));
                    MiniMap.Last().transform.localPosition = Position;
                    MiniMap.Last().GetComponent<Image>().sprite = Cell.Image;
                    if(Cell.Size == CellSize.Small)
                    {
                        MiniMap.Last().GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
                    }
                    else
                    {
                        MiniMap.Last().GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
                    }
                    MiniMap.Last().name = Cell.ToString();
                }
            }
        }
    }
}
