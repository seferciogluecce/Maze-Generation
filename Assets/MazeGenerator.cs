using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    List<List<Vector3>> Grid = new List<List<Vector3>>();
    List<List<Cell>> CellGrid = new List<List<Cell>>();
    ObjectPoints points;
    bool draw = false;
    int MazeWidth = 5;
    int MazeLength = 5;

    int PlaneWidth = 11;
    int planeHeight = 11;
    GameObject CubeParents;




    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        bool color = true;

        if (draw)
        {
            foreach (List<Vector3> row in Grid)
            {
                foreach (Vector3 point in row)
                {

                    Gizmos.DrawSphere(point, 0.2f);
                }

                if (color)
                {
                    Gizmos.color = Color.yellow;

                }
                else
                {
                    Gizmos.color = Color.red;

                }
                color = !color;

            }
            //Gizmos.color = Color.yellow;
            //foreach (Vector3 point in points.GetObjectGlobalVertices())
            //{

            //    Gizmos.DrawSphere(point, 0.3f);
            //}

        }

    }


    void Start()
    {

        CubeParents = new GameObject("CubesParent");
        points = this.GetComponent<ObjectPoints>();
        FillUpTheGrid();
        RecursiveBacktracker rb = new RecursiveBacktracker();
        Debug.Log(Grid.Count + "  111  " + Grid[0].Count);
        CellGrid = rb.GiveMeMyMaze(Grid);
        draw = true;

        //FillTheGridWithCubes();
        FillBorders();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillTheGridWithCubes()
    {
        for (int a = 0; a <MazeLength ; a++)
        {
            for (int b = 0; b < MazeWidth; b++)
            {

                if(Grid[a][b]!= Vector3.zero)
                { 
               GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newCube.transform.position = Grid[a][b];
                }
            }
        }
     }

    void FillBorders()
    {
        GameObject upBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        upBorder.transform.localScale = new Vector3(1.5f, 1, 0.5f);

        GameObject sideBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sideBorder.transform.localScale = new Vector3(0.5f, 1, 1.5f);
            for (int x = 0; x < MazeWidth; x++)

        {        for (int y = 0; y< MazeLength; y++)

            {
                if (CellGrid[y][x].Borders.Contains(Directions.N))
                {
                    PlaceUpperBorder(sideBorder, CellGrid[y][x],Directions.N);
                }
                if (CellGrid[y][x].Borders.Contains(Directions.S))
                {
                    PlaceUpperBorder(sideBorder, CellGrid[y][x], Directions.S);
                }
                if (CellGrid[y][x].Borders.Contains(Directions.E))
                {
                    PlaceSideBorder(upBorder, CellGrid[y][x], Directions.E);
                }
                if (CellGrid[y][x].Borders.Contains(Directions.W))
                {
                    PlaceSideBorder(upBorder, CellGrid[y][x], Directions.W);
                }

            }
        }
        GameObject.Destroy(upBorder);
        GameObject.Destroy(sideBorder);
    }

    void PlaceUpperBorder(GameObject border, Cell c, Directions d)
    {
        if (d == Directions.N)
        {
            Instantiate(border, c.position + Vector3.right*0.5f, Quaternion.identity,CubeParents.transform);
        }

        else
        {
            Instantiate(border, c.position + Vector3.left * 0.5f, Quaternion.identity, CubeParents.transform);

        }

    }

    void PlaceSideBorder(GameObject border, Cell c, Directions d)
    {
        if (d == Directions.W)
        {
            Instantiate(border, c.position+  Vector3.forward * 0.5f , Quaternion.identity, CubeParents.transform);
        }

        else
        {
            Instantiate(border, c.position+Vector3.back * 0.5f, Quaternion.identity, CubeParents.transform);
        }

    }


    void FillUpTheGrid()
    {
        List<Vector3> OneRow = new List<Vector3>();

        for (int a = 0; a < MazeLength; a++)
        {
            for (int b = 0; b < MazeWidth; b++)
            {
                OneRow.Add(points.GetObjectGlobalVertices()[a  + b* planeHeight]);
            }
            Grid.Add(OneRow);
            OneRow = new List<Vector3>();

        }
   

    }

}
