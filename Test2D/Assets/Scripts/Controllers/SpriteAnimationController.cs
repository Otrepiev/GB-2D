using System;
using System.Collections;
using System.Collections.Generic;
using PlatformerMVC;
using UnityEngine;

public class SpriteAnimationController : IDisposable
{
    private sealed class Animation
    {
        public AnimState Track;
        public List<Sprite> Sprites;
        public bool loop;
        public float speed = 10;
        public float counter = 10;
        public bool sleep;
        
        public void Update()
        {
            if (sleep) return;
            counter += Time.deltaTime * speed;

            if (loop)
            {
                while (counter > Sprites.Count)
                {
                    counter -= Sprites.Count;
                }
            }
            else if (counter > Sprites.Count)
            {
                counter = Sprites.Count;
                sleep = true;
            }

        }
    }

    private SpriteAnimatorConfig _config;
    private Dictionary<SpriteRenderer, Animation> _activeAnimation = new Dictionary<SpriteRenderer, Animation>();

    public SpriteAnimationController(SpriteAnimatorConfig config)
    {
        _config = config;
    }

    public void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool loop, float speed)
    {
        if (_activeAnimation.TryGetValue(spriteRenderer, out var animation))
        {
            animation.sleep = false;
            animation.loop = loop;
            animation.speed = speed;
            if (animation.Track != track)
            {
                animation.Track = track;
                animation.Sprites = _config.Sequence.Find(sequence => sequence.Track == track).Sprites;
                animation.counter = 0;
            }
        }
        else
        {
            _activeAnimation.Add(spriteRenderer, new Animation()
            {
                sleep = false,
                loop = loop,
                speed = speed,
                Track = track,
                Sprites = _config.Sequence.Find(sequence => sequence.Track == track).Sprites
            });
        }
    }

    public void StopAnimation(SpriteRenderer spriteRenderer)
    {
        if (_activeAnimation.ContainsKey(spriteRenderer))
        {

            _activeAnimation.Remove(spriteRenderer);
        }
    }
    
    public void Update()
    {
        foreach (var animation in _activeAnimation)
        {
           animation.Value.Update();

           if (animation.Value.counter < animation.Value.Sprites.Count)
           {
               animation.Key.sprite = animation.Value.Sprites[(int) animation.Value.counter];
           }
           
        }
    }

    public void Dispose()
    {
        _activeAnimation.Clear();
    }
}
