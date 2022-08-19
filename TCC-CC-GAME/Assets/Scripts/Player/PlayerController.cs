using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IPersistentSingleton<PlayerController>
{
    public Vector3 bottomLeftLimit;
    public Vector3 topRightLimit;

    public bool canMove = true;

    public string areaTransitionName;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;

    public Rigidbody2D rigidbody { get => _rigidbody; set => _rigidbody = value; }


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        OnMove();
        CharacterFlip();
    }

    void OnMove()
    {
        if (canMove)
        {
            _rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _speed;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    void CharacterFlip()
    {
        if(_rigidbody.velocity.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (_rigidbody.velocity.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
    }
}
