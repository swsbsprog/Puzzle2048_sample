using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
	public static TileManager instance;
	public MoveBox[,] grid = new MoveBox[4,4];
    public int max = 3;
    public MoveBox tile;
	public int randomSeed = 0;
    private void Awake()
    {
		instance = this;
		if (randomSeed != 0)
			Random.InitState(randomSeed);

		Spawn();
        Spawn();
    }

    void Spawn()
    {
        int x, y;
        do 
        {
            x = Random.Range(0, max + 1);
            y = Random.Range(0, max + 1);
        } while (grid[x, y] != null);

        int startValue = 2;

		var newTile = Instantiate(tile);
		newTile.Move(x, y, false);
		newTile.SetNumber(startValue);
    }

	public int maxMove = 4;
	public void Move(Direction dir)
    {
        print("Move: " + dir);
		Vector2Int vector = Vector2Int.zero;
		int[] xArray = { 0, 1, 2, 3 };
		int[] yArray = { 0, 1, 2, 3 };
		switch (dir)
		{
			case Direction.Up:
				vector = Vector2Int.up;
				System.Array.Reverse(yArray);
				break;
			case Direction.Down:
				vector = Vector2Int.down;
				break;
			case Direction.Left:
				vector = Vector2Int.left;
				break;
			case Direction.Right:
				vector = Vector2Int.right;
				System.Array.Reverse(xArray);
				break;
		}

		bool moved = false;
		foreach (int x in xArray)
		{
			foreach (int y in yArray)
			{
				if (grid[x, y] != null)
				{
					Vector2Int previousPos;
					Vector2Int nextPos = new Vector2Int(x, y);
					do
					{
						previousPos = nextPos;
						nextPos = new Vector2Int(previousPos.x + vector.x, previousPos.y + vector.y);
					} while (IsInArea(nextPos) && grid[nextPos.x, nextPos.y] == null);

					int nextX = nextPos.x; int nextY = nextPos.y;

					// 이동 방향의 블럭과 값이 같을때
					if (IsInArea(nextPos) && grid[nextX, nextY].number == grid[x, y].number)
					{
						grid[x, y].SetNumber(grid[x, y].number * 2);
						Destroy(grid[nextX, nextY].gameObject);
						grid[x, y].Move(nextX, nextY); 
                        moved = true;
                    }
					else
					{
						// 값이 다를때 
						if (x != previousPos.x || y != previousPos.y)
						{
							grid[x, y].Move(previousPos.x, previousPos.y);
							moved = true;
						}
					}
				}
			}
		}

		if(moved)
			Spawn();
	}

	bool IsInArea(Vector2 coord)
	{
		return 0 <= coord.x && coord.x <= 3 && 0 <= coord.y && coord.y <= 3;
	}
}