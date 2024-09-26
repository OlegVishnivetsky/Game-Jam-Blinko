using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


#if LITMOTION
using LitMotion;
using LitMotion.Extensions;
#elif DOTWEEN
using DG.Tweening;
#endif

#if R3
using R3;
#endif

namespace Widgets
{
    public class DoubleButtonWidget : MonoBehaviour
    {
#if R3
        public Subject<Unit> OnAnyClick = new();
        public Subject<Unit> OnLeftClick = new();
        public Subject<Unit> OnRightClick = new();
#else
        public event Action OnAnyClick;
        public event Action OnLeftClick;
        public event Action OnRightClick;
#endif

        [SerializeField] DoubleButtonWidgetElement _leftButton;
        [SerializeField] DoubleButtonWidgetElement _rightButton;
        [SerializeField] ScaleSettings _scaleSettings;
        [SerializeField] TimeSettings _timeSettings;
        [SerializeField] EasingSettings _easingSettings;
        [SerializeField] InteractionSettings _interactionSettings;

        [field: SerializeField] public bool IsInteractable { get; private set; } = true;

        Vector2 _leftMin = new Vector2(0.1f, 0.1f);
        Vector2 _leftMax = new Vector2(0.45f, 0.9f);
        Vector2 _rightMin = new Vector2(0.55f, 0.1f);
        Vector2 _rightMax = new Vector2(0.9f, 0.9f);
        private bool[] _elementsInteractionState;
        private bool _isInitialized = false;

        public void Awake()
        {
            TryInitialize();
        }

        public void TryInitialize()
        {
            if (_isInitialized) return;
            _isInitialized = true;
            if (_leftButton == null || _rightButton == null)
            {
                Debug.Log("DoubleButtonWidget requared two DoubleButtonWidgetElement refs. One of then is null.");
                return;
            }
            _leftButton.Init(_scaleSettings, _timeSettings, _easingSettings, _interactionSettings);
            _rightButton.Init(_scaleSettings, _timeSettings, _easingSettings, _interactionSettings);

            _elementsInteractionState = new bool[2] { _leftButton.IsInteractable, _rightButton.IsInteractable };

            _leftMin = _leftButton.RectTransform.anchorMin;
            _leftMax = _leftButton.RectTransform.anchorMax;
            _rightMin = _rightButton.RectTransform.anchorMin;
            _rightMax = _rightButton.RectTransform.anchorMax;

            _leftButton.OnClick += OnPointerClickOnElement;
            _leftButton.OnEnter += OnPointerEnterOnElement;
            _leftButton.OnExit += OnPointerExitOnElement;
            _leftButton.OnUp += OnPointerUpOnElement;
            _leftButton.OnDown += OnPointerDownOnElement;

            _rightButton.OnClick += OnPointerClickOnElement;
            _rightButton.OnEnter += OnPointerEnterOnElement;
            _rightButton.OnExit += OnPointerExitOnElement;
            _rightButton.OnUp += OnPointerUpOnElement;
            _rightButton.OnDown += OnPointerDownOnElement;
        }

        public void RecalculateButtonsState()
        {
            TryInitialize();

#if LITMOTION
            if (_leftButton.CurrentAnim.IsActive()) _leftButton.CurrentAnim.Cancel();
            if (_rightButton.CurrentAnim.IsActive()) _rightButton.CurrentAnim.Cancel();
#elif DOTWEEN
            _leftButton.CurrentAnim?.Kill();
            _rightButton.CurrentAnim?.Kill();
#endif
            _leftButton.transform.localScale = _leftButton.DefaultScale;
            _rightButton.transform.localScale = _rightButton.DefaultScale;
        }

        public void UnsubscribeButtonListeners()
        {
            TryInitialize();

#if R3
            OnAnyClick = new();
            OnLeftClick = new();
            OnRightClick = new();
#else

            OnAnyClick = null;
            OnLeftClick = null;
            OnRightClick = null;
#endif
    }

    public void SetTextInElement(IsLeftOrRight element, string text)
        {
            TryInitialize();
            if (element == IsLeftOrRight.Left)
            {
                _leftButton.TextContent.text = text;
            }
            else
            {
                _rightButton.TextContent.text = text;
            }
        }

