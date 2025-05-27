using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image barHP;


    private void Start()
    {
       
     
    }

    

    public void BarChanger(float currentHealth, float maxHealth)
    {
        barHP.fillAmount = currentHealth / maxHealth;
    }
    private void OnDisable()
    {
        
    }
}
