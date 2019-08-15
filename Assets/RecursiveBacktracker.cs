using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Cell {

    public List<Directions> Borders;
    public int x;
    public int y;
    public Vector3 position;

  public  Cell(Vector3 pos)
    {
        position = pos;
        Borders = new List<Directions>();
    }


}

   public enum Directions
    {
        N = 1,
        S = 2,
        E = 4,       
        W= 8
    }
public class RecursiveBacktracker 
{

   List<List<int>> grid = new List<List<int>>();
   List<List<Cell>> gridCell = new List<List<Cell>>();
   int GridHeight = 11;
   int GridWidth = 11;


    enum DX
    {
        E =1,
        W = -1,
        N = 0,
        S = 0
    }
    enum DY
    {
        E=0,
        W=0,
        N = -1,
        S = 1
    }
    enum OPPOSITE
    {
        E = 8,
        W = 4,
        N = 2,
        S = 1
    }


    void carve_passages_from(int cx, int cy, List<List<int>> grid)
    {
        List<Directions> Direction = new List<Directions>{Directions.N,Directions.S,Directions.E,Directions.W};

        Direction = Direction.OrderBy(a => Guid.NewGuid()).ToList();

        foreach (Directions dir in Direction)
        {
            int nx = cx +   DirToDX(dir);
            int ny = cy + DirToDY(dir);

            if ( (0 <= ny && ny<= GridHeight - 1) &&  (0 <= nx && nx <= GridWidth - 1 ))
            {
                if ( grid[ny][nx] == 0) { 
                grid[cy][cx] |= (int)dir;
                grid[ny][nx] |= DirToOpposite(dir);
                carve_passages_from(nx, ny, grid);
            }
            }
        }
    }


    void FillGrid(List<List<Vector3>> Maze)
    {
        List<Cell> cellRow = new List<Cell>();
        List<int> OneRow = new List<int>();
        for (int a = 0; a < GridHeight; a++)
        {
            for (int b = 0; b < GridWidth; b++)
            {
                cellRow.Add(new Cell(Maze[a][b]));
                OneRow.Add(0);
            }
            gridCell.Add(cellRow);
            grid.Add(OneRow);
            cellRow = new List<Cell>();
            OneRow = new List<int>();
        }
    }


    public List<List<Cell>> GiveMeMyMaze(List<List<Vector3>> Maze)
    {

        GridWidth = Maze[0].Count();
        GridHeight = Maze.Count();

        FillGrid(Maze);
        carve_passages_from(0,0,grid);
        ShowTheMaze();
        return gridCell;

    }
    void ShowTheMaze()
    {
        string a = "";
        string b = "";

        for (int t = 0; t < GridHeight; t++)
        {
            gridCell[t][0].Borders.Add(Directions.W);

        }
        for (int t = 0; t < GridWidth; t++)
        {
            gridCell[0][t].Borders.Add(Directions.N);

        }
        for (int y = 0; y < GridHeight; y++)
        {
             int z = 0;
             a += "\n|";
             b += "\n";

            for (int x = 0; x < GridWidth; x++)
            {

                b += "(" + grid[y][x] + ")";
                a += (((grid[y][x] & (int)Directions.S) != 0)? " ":"_") ;
                z++;


                if ((grid[y][x] & (int)Directions.S) == 0)
                {
                    gridCell[y][x].Borders.Add(Directions.S);
                }
                    if ((grid[y][x] & (int)Directions.E) == 0)//sağında yol var
                {                
                    gridCell[y][x].Borders.Add(Directions.E);
                    a += "|"  ;
                }
            }
            a += "  " + z.ToString();







        }            
 
Debug.Log(a);
Debug.Log(b);

       
    }

    int DirToDX(Directions dir)
    {

        switch (dir)
        {
            case Directions.N:
                return (int)DX.N;
            case Directions.S:
                return (int)DX.S;
            case Directions.W:
                return (int)DX.W;
            case Directions.E:
                return (int)DX.E;
        }
        return 0;
    }

    int DirToDY(Directions dir)
    {
        switch (dir)
        {
            case Directions.N:
                return (int)DY.N;
            case Directions.S:
                return (int)DY.S;
            case Directions.W:
                return (int)DY.W;
            case Directions.E:
                return (int)DY.E;
        }
        return 0;
    }

    int DirToOpposite(Directions dir)
    {
        switch (dir)
        {
            case Directions.N:
                return (int)OPPOSITE.N;
            case Directions.S:
                return (int)OPPOSITE.S;
            case Directions.W:
                return (int)OPPOSITE.W;
            case Directions.E:
                return (int)OPPOSITE.E;
        }
        return 0;
    }

}