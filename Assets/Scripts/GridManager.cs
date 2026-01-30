using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public Transform[,] grid;
    public GameObject blockPrefab;
    public ThemeManager themeManager;

    void Start()
    {
        grid = new Transform[width, height];
    }

    public bool CanPlaceBlock(Vector2Int[] shape, Vector2Int pos)
    {
        foreach (var cell in shape)
        {
            int x = pos.x + cell.x;
            int y = pos.y + cell.y;

            if (x < 0 || x >= width || y < 0 || y >= height)
                return false;

            if (grid[x, y] != null)
                return false;
        }
        return true;
    }

    public void PlaceBlock(Vector2Int[] shape, Vector2Int pos)
    {
        foreach (var cell in shape)
        {
            int x = pos.x + cell.x;
            int y = pos.y + cell.y;

            GameObject block = Instantiate(blockPrefab, new Vector3(x, y, 0), Quaternion.identity);
            block.GetComponent<SpriteRenderer>().sprite = themeManager.GetCurrentSprite();
            grid[x, y] = block.transform;
        }

        CheckLines();
    }

    void CheckLines()
    {
        for (int y = 0; y < height; y++)
        {
            bool full = true;
            for (int x = 0; x < width; x++)
                if (grid[x, y] == null) full = false;

            if (full) ClearRow(y);
        }

        for (int x = 0; x < width; x++)
        {
            bool full = true;
            for (int y = 0; y < height; y++)
                if (grid[x, y] == null) full = false;

            if (full) ClearColumn(x);
        }

        if (IsBoardEmpty())
            themeManager.SwitchTheme();
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    void ClearColumn(int x)
    {
        for (int y = 0; y < height; y++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    bool IsBoardEmpty()
    {
        foreach (var cell in grid)
            if (cell != null) return false;
        return true;
    }
}
