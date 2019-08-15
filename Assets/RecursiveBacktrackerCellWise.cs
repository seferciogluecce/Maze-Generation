using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

//UNFINISHED but tried anyway

/*public class Cell {  //a cell consists of its position and borders
    public List<Direction> Borders;
    public Vector3 position;
    public bool visited;
  public  Cell(Vector3 pos)
    {
        visited = false;
        position = pos;
        Borders = new List<Direction>();
    }
}

   public enum Direction //direction's bitwise value
{
        N = 1, //0001
        S = 2, //0010
        E = 4, //0100
        W= 8   //1000
    }*/

public class RecursiveBacktrackerCellWise
{
    enum DX // direction's x axis movement value
    {
        E = 1,
        W = -1,
        N = 0,
        S = 0
    }
    enum DY //returns direction's y axis movement value
    {
        E = 0,
        W = 0,
        N = -1,
        S = 1
    }
    enum OPPOSITE//opposite of direction's bitwise value
    {
        E = 8,
        W = 4,
        N = 2,
        S = 1
    }

    List<List<int>> Grid = new List<List<int>>();
   List<List<Cell>> CellGrid = new List<List<Cell>>();

   int GridHeight = 11;
   int GridWidth = 11;


    public List<List<Cell>> GetNewMaze(List<List<Vector3>> Maze)
    {

        SetGridBounds(Maze);
        CreateCellGrid(Maze);
        CarvePassagesFromCell(0, 0, CellGrid);
        AddBorders();
        return CellGrid;
    }
  

    void SetGridBounds(List<List<Vector3>> Maze)
    {
        GridWidth = Maze[0].Count();
        GridHeight = Maze.Count();
    }

    void CreateCellGrid(List<List<Vector3>> Maze)  //CellGrid and Grid veriables are initially created
    {
        List<Cell> cellRow = new List<Cell>();
        for (int a = 0; a < GridHeight; a++)
        {
            for (int b = 0; b < GridWidth; b++)
            {
                cellRow.Add(new Cell(Maze[a][b]));
            }
            CellGrid.Add(cellRow);
            cellRow = new List<Cell>();
        }
    }

    void CarvePassagesFromCell(int cx, int cy, List<List<Cell>> Grid)
    {
        List<Direction> Directions = new List<Direction> { Direction.Up, Direction.Down, Direction.Right, Direction.Left };

        Directions = Directions.OrderBy(a => Guid.NewGuid()).ToList();  //Randomize directions
        //Grid[cy][cx].visited = true;

        foreach (Direction dir in Directions) //check each direction
        {
            int nx = cx + DirToDX(dir);//translate current coordinate to drections coordinate
            int ny = cy + DirToDY(dir);

            if ((0 <= ny && ny <= GridHeight - 1) && (0 <= nx && nx <= GridWidth - 1)) //if new coordinate between the bounds
            {
                //if (!Grid[ny][nx].visited )
                { //If the new coordinate unvisited
                    Grid[cy][cx].Borders.Add(dir); //current direction added to current cell 
                    Grid[ny][nx].Borders.Add(DirToOppositeCell(dir));// DirToOpposite(dir);//opposite of current direction added to next cell bitwise
                    CarvePassagesFromCell(nx, ny, Grid);//Carving the map continued from the next cell 
                }
            }
        }
    }

    void AddBorders()
    {
        for (int t = 0; t < GridHeight; t++) //left border values 
        {
            CellGrid[t][0].Borders.Add(Direction.Left);

        }
        for (int t = 0; t < GridWidth; t++)
        {
            CellGrid[0][t].Borders.Add(Direction.Up);

        }

        for (int t = 0; t < GridHeight; t++) //left border values 
        {
            CellGrid[t][GridHeight-1].Borders.Add(Direction.Right);

        }
        for (int t = 0; t < GridWidth; t++)
        {
            CellGrid[GridWidth-1][t].Borders.Add(Direction.Down);

        }
    }
    int DirToDX(Direction dir)//returns direction's x axis movement value
    {

        switch (dir)
        {
            case Direction.Up:
                return (int)DX.N;
            case Direction.Down:
                return (int)DX.S;
            case Direction.Left:
                return (int)DX.W;
            case Direction.Right:
                return (int)DX.E;
        }
        return 0;
    }

    int DirToDY(Direction dir) //returns direction's y axis movement value
    {
        switch (dir)
        {
            case Direction.Up:
                return (int)DY.N;
            case Direction.Down:
                return (int)DY.S;
            case Direction.Left:
                return (int)DY.W;
            case Direction.Right:
                return (int)DY.E;
        }
        return 0;
    }

    Direction DirToOppositeCell(Direction dir) //returns direction's opposite direction value
    {
        switch (dir)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
        }
        return Direction.Up;
    }

}