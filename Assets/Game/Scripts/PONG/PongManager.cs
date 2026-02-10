using DG.Tweening;
using UnityEngine;

public class PongManager : MonoBehaviour
{

    private float _dir = 26f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if(other.CompareTag("Player"))
        {
            Debug.Log("hit player");
        }
    }

    private void Start()
    {
        MoveWithVelocity(new Vector3(0f, 1f, 26f), 2f);
    }

    public void MoveWithVelocity(Vector3 target, float speed)
    {
        

        transform.DOKill(); // kill previous tween (important!)

        float distance = Vector3.Distance(transform.position, target);
        float duration = distance / speed;

        transform.DOMove(target, duration)
         .SetEase(Ease.Linear).OnComplete(() =>
         {
             if (_dir == 26f)
             { _dir = 1f; }
             else
             {
                 _dir = 26f; 
             }
                 MoveWithVelocity(new Vector3(0f, 1f, _dir), 2f);

         });


    }
}
