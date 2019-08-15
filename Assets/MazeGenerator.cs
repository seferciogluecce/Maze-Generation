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
    int MazeWidth = 11;
    int MazeLength = 11;
    GameObject CubeParents;


    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    if (draw)
    //    {
    //        foreach(Cell c in CellGrid[0])
    //        {
    //            Gizmos.color = Color.red;

    //            Gizmos.DrawSphere(c.position, 0.25f);
    //            Gizmos.color = Color.yellow;


    //            if (c.Borders.Contains(Directions.N))
    //            {
    //                Gizmos.DrawSphere(c.position + Vector3.right*0.5f, 0.25f);

    //            }
    //            if (c.Borders.Contains(Directions.S))
    //            {
    //                Gizmos.DrawSphere(c.position + Vector3.left * 0.5f, 0.25f);

    //            }
    //            if (c.Borders.Contains(Directions.E))
    //            {
    //                Gizmos.DrawSphere(c.position + Vector3.back * 0.5f, 0.25f);

    //            }
    //            if (c.Borders.Contains(Directions.W))
    //            {
    //                Gizmos.DrawSphere(c.position + Vector3.forward * 0.5f, 0.25f);

    //            }




    //        }


    //    }



    //}

    //void OnDrawGizmos()
    //{

    //    Gizmos.color = Color.red;
    //    bool color = true;

    //    if (draw) {
    //        foreach (List<Vector3> row in Grid)
    //        {
    //            foreach (Vector3 point in row)
    //            {

    //                Gizmos.DrawSphere(point, 0.2f);
    //            }

    //            if (color)
    //            {
    //        Gizmos.color = Color.yellow;

    //            }
    //            else
    //            {
    //                Gizmos.color = Color.red;

    //            }
    //            color = !color;

    //        }
    //        //Gizmos.color = Color.yellow;
    //        //foreach (Vector3 point in points.GetObjectGlobalVertices())
    //        //{

    //        //    Gizmos.DrawSphere(point, 0.3f);
    //        //}

    //    }

    //}


    void Start()
    {

        CubeParents = new GameObject("CubesParent");
        points = this.GetComponent<ObjectPoints>();
        FillUpTheGrid();
        RecursiveBacktracker rb = new RecursiveBacktracker();
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
        for (int a = 0; a < MazeWidth; a++)
        {
            for (int b = 0; b < MazeLength; b++)
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

        for (int y = 0; y< MazeLength; y++)
        {
            for (int x = 0; x < MazeWidth; x++)
            {
                if (CellGrid[x][y].Borders.Contains(Directions.N))
                {
                    PlaceUpperBorder(sideBorder, CellGrid[x][y],Directions.N);
                }
                if (CellGrid[x][y].Borders.Contains(Directions.S))
                {
                    PlaceUpperBorder(sideBorder, CellGrid[x][y], Directions.S);
                }
                if (CellGrid[x][y].Borders.Contains(Directions.E))
                {
                    PlaceSideBorder(upBorder, CellGrid[x][y], Directions.E);
                }
                if (CellGrid[x][y].Borders.Contains(Directions.W))
                {
                    PlaceSideBorder(upBorder, CellGrid[x][y], Directions.W);
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
                OneRow.Add(points.GetObjectGlobalVertices()[a*MazeLength+b]);
            }
            Grid.Add(OneRow);
            OneRow = new List<Vector3>();

        }
   

    }

}
