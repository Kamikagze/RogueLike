using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DELETALL : MonoBehaviour
{
    public static event Action Delit;

    public static event Action HPOut;
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EXP"))
        {
            OnDelit();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("HP"))
        {
            OnHPOut();
            Destroy(collision.gameObject);
        }

        else if (!collision.gameObject.CompareTag("Abil") && !collision.gameObject.CompareTag("WanderingFlash")
            && !collision.gameObject.CompareTag("FreezingField") && !collision.gameObject.CompareTag("Meteor")
            && !collision.gameObject.CompareTag("IceArrow") && !collision.gameObject.CompareTag("MagicBolt")
            && !collision.gameObject.CompareTag("LavaField") && !collision.gameObject.CompareTag("FireBall") 
            && !collision.gameObject.CompareTag("LightningOrb") &&  !collision.gameObject.CompareTag("GravityOrb")
            && !collision.gameObject.CompareTag("GravityExplosion")&& !collision.gameObject.CompareTag("Orbital")
            && !collision.gameObject.CompareTag("StonePicke") && !collision.gameObject.CompareTag("StoneWalk")
            && !collision.gameObject.CompareTag("FireBreath"))
            Destroy(collision.gameObject );
    }

    public void OnDelit()
    {
        Delit?.Invoke();
    }
    public void OnHPOut()
    {
        HPOut?.Invoke();
    }

}
