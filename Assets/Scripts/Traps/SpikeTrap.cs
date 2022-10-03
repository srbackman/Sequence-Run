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
    [SerializeField] public float _timer = 0f;
    [Space]
    [SerializeField] private float _upSpeed = 3f;
    [SerializeField] private float _downSpeed = 1f;
    [SerializeField] private float _stayUpTime = 0.2f;
    private float _upTimer = 0;
    [SerializeField] private Color _warningColor = Color.red;
    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _spikeCollider;

    private void Awake()
    {
        _spikeCollider = transform.GetComponentInChildren<PolygonCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        if (_stayUp)
        {
            _spikeTransform.localPosition = (Vector2)Vector2.zero + _upVector;
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
            MoveSpike(false); //spike going down
            ChangeWarningColor();
        }
        while (_timer <= 0)
        {
            
            _upTimer = _stayUpTime;
            _timer += _triggerTime;
        }
        if (_upTimer > 0) MoveSpike(true);//spike going up
    }

    private void ChangeWarningColor()
    {
        if (_timer < (_triggerTime * 0.2f))
            _spriteRenderer.color = _warningColor;
        else
            _spriteRenderer.color = Color.white;
    }

    private void MoveSpike(bool goingUp)
    {
        if (goingUp && (Vector2.zero.y + _upVector.y) > _spikeTransform.localPosition.y)
        {
            _spikeCollider.enabled = true;
            _spikeTransform.localPosition += new Vector3(0f, _upSpeed * Time.deltaTime, 0f);
            if ((Vector2.zero.y + _upVector.y) < _spikeTransform.localPosition.y)
                _spikeTransform.localPosition = ((Vector2)Vector2.zero + _upVector);
        }
        else if (!goingUp && (Vector2.zero.y + _downVector.y) < _spikeTransform.localPosition.y)
        {
            _spikeCollider.enabled = false;
            _spikeTransform.localPosition -= new Vector3(0f, _downSpeed * Time.deltaTime, 0f);
        }
    }
}
