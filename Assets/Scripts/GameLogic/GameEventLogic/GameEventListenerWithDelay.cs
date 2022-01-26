using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerWithDelay : GameEventListener
{
    [SerializeField] float _delay = 1f;
    [SerializeField] UnityEvent _delayUnityEvent;

    public override void RaiseEvent()
    {
        _unityEvent.Invoke();
        StartCoroutine(RunDelayedEvent());
    }

    private IEnumerator RunDelayedEvent()
    {
        yield return new WaitForSeconds(_delay);
        _delayUnityEvent.Invoke();
    }
}
