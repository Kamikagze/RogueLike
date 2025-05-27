using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundManager : MonoBehaviour
{
    public Tilemap tilemap; // ������ �� Tilemap
    public TileBase[] tiles; // ������ ������
    public Transform player; // ������ �� ������

    private Vector3Int lastPlayerCell;
    private const int drawDistance = 5; // ����� ������, � ������� ��������� �����

    private void Start()
    {
        lastPlayerCell = tilemap.WorldToCell(player.position);
        CreateTiles();
    }

    private void Update()
    {
        Vector3Int currentCell = tilemap.WorldToCell(player.position);
        if (currentCell != lastPlayerCell)
        {
            UpdateTiles(currentCell);
            lastPlayerCell = currentCell;
            RemoveFarTiles();
        }
    }

    private void CreateTiles()
    {
        Vector3Int playerCell = tilemap.WorldToCell(player.position);
        // ������� 3x3 ������� ������ ������
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                tilemap.SetTile(new Vector3Int(playerCell.x + x, playerCell.y + y, 0), GetRandomTile());
            }
        }
    }

    private void UpdateTiles(Vector3Int currentCell)
    {
        // ���������� ������ � ����� �������
        int dx = currentCell.x - lastPlayerCell.x;
        int dy = currentCell.y - lastPlayerCell.y;

        // ���� ����� ������������ ������ ��� �����
        if (dx > 0)
        {
            tilemap.SetTile(new Vector3Int(currentCell.x + 1, currentCell.y, 0), GetRandomTile());
            tilemap.SetTile(new Vector3Int(currentCell.x + 1, currentCell.y + 1, 0), GetRandomTile());
            tilemap.SetTile(new Vector3Int(currentCell.x + 1, currentCell.y - 1, 0), GetRandomTile());
        }
        else if (dx < 0)
        {
            tilemap.SetTile(new Vector3Int(currentCell.x - 1, currentCell.y, 0), GetRandomTile());
            tilemap.SetTile(new Vector3Int(currentCell.x - 1, currentCell.y + 1, 0), GetRandomTile());
            tilemap.SetTile(new Vector3Int(currentCell.x - 1, currentCell.y - 1, 0), GetRandomTile());
        }

        // ���� ����� ������������ ����� ��� ����
        if (dy > 0)
        {
            tilemap.SetTile(new Vector3Int(currentCell.x, currentCell.y + 1, 0), GetRandomTile());
            tilemap.SetTile(new Vector3Int(currentCell.x + 1, currentCell.y + 1, 0), GetRandomTile());
            tilemap.SetTile(new Vector3Int(currentCell.x - 1, currentCell.y + 1, 0), GetRandomTile());
        }
        else if (dy < 0)
        {
            tilemap.SetTile(new Vector3Int(currentCell.x, currentCell.y - 1, 0), GetRandomTile());
            tilemap.SetTile(new Vector3Int(currentCell.x + 1, currentCell.y - 1, 0), GetRandomTile());
            tilemap.SetTile(new Vector3Int(currentCell.x - 1, currentCell.y - 1, 0), GetRandomTile());
        }
    }

    private void RemoveFarTiles()
    {
        Vector3Int playerCell = tilemap.WorldToCell(player.position);

        // �������� �� ���� ������ �� Tilemap
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            // ���������, ���������� �� ����
            if (tilemap.HasTile(position))
            {
                // ��������� ���������� �� ������ �� �������� �����
                int distance = Mathf.Abs(position.x - playerCell.x) + Mathf.Abs(position.y - playerCell.y);

                // ������� �����, ������� ��������� ����� 5 ������ �� ������
                if (distance > drawDistance)
                {
                    tilemap.SetTile(position, null);
                }
            }
        }
    }

    private TileBase GetRandomTile()
    {
        if (tiles.Length == 0)
        {
            return null; // ���������� null, ���� ��� ��������� ������
        }
        int randomIndex = Random.Range(0, tiles.Length); // ���������� ��������� ������
        return tiles[randomIndex];
    }
}