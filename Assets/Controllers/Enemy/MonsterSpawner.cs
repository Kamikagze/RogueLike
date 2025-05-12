
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour, ITimeController
{
    public GameObject monsterPrefab; // Префаб монстра
    public Transform player; // Ссылка на объект игрока
    public DamageAbillController damageAbillController; // Ссылка на счетчик урона
                                                        // public SpriteRenderer enemySpRender;
    [SerializeField] private float spawnDelay = 0.45f; // Задержка между спавном
    [SerializeField] private float multiplicatorSpawnDelay = 0.9f;
    [SerializeField] private float radius = 7f; // Радиус спавна
    [SerializeField] FounderOfEnemies founderOfEnemies;
    private System.Random random = new System.Random(); // Экземпляр System.Random для случаев использования

    private bool canSpawn = true;
    private float currentTime = 0;

    private void OnEnable()
    {
        TimeManager.OnUpgrade += SpawnDelay;
    }


    Vector2 GetRandomPointOnArc()
    {
        // Генерируем случайный угол от -135 до 135 градусов для вращения дуги
        float randomAngle = (float)(random.NextDouble() * 270) - 135;

        // Получаем текущий угол игрока
        float playerAngle = player.eulerAngles.z;

        // Вычисляем окончательный угол, добавляя угол игрока к случайному углу
        float finalAngle = playerAngle + randomAngle;

        // Конвертируем угол в радианы
        float angleInRadians = Mathf.Deg2Rad * finalAngle;

        // Вычисляем координаты x и y
        float x = radius * Mathf.Cos(angleInRadians);
        float y = radius * Mathf.Sin(angleInRadians);

        return new Vector2(player.position.x + x, player.position.y + y);
    }

    private void SpawnMonster()
    {
        // Генерация случайного значения для переворота по X
        bool flipX = random.NextDouble() < 0.5; // 50% вероятность

        // Спавн монстра
        GameObject enemy = Instantiate(monsterPrefab, GetRandomPointOnArc(), Quaternion.identity);

        
        // Применение переворота по X, если нужно
        if (flipX)
        {
            enemy.transform.localScale = new Vector3(-1, 1, 1); // Установка LocalScale -1 по X
        }

        // Получение компонента EnemyBehaviour
        EnemyBehaviour enemyComponent = enemy.GetComponent<EnemyBehaviour>();
        Transform enemyTransform = enemy.transform;
        founderOfEnemies.GetNewEnemyInCollection(enemyTransform);
        // Передача ссылки на объект игрока
        enemyComponent.player = player;
        enemyComponent.damageAbillController = damageAbillController;

    }

    public void Timerred(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentTime >= spawnDelay)

        {
            SpawnMonster();
            currentTime = 0;
        }
    }
    private void SpawnDelay()
    {
        spawnDelay *= multiplicatorSpawnDelay;
    }
    private void OnDisable()
    {
        TimeManager.OnUpgrade -= SpawnDelay;
    }
}