        public void SetInteractable(bool isInteractable)
        {
            TryInitialize();
            IsInteractable = isInteractable;

            if (isInteractable)
            {
                _leftButton.SetInteractable(_elementsInteractionState[0]);
                _rightButton.SetInteractable(_elementsInteractionState[1]);
            }
            else
            {
                _elementsInteractionState[0] = _leftButton.IsInteractable;
                _elementsInteractionState[1] = _rightButton.IsInteractable;


                _leftButton.SetInteractable(false);
                _rightButton.SetInteractable(false);
            }
        }

        public void SetInteractableForElement(IsLeftOrRight element, bool isInteractable)
        {
            TryInitialize();
            if (element == IsLeftOrRight.Left)
            {
                if (IsInteractable)
                    _leftButton.SetInteractable(isInteractable);
                _elementsInteractionState[0] = isInteractable;
            }
            else
            {
                if (IsInteractable)
                    _rightButton.SetInteractable(isInteractable);
                _elementsInteractionState[1] = isInteractable;
            }
        }

        public void SetActivityForElement(IsLeftOrRight element, bool isActive)
        {
            TryInitialize();
            if (isActive)
            {
                if (element == IsLeftOrRight.Left)
                {
                    _leftButton.SetActivity(true);
                    if (_rightButton.IsActive == false && _interactionSettings.AllowMergeButtons)
                    {
                        _leftButton.RectTransform.anchorMax = _rightMax;
                    }
                    else
                    {
                        _rightButton.RectTransform.anchorMin = _rightMin;
                        _leftButton.RectTransform.anchorMax = _leftMax;
                    }
                }
                else
                {
                    _rightButton.SetActivity(true);
                    if (_leftButton.IsActive == false && _interactionSettings.AllowMergeButtons)
                    {
                        _rightButton.RectTransform.anchorMin = _leftMin;
                    }
                    else
                    {
                        _leftButton.RectTransform.anchorMax = _leftMax;
                        _rightButton.RectTransform.anchorMin = _rightMin;
                    }
                }
            }
            else
            {
                if (element == IsLeftOrRight.Left)
                {
                    _leftButton.SetActivity(false);
                    if (_rightButton.IsActive && _interactionSettings.AllowMergeButtons)
                    {
                        _rightButton.RectTransform.anchorMin = _leftMin;
                    }
                }
                else
                {
                    _rightButton.SetActivity(false);
                    if (_leftButton.IsActive && _interactionSettings.AllowMergeButtons)
                    {
                        _leftButton.RectTransform.anchorMax = _rightMax;
                    }
                }
            }
            _leftButton.RectTransform.sizeDelta = Vector2.zero;
            _rightButton.RectTransform.sizeDelta = Vector2.zero;
        }

        private void OnPointerClickOnElement(DoubleButtonWidgetElement el, PointerEventData data)
        {
#if R3
            if (el.IsLeftOrRight == IsLeftOrRight.Left)
                OnLeftClick.OnNext(new());
            else
                OnRightClick.OnNext(new());

            OnAnyClick.OnNext(new());
#else
            if (el.IsLeftOrRight == IsLeftOrRight.Left)
                OnLeftClick?.Invoke();
            else
                OnRightClick?.Invoke();

            OnAnyClick?.Invoke();
#endif
        }

