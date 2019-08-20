using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Cell {  //a cell consists of its position and borders
    public List<Direction> Borders;
    public Vector3 position;
  public  Cell(Vector3 pos)
    {
        position = pos;
        Borders = new List<Direction>();
    }
}

   public enum Direction //direction's bitwise value
{
        Up = 1, //0001
        Down = 2, //0010
        Right = 4, //0100
        Left= 8   //1000
    }

public class RecursiveBacktracker 
{

    enum DirectionX // direction's x axis movement value
    {
        Right = 1,
        Left = -1,
        Up = 0,
        Down = 0
    }
    enum DirectionY //returns direction's y axis movement value
    {
        Right = 0,
        Left = 0,
        Up = -1,
        Down = 1
    }
    enum Opposite//opposite of direction's bitwise value
    {
        Right = 8,
        Left = 4,
        Up = 2,
        Down = 1
    }

    List<List<int>> Grid;
   List<List<Cell>> CellGrid;

   int GridHeight = 11;
   int GridWidth = 11;


    public List<List<Cell>> GetNewMaze(List<List<Vector3>> Maze)
    {
        SetGridBounds(Maze);
        CreateGrid(Maze);
        CarvePassagesFrom(0, 0, Grid);
        FillMazeValues();
        return CellGrid;
    }
  

    void SetGridBounds(List<List<Vector3>> Maze)
    {
        GridWidth = Maze[0].Count();
        GridHeight = Maze.Count();
    }

    void CreateGrid(List<List<Vector3>> Maze)  //CellGrid and Grid variables are initially created
    {
        Grid = new List<List<int>>();
        CellGrid = new List<List<Cell>>();
        List<Cell> cellRow = new List<Cell>();
        List<int> OneRow = new List<int>();
        for (int a = 0; a < GridHeight; a++)
        {
            for (int b = 0; b < GridWidth; b++)
            {
                cellRow.Add(new Cell(Maze[a][b]));
                OneRow.Add(0);
            }
            CellGrid.Add(cellRow);
            Grid.Add(OneRow);
            cellRow = new List<Cell>();
            OneRow = new List<int>();
        }
    }

    void CarvePassagesFrom(int cx, int cy, List<List<int>> Grid) //Implementation of recursive backtracking algorithm
    {
        List<Direction> Directions = new List<Direction>{Direction.Up,Direction.Down,Direction.Right,Direction.Left};

        Directions = Randomize(Directions);

        foreach (Direction dir in Directions) //check each direction
        {
            int nx = cx +   DirToDirectionX(dir);//translate current coordinate to drections coordinate
            int ny = cy + DirToDirectionY(dir);

            if ( (0 <= ny && ny<= GridHeight - 1) &&  (0 <= nx && nx <= GridWidth - 1 )) //if new coordinate between the bounds
            {
                if ( Grid[ny][nx] == 0) { //If the new coordinate unvisited
                Grid[cy][cx] |= (int)dir; //current direction added to current cell bitwise
                Grid[ny][nx] |= DirToOpposite(dir);//opposite of current direction added to next cell bitwise
                CarvePassagesFrom(nx, ny, Grid);//Carving the map continued from the next cell 
            }
            }
        }
    }

    void FillMazeValues()
    {
        string AsciiMapRepresentation = ""; //These are used to show the map as ascii representation and bit values of cells
        string BitwiseMapRepresentation = "";

        for (int t = 0; t < GridHeight; t++) //leftest border values 
        {
            CellGrid[t][0].Borders.Add(Direction.Left);

        }
        for (int t = 0; t < GridWidth; t++)//uppest border values 
        {
            CellGrid[0][t].Borders.Add(Direction.Up);

        }
        for (int y = 0; y < GridHeight; y++)
        {
             
             AsciiMapRepresentation += "\n|";
             BitwiseMapRepresentation += "\n";

            for (int x = 0; x < GridWidth; x++)
            {

                BitwiseMapRepresentation += "(" + Grid[y][x] + ")";
                AsciiMapRepresentation += (((Grid[y][x] & (int)Direction.Down) != 0)? " ":"_") ;
                
                if ((Grid[y][x] & (int)Direction.Down) == 0) //there is a wall in the downside of the map
                {
                    CellGrid[y][x].Borders.Add(Direction.Down);
                }
                if ((Grid[y][x] & (int)Direction.Right) == 0)//There is a wall in the rightside of the cell
                {                
                    CellGrid[y][x].Borders.Add(Direction.Right);
                    AsciiMapRepresentation += "|"  ;
                }
            }           
        }            
 
            Debug.Log(AsciiMapRepresentation);  //Mapps are printed to console
            Debug.Log(BitwiseMapRepresentation);     
    }
    int DirToDirectionX(Direction dir)//returns direction's x axis movement value
    {

        switch (dir)
        {
            case Direction.Up:
                return (int)DirectionX.Up;
            case Direction.Down:
                return (int)DirectionX.Down;
            case Direction.Left:
                return (int)DirectionX.Left;
            case Direction.Right:
                return (int)DirectionX.Right;
        }
        return 0;
    }
    int DirToDirectionY(Direction dir) //returns direction's y axis movement value
    {
        switch (dir)
        {
            case Direction.Up:
                return (int)DirectionY.Up;
            case Direction.Down:
                return (int)DirectionY.Down;
            case Direction.Left:
                return (int)DirectionY.Left;
            case Direction.Right:
                return (int)DirectionY.Right;
        }
        return 0;
    }
    int DirToOpposite(Direction dir) //returns direction's opposite direction value
    {
        switch (dir)
        {
            case Direction.Up:
                return (int)Opposite.Up;
            case Direction.Down:
                return (int)Opposite.Down;
            case Direction.Left:
                return (int)Opposite.Left;
            case Direction.Right:
                return (int)Opposite.Right;
        }
        return 0;
    }

    List<Direction> Randomize(List<Direction> Directions)
    {
        for (int i = 0; i < Directions.Count; i++)
        {
            Direction temp = Directions[i];
            int randomIdx = UnityEngine.Random.Range(i, Directions.Count);
            Directions[i] = Directions[randomIdx];
            Directions[randomIdx] = temp;
        }
        return Directions;
    }

}