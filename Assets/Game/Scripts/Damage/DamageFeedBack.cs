using System.Collections;

using UnityEngine;


public class DamageFeedback : MonoBehaviour , IObserver<HealthData>
{
    private SpriteRenderer _spriteRend;
    [SerializeField] private Entity _entity;

    private void Start()
    {
        if (_entity == null) _entity = GetComponent<Entity>();
        _entity.getHealt().Suscribe(this);
        _spriteRend = GetComponent<SpriteRenderer>();
    }
    private void OnDisable()
    {
        if (_entity != null)
        {
            _entity.getHealt().UnSuscribe(this);
        }
    }

    private void Damaged()
    {
        StartCoroutine(PaintRed());
    }

    private IEnumerator PaintRed()
    {
        _spriteRend.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRend.color = Color.white;
    }

    public void Notify(HealthData t)
    {
        Damaged();
        SoundTypes sound = SoundTypes.menu;
        if (_entity is Player) sound = SoundTypes.HittedPlayer;
        else if (_entity is Enemy) sound = SoundTypes.HittedEnemy;

        SoundManager.instance.PlayRandom(sound);
    }
}

