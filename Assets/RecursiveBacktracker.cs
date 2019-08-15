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

   List<Vector3> Grid;
   List<List<int>> grid = new List<List<int>>();
   List<List<Cell>> gridCell = new List<List<Cell>>();
    int GridLength = 11;
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


            if ( (0 <= ny && ny<= GridLength - 1) &&  (0 <= nx && nx <= GridWidth - 1 ))
            {
//                Debug.Log(ny + " " + nx);
                if ( grid[nx][ny] == 0) { 
                grid[cx][cy] |= (int)dir;
                grid[nx][ny] |= DirToOpposite(dir);
                carve_passages_from(nx, ny, grid);
            }
            }
        }


    }


    void FillGrid(List<List<Vector3>> Maze)
    {
        List<Cell> cellRow = new List<Cell>();
        List<int> OneRow = new List<int>();
        for (int a = 0; a < GridLength; a++)
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

        Debug.Log(grid.Count + " " + grid[0].Count);
        Debug.Log(gridCell.Count + " 333 " + gridCell[0].Count);
    }
    List<List<Vector3>>  ConvertToVector(List<List<int>> grid, List<List<Vector3>> Maze    )
    {
        for (int a = 0; a < GridLength; a++)
        {
            for (int b = 0; b < GridWidth; b++)
            {
               if(grid[a][b] == 0)
                {
                    Maze[a][b] = Vector3.zero;
                }
            }
            
        }
        return Maze;
    }


    public List<List<Cell>> GiveMeMyMaze(List<List<Vector3>> Maze)
    {

        GridWidth = Maze[0].Count();
        GridLength = Maze.Count();

        FillGrid(Maze);
        carve_passages_from(0,0,grid);
        ShowTheMaze();
        // return ConvertToVector( grid, Maze    );
        return gridCell;

    }
    void ShowTheMaze()
    {
        string a = "";
        string b = "";

        for(int t = 0; t < GridWidth; t++)
        {


        }
        for (int t = 0; t < GridWidth; t++)
        {
            gridCell[t][0].Borders.Add(Directions.N);

        }
        for (int t = 0; t < GridLength; t++)
        {
            gridCell[0][t].Borders.Add(Directions.W);

        }
        for (int y = 0; y < GridLength; y++)
        {
            int z = 0;
             a += "\n|";
             b += "\n";

            for (int x = 0; x < GridWidth; x++)
            {

                b += "(" + grid[x][y] + ")";
                a += (((grid[x][y] & (int)Directions.S) != 0)? " ":"_") ;

                if ((grid[x][y] & (int)Directions.S) == 0)
                    gridCell[x][y].Borders.Add(Directions.S);


                z++;
                    if ((grid[x][y] & (int)Directions.E) != 0)//sağında yol var
                {
                   // a += ((((grid[x][y] | grid[x+1][y]) & (int)Directions.S ) != 0) ? " " : "_");
                   // z++;
                }
                else
                {
                    gridCell[x][y].Borders.Add(Directions.E);

                    a += "|"  ;
                }
            }
            a += "  " + z.ToString();







        }            string map = "";
 
Debug.Log(a);
Debug.Log(b);

        for (int y = 0; y < GridLength; y++)
            {

                for (int x = 0; x < GridWidth; x++)
                {

                    //if(gridCell[x][y].Borders.Contains())




                }
            }

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

//https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum