using System.Collections;
using System.Collections.Generic;
using PlatformerMVC;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private SpriteAnimatorConfig _playerConfig;
    [SerializeField] private int _animationSpeed = 10;
    [SerializeField] private LevelObjectView _playerView;

    private SpriteAnimationController _playerAnimator;
    void Start()
    {
        _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
        if (_playerConfig)
        {
            _playerAnimator = new SpriteAnimationController(_playerConfig);
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Run,true,_animationSpeed);
        }

    }

    // Update is called once per frame
    void Update()
    {
        _playerAnimator.Update();
    }
}
