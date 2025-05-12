
using UnityEngine;

public class Eruption : AbstractAbill
{
    [SerializeField] private StatsHolder statsHolder; // Статус держателя
    [SerializeField] private GameObject[] eruptions; // Массив объектов взрывов
    [SerializeField] private float duration = 5f; // Длительность действия способности
    [SerializeField] private float cooldown = 10f; // Время перезарядки

    protected override float Duration => duration;

    //это возьмем как базу для последующих способностей
    private float timerForNextExplosion; // Таймер до следующего взрыва
    private int nextExplosionIndex; // Индекс следующего взрыва

    

    private void Start()
    {
        cooldownTime = cooldown; // Устанавливаем время перезарядки
        
        Activate(); // Активируем способность сразу при старте
    }

    protected override void ActionOfAbill()
    {
        Starting();
    }


    private void Starting()
    {
        if (isInWork) return; // Проверяем, если способность уже активна

        // Перемешиваем массив перед активацией
        Shuffle(eruptions);

        // Активация первых 7 взрывов
        for (int i = 0; i < 7 && i < eruptions.Length; i++)
        {
            eruptions[i].SetActive(true);
        }

        
       
        timerForNextExplosion = 0; // Сбрасываем таймер для следующего взрыва
        nextExplosionIndex = 7; // Устанавливаем индекс следующего взрыва (8-й элемент)
    }
    protected override void DurationPartOfAbill(float deltaTime)
    {
                             
        timerForNextExplosion += deltaTime;

        // Если время для следующего взрыва прошло
        if (timerForNextExplosion >= 0.07f)
        {
            ActivateNextExplosion(); // Активируем следующий взрыв
            timerForNextExplosion = 0f; // Сбрасываем таймер
        }
    }
    

    private void ActivateNextExplosion()
    {
        if (nextExplosionIndex < eruptions.Length)
        {
            // Активируем взрыв по текущему индексу
            eruptions[nextExplosionIndex].SetActive(true);
        }
        else
        {
            // Если достигли конца массива, запускаем цикл
            nextExplosionIndex = 0;
            eruptions[nextExplosionIndex].SetActive(true); // Активируем первый элемент
        }

        // Увеличиваем индекс для следующего взрыва
        nextExplosionIndex++;
    }


    public override void CooldownReduction()
    {
        cooldown *= statsHolder.CooldownReduction * cooldownMultiplicator * bonusCooldown; // Уменьшаем время перезарядки
        if (IsActive)
        {
            ChangeCooldown(cooldown); // Изменяем перезарядку, если способность активна
        }
    }
    
    private void Shuffle(GameObject[] array)
    {
        int n = array.Length;
        System.Random rng = new System.Random();
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    protected override void DamageUpgrage()
    {
       
    }

    protected override void RadiusUpgrade()
    {
        
    }

    protected override void DurationUpgrade()
    {
        Duration = duration * bonusDuration;
    }

    protected override void CountUpgrade()
    {
       
    }
}