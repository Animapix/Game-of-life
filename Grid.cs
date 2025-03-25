using System.Numerics;

class Grid
{
    public readonly int columns;
    public readonly int rows;
    public readonly int cellSize;

    protected bool[,] cells;

    public Grid(int columns = 10, int rows = 10, int cellSize = 20)
    {
        this.rows = rows;
        this.columns = columns;
        this.cellSize = cellSize;
        cells = new bool[columns, rows];
    }

    public bool GetCell(int column, int row)
    {
        if (column < 0 || column >= columns || row < 0 || row >= rows) return false;
        return cells[column, row];
    }

    public void SetCell(int column, int row, bool value)
    {
        if (column < 0 || column >= columns || row < 0 || row >= rows) return;
        cells[column, row] = value;
    }

    public Vector2 ToWorld(int column, int row)
    {
        return new Vector2(column * cellSize, row * cellSize);
    }

    public (int column, int row) ToGrid(Vector2 position)
    {
        return ((int)(position.X / cellSize), (int)(position.Y / cellSize));
    }

    public bool[] MooreNeighborhood(int column, int row)
    {
        bool[] neighborhood = new bool[8];
        neighborhood[0] = GetCell(column - 1, row - 1);
        neighborhood[1] = GetCell(column, row - 1);
        neighborhood[2] = GetCell(column + 1, row - 1);
        neighborhood[3] = GetCell(column - 1, row);
        neighborhood[4] = GetCell(column + 1, row);
        neighborhood[5] = GetCell(column - 1, row + 1);
        neighborhood[6] = GetCell(column, row + 1);
        neighborhood[7] = GetCell(column + 1, row + 1);
        return neighborhood;
    }
}
