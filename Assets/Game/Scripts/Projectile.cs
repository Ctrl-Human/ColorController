using UnityEngine;

public class Projectile : ProjectileBase
{

    private Vector3 _direction;
    private float _speed = 20f;
    public override void Initialize(Vector3 direction)
    {
        _direction = direction;
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction.normalized * _speed * Time.deltaTime;
    }
}
