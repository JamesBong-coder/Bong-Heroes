using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool LeftWall = true;
    public bool BottomWall = true;
    public bool floor = true;
    public bool Occupied = false;

    public bool visited = false;
    public int DistanceFromStart;
    public bool Exit = false;
}

public class MazeGenerator
{
    public int Width; //Высота и ширина лабиринта
    public int Height;
    
    public MazeGenerator(int Width, int Height)
    {
        this.Width = Width;
        this.Height = Height;
    }


    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[Width, Height];
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = new MazeGeneratorCell { X = i, Y = j };
            }
        }
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, Height - 1].LeftWall = false;
            maze[x, Height - 1].floor = false;
        }
        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[Width - 1, y].BottomWall = false;
            maze[Width - 1, y].floor = false;
        }



        RemoveWallWithBacktracker(maze);
        SetPlaceExit(maze);

        return maze;
    }


    //в данной функции описывается алгоритм генерации лабиринта, начиная с начальной точки мы двигаемся в случайном направлении которое еще не посещали и сносим стены
    //пока не зайдем в тупик, далее мы возвращаемся обратно по созданной нами дороге до места где можно строить лабиринт дальше, цикл заканчивается когда мы
    //возвращаемся в начальную точку
    public void RemoveWallWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.visited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> UnvisitedCell = new List<MazeGeneratorCell>();
            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].visited) UnvisitedCell.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].visited) UnvisitedCell.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].visited) UnvisitedCell.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y+1].visited) UnvisitedCell.Add(maze[x, y+1]);

            if (UnvisitedCell.Count > 0)
            {
                MazeGeneratorCell choosen = UnvisitedCell[Random.Range(0, UnvisitedCell.Count)];
                RemoveWall(current, choosen);
                choosen.visited = true;
                stack.Push(choosen);
                choosen.DistanceFromStart = stack.Count;
                current = choosen;

            }
            else
            {
                current = stack.Pop();
            }


        } while (stack.Count > 0);
    }
    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if(a.X == b.X)
        {
            if (a.Y > b.Y) a.BottomWall = false;
            else b.BottomWall = false;

        }
        else
        {
            if (a.X > b.X) a.LeftWall = false;
            else b.LeftWall = false;
        }
    }

    private void SetPlaceExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell Exit = maze[0, 0];

        for(int i=0; i<maze.GetLength(0); i++)
        {
            for(int j=0; j<maze.GetLength(1); j++)
            {
                if (Exit.DistanceFromStart < maze[i, j].DistanceFromStart) Exit = maze[i, j];
            }
        }
        Exit.Exit = true;
    }
}
