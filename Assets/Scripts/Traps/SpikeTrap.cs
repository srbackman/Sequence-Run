using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private Transform _spikeTransform;
    [Space]
    [SerializeField] private bool _stayUp;
    [SerializeField] private Vector2 _upVector;
    [SerializeField] private Vector2 _downVector;
    [Space]
    [SerializeField] private float _triggerTime = 1f;
    [SerializeField] private float _timer = 0f;
    [Space]
    [SerializeField] private float _upSpeed = 3f;
    [SerializeField] private float _downSpeed = 1f;
    [SerializeField] private float _stayUpTime = 0.2f;
    private float _upTimer = 0;
    [SerializeField] private Color _warningColor = Color.red;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        if (_stayUp)
        {
            _spikeTransform.position = (Vector2)transform.position + _upVector;
            _spriteRenderer.color = _warningColor;
        }


    }


    void Update()
    {
        if (_stayUp) return;

        _timer -= Time.deltaTime;
        _upTimer -= Time.deltaTime;
        if (_timer > 0 && _upTimer <= 0)
        {
            MoveSpike(false);
            ChangeWarningColor();
        }

        while (_timer <= 0)
        {
            _upTimer = _stayUpTime;
            _timer += _triggerTime;
        }
        if (_upTimer > 0) MoveSpike(true);
    }

    private void ChangeWarningColor()
    {
        if (_timer < (_triggerTime * 0.2f))
        {
            _spriteRenderer.color = _warningColor;
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }
    }

    private void MoveSpike(bool goingUp)
    {
        if (goingUp && (transform.position.y + _upVector.y) > _spikeTransform.position.y)
        {
            _spikeTransform.position += new Vector3(0f, _upSpeed * Time.deltaTime, 0f);
            if ((transform.position.y + _upVector.y) < _spikeTransform.position.y)
                _spikeTransform.position = ((Vector2)transform.position + _upVector);
        }
        else if (!goingUp && (transform.position.y + _downVector.y) < _spikeTransform.position.y)
        {
            _spikeTransform.position -= new Vector3(0f, _downSpeed * Time.deltaTime, 0f);
        }
    }
}
