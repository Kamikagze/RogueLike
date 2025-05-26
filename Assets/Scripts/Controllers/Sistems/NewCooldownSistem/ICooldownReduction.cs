using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICooldownable 
{
    public void CooldownReduction();

    public void PercentReduction(float percent);
    
}
