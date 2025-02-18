using System;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    #region 
    [SerializeField] private LayerMask target;
    #endregion
    #region AnimationEvents
    private void OnDamageEnter()
    {
        OnDamage?.Invoke();
    }

    private void OnDamageExit()
    {
        Destroy(gameObject);
    }
    #endregion

    public event Action OnDamage;
    public event Action OnTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target.value == (target.value | (1 << collision.gameObject.layer)))
        {
            OnTrigger?.Invoke();
            GetComponent<Collider2D>().enabled = false;
        }
    }
}