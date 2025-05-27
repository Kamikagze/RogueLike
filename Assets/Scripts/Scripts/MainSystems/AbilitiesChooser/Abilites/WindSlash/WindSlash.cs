using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlash : MonoBehaviour
{
    // направление
    [SerializeField] private Transform startingPos;
    [SerializeField] private Transform endPos;

    public float duration = 0.8f;// ¬рем€ дл€ перемещени€ 

    // родители
    [SerializeField] private Transform defaultParent;
    [SerializeField] private Transform secondParent;

    // лицо
    [SerializeField] private Transform fase;
    private float defaultAngle;
    private void Awake()
    {
        defaultAngle = transform.localRotation.eulerAngles.z;
    }
    private void OnEnable()
    {
       StartCoroutine(LifeTime());
    }
    private IEnumerator ProcessOfMoove()
    {
        
        Vector2 _startPos = startingPos.position;
        Vector2 _endPos = endPos.position;
        float currentTime = 0;
        while (currentTime < duration)
        {
            transform.position = Vector2.Lerp(_startPos, _endPos, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        transform.position = _endPos;
        yield return new WaitForSeconds(0.2f);
    }
    private IEnumerator LifeTime()
    {
        SetNewParent();
        yield return StartCoroutine(ProcessOfMoove());
        
        ReturnDefaultParent();
        Offer();
    }
    public void Offer()
    {
        gameObject.SetActive(false);
    }

    public void SetNewParent()
    {
        gameObject.transform.SetParent(secondParent);
    }
    public void ReturnDefaultParent()
    {
        gameObject.transform.SetParent(defaultParent);
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.Euler(0,0,defaultAngle);
    }
}
