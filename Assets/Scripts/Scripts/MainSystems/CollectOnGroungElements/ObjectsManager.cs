using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectsManager : MonoBehaviour, ITimeController
{
    [SerializeField] private Transform player;
    private HashSet<Transform> onGroundObjects = new HashSet<Transform>();
    private float currentTime = 0;

    private void OnEnable()
    {
        CrystalPrefab.OnGetCrystal += GetNewOnGround;
        CrystalPrefab.OnDeleteCrystal += GetOutOnGround;
        Heart.OnGetHeart += GetNewOnGround;
        Heart.OnGetOutHeart += GetOutOnGround;
    }
    private void GetNewOnGround(Transform element)
    {
        onGroundObjects.Add(element);
    }
    private void GetOutOnGround(Transform element)
    {
        if (element != null && onGroundObjects.Contains(element))
        {
            onGroundObjects.Remove(element);
        }
    }
   

    public void Timerred(float deltaTime)
    {
        currentTime = currentTime + deltaTime;
        if (currentTime >= 3f && onGroundObjects != null)
        {
            List<Transform> objectsToDestroy = new List<Transform>();
            foreach (Transform t in onGroundObjects)
            {
                if (t != null)
                {
                    float distanse = Vector2.Distance(player.position, t.position);

                    if (distanse > 10)
                    {
                        objectsToDestroy.Add(t); // Добавляем объект в список, который будет удален
                    }
                }

                
            }
            foreach (Transform t in objectsToDestroy)
            {
                Destroy(t.gameObject);
                onGroundObjects.Remove(t); // Удаляем из HashSet
            }
            currentTime = 0f;
        }
    }

    private void OnDisable()
    {
        CrystalPrefab.OnGetCrystal -= GetNewOnGround;
        CrystalPrefab.OnDeleteCrystal -= GetOutOnGround;
        Heart.OnGetHeart -= GetNewOnGround;
        Heart.OnGetOutHeart -= GetOutOnGround;
    }
}
