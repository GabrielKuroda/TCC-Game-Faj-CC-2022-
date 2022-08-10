using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.Instance.rigidbody.velocity.sqrMagnitude > 0)
        {
            _animator.SetInteger("transition", 1);
        }
        else
        {
            _animator.SetInteger("transition", 0);
        }
    }
}
