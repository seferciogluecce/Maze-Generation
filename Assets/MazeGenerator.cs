using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{

    List<List<Vector3>> Grid;
    List<List<Cell>> CellGrid;
    ObjectPoints PointsOnThePlane;
    int MazeWidth = 11;
    int MazeHeight = 11;
    readonly int planeHeight = 11;
    readonly int planeWidth = 11;
    GameObject BorderParent;//Empty parent object to keep borders transform
    RecursiveBacktracker RB;
    UIInfoManager UIIM;

    void Start()
    {
        UIIM = GetComponent<UIInfoManager>();
        BorderParent = new GameObject("BorderParent"); 
        PointsOnThePlane = this.GetComponent<ObjectPoints>();
        RB = new RecursiveBacktracker();
        CreateNewMaze();       
    }

    public void CreateNewMaze()
    {
        Destroy(BorderParent);
        BorderParent = new GameObject("BorderParent");

        MazeWidth = Random.Range(2,planeWidth+1);
        MazeHeight = Random.Range(2, planeHeight+1);
        UIIM.UpdateUI(MazeHeight,MazeWidth);
        CreateTheGrid();
        CellGrid = RB.GetNewMaze(Grid);

        ShowMaze();
    }

    void CreateTheGrid()  //creates grid with given width and length from the points on the plane
    {
        Grid = new List<List<Vector3>>();
        List<Vector3> OneRow = new List<Vector3>();

        for (int y = 0; y < MazeHeight; y++)
        {
            for (int x = 0; x < MazeWidth; x++)
            {
                OneRow.Add(PointsOnThePlane.GetObjectGlobalVertices()[y + x * planeHeight]); //getting the points on the plane from the upper left corner
            }
            Grid.Add(OneRow);
            OneRow = new List<Vector3>();
        }
    }

    void ShowMaze()  //Creates maz with borders as cubes
    {
        GameObject VerticalBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);  //vertically scaled cube for up and down borders
        VerticalBorder.transform.localScale = new Vector3(1.5f, 1, 0.5f);

        GameObject HorizontalBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);//horizontally scaled cube for left and right borders
        HorizontalBorder.transform.localScale = new Vector3(0.5f, 1, 1.5f);

        for (int x = 0; x < MazeWidth; x++)
        {
            for (int y = 0; y < MazeHeight; y++)
            {
                if (CellGrid[y][x].Borders.Contains(Direction.Up))
                {
                    PlaceHorizontalBorder(HorizontalBorder, CellGrid[y][x], Direction.Up);
                }
                if (CellGrid[y][x].Borders.Contains(Direction.Down))
                {
                    PlaceHorizontalBorder(HorizontalBorder, CellGrid[y][x], Direction.Down);
                }

                if (CellGrid[y][x].Borders.Contains(Direction.Right))
                {
                    PlaceVerticalBorder(VerticalBorder, CellGrid[y][x], Direction.Right);
                }
                if (CellGrid[y][x].Borders.Contains(Direction.Left))
                {
                    PlaceVerticalBorder(VerticalBorder, CellGrid[y][x], Direction.Left);
                }
            }
        }
        GameObject.Destroy(VerticalBorder); //destroy source objects
        GameObject.Destroy(HorizontalBorder);
    }

    void PlaceHorizontalBorder(GameObject border, Cell c, Direction d) //borders are put moved away from the point to suround it
    {                                                   //upper border moved up, and lower border moved down along x axis
        Instantiate(border, c.position + Vector3.right * 0.5f * ((d == Direction.Up) ? 1 : -1), Quaternion.identity, BorderParent.transform);
    }

    void PlaceVerticalBorder(GameObject border, Cell c, Direction d)
    {                                                   //left border moved left, and right border moved right along z axis
        Instantiate(border, c.position + Vector3.forward * 0.5f * ((d == Direction.Left) ? 1 : -1), Quaternion.identity, BorderParent.transform);
    }




}
