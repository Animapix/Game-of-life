
class CellularAutomata : Grid
{
    private Random rnd = new Random();

    public CellularAutomata(int columns = 10, int rows = 10, int cellSize = 20) : base(columns, rows, cellSize)
    {}

    public void Update()
    {
        bool[,] nextGrid = new bool[columns, rows];

        for (int column = 0; column < columns; column++)
        {
            for (int row = 0; row < rows; row++)
            {
                int neighborhoodCount = MooreNeighborhoodCount(column, row);
                if (GetCell(column, row))
                {
                    if (neighborhoodCount == 2 || neighborhoodCount == 3) nextGrid[column, row] = true;
                    else nextGrid[column, row] = false;
                }
                else
                {
                    if (neighborhoodCount == 3) nextGrid[column, row] = true;
                }
            }
        }

        copyGrid(nextGrid);
    }

    private void copyGrid(bool[,] nextGrid)
    {
        for (int column = 0; column < columns; column++)
        {
            for (int row = 0; row < rows; row++)
            {
                cells[column, row] = nextGrid[column, row];
            }
        }
    }

    public void Randomize()
    {
        for (int column = 0; column < columns; column++)
        {
            for (int row = 0; row < rows; row++)
            {
                SetCell(column, row, rnd.Next(100) <= 30);
            }
        }
    }

    public int MooreNeighborhoodCount(int column, int row)
    {
        bool[] neighborhood = MooreNeighborhood(column, row);
        int count = 0;
        foreach (bool cell in neighborhood) if (cell) count++;
        return count;
    }
}