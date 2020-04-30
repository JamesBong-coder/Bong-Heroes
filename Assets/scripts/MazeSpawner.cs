using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    //public static MazeSpawner Instance { get; set; }

    public GameObject CellPrefab;
    public GameObject ExitPrefab;
    public GameObject Door;
    private int Width;
    private int Height;
    public GameObject[] Weapons;
    public GameObject[] Enemies;
    private int EnemiesQuantity;
    public GameObject BulletsPrefab;
    private int BulletQuantity;
    public GameObject WalkPointPrefab;
    private int WalkPointQuantity;
    public GameObject HealPrefab;
    private int HealQuantity;
    public GameObject Scrimmer;
    private int ScrimmerQuantity;
    public static int Complexity;

    void Start()
    {
        SetComplexity(Complexity);
        MazeGenerator generator = new MazeGenerator(Width, Height);
        MazeGeneratorCell[,] maze = generator.GenerateMaze();

        for(int i=0; i<maze.GetLength(0); i++)
        {
            for(int j=0; j<maze.GetLength(1); j++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(i * 10, 0, j * 10), Quaternion.identity).GetComponent<Cell>();

                c.WallLeft.SetActive(maze[i, j].LeftWall);
                c.WallBottom.SetActive(maze[i, j].BottomWall);
                c.floor.SetActive(maze[i, j].floor);
                if (i == 0 && j == 0)
                {
                    Instantiate(Door, new Vector3(0, 5, -5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                    c.WallBottom.SetActive(false);
                }
                if (maze[i, j].Exit) {
                    Instantiate(ExitPrefab, new Vector3(i * 10, 0, j * 10), Quaternion.identity);
                    maze[i, j].Occupied = true;
                }
                
            }
        }

        for (int i = 0; i < 4; i++)
            SpawnGameObject(Weapons[i], 1, maze);
        for (int i = 0; i < 4; i++)
            SpawnGameObject(Enemies[i], EnemiesQuantity, maze);
        SpawnGameObject(BulletsPrefab, BulletQuantity, maze);
        SpawnGameObject(WalkPointPrefab, WalkPointQuantity, maze);
        SpawnGameObject(HealPrefab, HealQuantity, maze);
        SpawnGameObject(Scrimmer, ScrimmerQuantity, maze);
    }
    public int[] GetCell()
    {
        int[] WeapCor = new int[2];
        WeapCor[0] = Random.Range(1, Width - 1);
        WeapCor[1] = Random.Range(1, Height - 1);
        return WeapCor;
    }

    public void SpawnGameObject(GameObject obj, int quantity, MazeGeneratorCell[,] maze)
    {
        for (int i = 0; i < quantity; i++)
        {
            int[] WeapCor;
            do
            {
                WeapCor = GetCell();
            } while (maze[WeapCor[0], WeapCor[1]].Occupied);
            Instantiate(obj, new Vector3(WeapCor[0] * 10, 0.63f, WeapCor[1] * 10), Quaternion.identity); 
            maze[WeapCor[0], WeapCor[1]].Occupied = true;
        }
    }
    public void SetComplexity(int com)
    {
        switch (com)
        {
            case 1:
                Width = 13;
                Height = 7;
                BulletQuantity = 4;
                WalkPointQuantity = 4;
                HealQuantity = 4;
                EnemiesQuantity = 2;
                ScrimmerQuantity = 1;
                break;
            case 2:
                Width = 17;
                Height = 11;
                BulletQuantity = 4;
                WalkPointQuantity = 4;
                HealQuantity = 4;
                EnemiesQuantity = 4;
                ScrimmerQuantity = 2;

                break;
            case 3:
                Width = 25;
                Height = 15;
                BulletQuantity = 5;
                WalkPointQuantity = 5;
                HealQuantity = 5;
                EnemiesQuantity = 5;
                ScrimmerQuantity = 3;
                break;
            default:
                break;
        }
    }
}
