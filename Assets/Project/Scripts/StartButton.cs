using DG.Tweening;
using Gameplay;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private Button _button;

    [Inject]
    private GameStateHandler _gameStateHandler;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick
            .AsObservable()
            .Subscribe(_ =>
            {
                _gameStateHandler.ChangeState(GameState.Start);

                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.DOFade(0f, 0.4f)
                    .SetEase(Ease.OutSine);
            })
            .AddTo(this);
    }
}