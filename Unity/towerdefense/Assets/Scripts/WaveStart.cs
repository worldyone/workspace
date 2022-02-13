using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStart : TextObj
{
    enum eState
    {
        Appear,
        Wait,
        Disappear,
        End,
    }

    const float CENTER_X = 0;
    const float OFFSET_X = 600;

    eState _state = eState.End;
    float _timer = 0;

    void Start()
    {
        X = CENTER_X + OFFSET_X;
        Visible = false;
    }

    public void Begin(int nWave)
    {
        Label = "Wave " + nWave;
        _timer = OFFSET_X;
        _state = eState.Appear;
        Visible = true;
    }

    void FixedUpdate()
    {
        switch (_state)
        {
            case eState.Appear:
                _timer *= 0.9f;
                X = CENTER_X - _timer;
                if (_timer < 1)
                {
                    // 40フレーム停止する
                    _timer = 40;
                    _state = eState.Wait;
                }
                break;
            case eState.Wait:
                _timer -= 1;
                if (_timer < 1)
                {
                    _timer = OFFSET_X;
                    _state = eState.Disappear;
                }
                break;
            case eState.Disappear:
                _timer *= 0.9f;
                X = CENTER_X + (OFFSET_X - _timer);
                if (_timer < 1)
                {
                    _state = eState.End;
                    Visible = false;
                }
                break;
            case eState.End:
                break;
        }
    }

}