        #region Up/Down   
        private void OnPointerDownOnElement(DoubleButtonWidgetElement el, PointerEventData data)
        {

#if LITMOTION
            var handle = LMotion.Create(el.transform.localScale,
                el.DefaultScale * _scaleSettings.DownScale, _timeSettings.DownAnimTime)
                .WithEase(_easingSettings.DownEasing)
                .WithOnCancel(CheckActivity)
                .WithOnComplete(CheckActivity)
                .BindToLocalScale(el.transform);

            if (el.IsLeftOrRight == IsLeftOrRight.Left)
                el.SetAnimation(handle);
            else
                el.SetAnimation(handle);


#elif DOTWEEN
            var tween = el.transform
                    .DOScale(el.DefaultScale * _scaleSettings.DownScale, _timeSettings.DownAnimTime)
                    .SetEase(_easingSettings.DownEasing);
            if (el.IsLeftOrRight == IsLeftOrRight.Left)
                el.SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
            else
                el.SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
#endif

        }
        private void OnPointerUpOnElement(DoubleButtonWidgetElement el, PointerEventData data)
        {
#if LITMOTION
            if (el.IsEntered)
            {
                var handle = LMotion.Create(el.transform.localScale,
                    el.DefaultScale * _scaleSettings.EnterScale, _timeSettings.UpAnimTime)
                    .WithEase(_easingSettings.UpEasing)
                    .WithOnCancel(CheckActivity)
                    .WithOnComplete(CheckActivity)
                    .BindToLocalScale(el.transform);

                if (el.IsLeftOrRight == IsLeftOrRight.Left)
                    el.SetAnimation(handle);
                else
                    el.SetAnimation(handle);
            }
            else
            {
                var handle = LMotion.Create(el.transform.localScale,
                    el.DefaultScale, _timeSettings.UpAnimTime)
                    .WithEase(_easingSettings.UpEasing)
                    .WithOnCancel(CheckActivity)
                    .WithOnComplete(CheckActivity)
                    .BindToLocalScale(el.transform);

                if (el.IsLeftOrRight == IsLeftOrRight.Left)
                    el.SetAnimation(handle);
                else
                    el.SetAnimation(handle);
            }
#elif DOTWEEN
            if (el.IsEntered)
            {
                var tween = el.transform
                    .DOScale(el.DefaultScale * _scaleSettings.EnterScale, _timeSettings.UpAnimTime)
                    .SetEase(_easingSettings.UpEasing);
                if (el.IsLeftOrRight == IsLeftOrRight.Left)
                    el.SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
                else
                    el.SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
            }
            else
            {
                var tween = el.transform
                    .DOScale(el.DefaultScale, _timeSettings.UpAnimTime)
                    .SetEase(_easingSettings.UpEasing);
                if (el.IsLeftOrRight == IsLeftOrRight.Left)
                    el.SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
                else
                    el.SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
            }
#endif

        }
        #endregion

        #region Enter/Exit
        private void OnPointerEnterOnElement(DoubleButtonWidgetElement el, PointerEventData data)
        {
#if LITMOTION
            if (el.IsLeftOrRight == IsLeftOrRight.Left)
            {
                var handle1 = LMotion.Create(_rightButton.transform.localScale, 
                    _rightButton.DefaultScale * _scaleSettings.NonEnterScale, _timeSettings.EnterAnimTime)
                    .WithEase(_easingSettings.EnterEasing)
                    .WithOnCancel(CheckActivity)
                    .WithOnComplete(CheckActivity)
                    .BindToLocalScale(_rightButton.transform);

                _rightButton.SetAnimation(handle1);

                var handle2 = LMotion.Create(_leftButton.transform.localScale, 
                    _leftButton.DefaultScale * _scaleSettings.EnterScale, _timeSettings.EnterAnimTime)
                    .WithEase(_easingSettings.EnterEasing)
                    .WithOnCancel(CheckActivity)
                    .WithOnComplete(CheckActivity)
                    .BindToLocalScale(_leftButton.transform);

                _leftButton.SetAnimation(handle2);
            }
            else
            {
                var handle1 = LMotion.Create(_rightButton.transform.localScale,
                    _rightButton.DefaultScale * _scaleSettings.EnterScale, _timeSettings.EnterAnimTime)
                    .WithEase(_easingSettings.EnterEasing)
                    .WithOnCancel(CheckActivity)
                    .WithOnComplete(CheckActivity)
                    .BindToLocalScale(_rightButton.transform);

                _rightButton.SetAnimation(handle1);

                var handle2 = LMotion.Create(_leftButton.transform.localScale,
                    _leftButton.DefaultScale * _scaleSettings.NonEnterScale, _timeSettings.EnterAnimTime)
                    .WithEase(_easingSettings.EnterEasing)
                    .WithOnCancel(CheckActivity)
                    .WithOnComplete(CheckActivity)
                    .BindToLocalScale(_leftButton.transform);

                _leftButton.SetAnimation(handle2);
            }
#elif DOTWEEN
            if (el.IsLeftOrRight == IsLeftOrRight.Left)
            {
                var tween = _leftButton.transform
                    .DOScale(_leftButton.DefaultScale * _scaleSettings.EnterScale, _timeSettings.EnterAnimTime)
                    .SetEase(_easingSettings.EnterEasing);
                _leftButton.SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));

