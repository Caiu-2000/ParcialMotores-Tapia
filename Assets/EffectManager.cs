using System;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Material materialDamage;

    [SerializeField] float EffectValue = 0.04f;

    float Damagevalue = 0.0f;

    int bufferedlife;

    private void Start()
    {
        Gamemanager.Effect = this;
        materialDamage.SetFloat("_HealtPercentage", 0.0f);

        EventManager<GameEvent>.Subscribe<int>(GameEvent.DamagePlayer, DamagedPlayer);
        EventManager<GameEvent>.Subscribe<int>(GameEvent.PlayerDied, Died);
    }
    private void OnDisable()
    {
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.DamagePlayer, DamagedPlayer);
        EventManager<GameEvent>.Unsubscribe<int>(GameEvent.PlayerDied, Died);
    }



    protected void DamagedPlayer(int data)
    {
        bufferedlife = data;

        Damagevalue = EffectValue;
        print("dañado : " + bufferedlife);

       
    }
    protected void Died(int data){

        materialDamage.SetFloat("_HealtPercentage", 0.0f);
        }

    private void Update()
    {
        if (Damagevalue > 0)
        {
            materialDamage.SetFloat("_HealtPercentage", Damagevalue);
            Damagevalue -= Time.deltaTime * 4;
            if (Damagevalue < 0) Damagevalue = 0;

        }
    }

    public void ResetValues()
    {
        materialDamage.SetFloat("_HealtPercentage", 0.0f);
        Damagevalue = 0;
    }
}
