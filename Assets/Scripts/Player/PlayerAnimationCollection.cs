using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationCollection : MonoBehaviour
{
    public Sprite _idleSprite;
    public Sprite _jumpSprite;
    public Sprite[] _runingSprites;
    [HideInInspector] public int _currentRunSprite = 0;
}