                var tween2 = _rightButton.transform
                    .DOScale(_rightButton.DefaultScale * _scaleSettings.NonEnterScale, _timeSettings.EnterAnimTime)
                    .SetEase(_easingSettings.EnterEasing);
                _rightButton.SetAnimation(DOTween.Sequence().Append(tween2).AppendCallback(CheckActivity));
            }
            else
            {
                var tween = _rightButton.transform
                    .DOScale(_rightButton.DefaultScale * _scaleSettings.EnterScale, _timeSettings.EnterAnimTime)
                    .SetEase(_easingSettings.EnterEasing);
                _rightButton.SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));

                var tween2 = _leftButton.transform
                    .DOScale(_leftButton.DefaultScale * _scaleSettings.NonEnterScale, _timeSettings.EnterAnimTime)
                    .SetEase(_easingSettings.EnterEasing);
                _leftButton.SetAnimation(DOTween.Sequence().Append(tween2).AppendCallback(CheckActivity));
            }
#endif

        }
        private void OnPointerExitOnElement(DoubleButtonWidgetElement el, PointerEventData data)
        {
#if LITMOTION
            var handle1 = LMotion.Create(_rightButton.transform.localScale, _rightButton.DefaultScale, _timeSettings.ExitAnimTime)
                .WithEase(_easingSettings.ExitEasing)
                .WithOnCancel(CheckActivity)
                .WithOnComplete(CheckActivity)
                .BindToLocalScale(_rightButton.transform);

            _rightButton.SetAnimation(handle1);

            var handle2 = LMotion.Create(_leftButton.transform.localScale, _leftButton.DefaultScale, _timeSettings.ExitAnimTime)
                .WithEase(_easingSettings.ExitEasing)
                .WithOnCancel(CheckActivity)
                .WithOnComplete(CheckActivity)
                .BindToLocalScale(_leftButton.transform);

            _leftButton.SetAnimation(handle2);
#elif DOTWEEN
            var tween = _rightButton.transform
                    .DOScale(_rightButton.DefaultScale, _timeSettings.ExitAnimTime)
                    .SetEase(_easingSettings.ExitEasing);
            _rightButton.SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));

            var tween2 = _leftButton.transform
                .DOScale(_leftButton.DefaultScale, _timeSettings.ExitAnimTime)
                .SetEase(_easingSettings.ExitEasing);
            _leftButton.SetAnimation(DOTween.Sequence().Append(tween2).AppendCallback(CheckActivity));
#endif
        }
        #endregion

        private void CheckActivity()
        {
            TryInitialize();
            if (_leftButton.isActiveAndEnabled == false)
            {
                _leftButton.transform.localScale = _leftButton.DefaultScale;
            }
            if (_rightButton.isActiveAndEnabled == false)
            {
                _rightButton.transform.localScale = _rightButton.DefaultScale;
            }
        }

        #region Settings
        [Serializable]
        public class ScaleSettings
        {
            public float EnterScale = 1.1f;
            public float NonEnterScale = 0.8f;
            public float DownScale = 0.8f;
        }

        [Serializable]
        public class TimeSettings
        {
            public float EnterAnimTime = 0.17f;
            public float ExitAnimTime = 0.3f;
            public float DownAnimTime = 0.2f;
            public float UpAnimTime = 0.3f;
        }

        [Serializable]
        public class EasingSettings
        {
            public Ease EnterEasing = Ease.OutCubic;
            public Ease ExitEasing = Ease.OutCubic;
            public Ease DownEasing = Ease.OutCubic;
            public Ease UpEasing = Ease.OutCubic;
        }

        [Serializable]
        public class InteractionSettings
        {
            public bool AllowMergeButtons = true;
            public float ColorMultiplier = 0.9f;
            public float AlphaMultiplier = 0.6f;
        }
        #endregion
    }
}