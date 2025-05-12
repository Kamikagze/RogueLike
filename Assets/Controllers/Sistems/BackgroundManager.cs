using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundManager : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на Tilemap
    public TileBase[] tiles; // Массив тайлов
    public Transform player; // Ссылка на игрока

    private Vector3Int lastPlayerCell;
    private const int drawDistance = 5; // Число клеток, в которых находятся тайлы

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
        // Создаем 3x3 область вокруг игрока
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
        // Определяем старые и новые позиции
        int dx = currentCell.x - lastPlayerCell.x;
        int dy = currentCell.y - lastPlayerCell.y;

        // Если игрок переместился вправо или влево
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

        // Если игрок переместился вверх или вниз
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

        // Проходим по всем тайлам на Tilemap
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            // Проверяем, существует ли тайл
            if (tilemap.HasTile(position))
            {
                // Вычисляем расстояние от игрока до текущего тайла
                int distance = Mathf.Abs(position.x - playerCell.x) + Mathf.Abs(position.y - playerCell.y);

                // Удаляем тайлы, которые находятся далее 5 клеток от игрока
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
            return null; // Возвращаем null, если нет доступных тайлов
        }
        int randomIndex = Random.Range(0, tiles.Length); // Генерируем случайный индекс
        return tiles[randomIndex];
    }
}