using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{

    #region Properties

    public Vector2 Velocity = Vector2.right;
    public Vector2 Direction { get; protected set; }
    public int Damage { get; protected set; }

    #endregion

    #region fields

    private SpriteRenderer _spriter;
    private Rigidbody2D _rigidbody;

    [SerializeField] private bool _initialized;

    #endregion

    #region MonoBehaviours

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Enemy>(out var creature))
        {
            Debug.Log(creature.name + " " + creature.hp);
            creature.hp -= Damage;
        }

        if (!(this.gameObject.activeSelf && this.gameObject != null)) return;
        Main.ObjectManager.Despawn(this);
    }

    #endregion

    private void Initialize()
    {
        _spriter = this.GetComponentInChildren<SpriteRenderer>();
        _rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    public void SetInfo(string key, int damage)
    {
        Debug.Log(key);
        Initialize();
        _spriter.sprite = Main.ResourceManager.GetResource<Sprite>($"{key}.sprite");
        
        Damage = damage;
    }
}