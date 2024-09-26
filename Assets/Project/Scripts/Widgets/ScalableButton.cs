using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;



#if LITMOTION
using LitMotion;
using LitMotion.Extensions;
#elif DOTWEEN
using DG.Tweening;
#endif

#if R3
using R3;
#endif

namespace Wigets
{
    [RequireComponent(typeof(Image))]
    public class ScalableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
#if R3
        public Subject<Unit> OnClick = new();
#else
        public event Action OnClick;
#endif

        [SerializeField] Image _image;
        [SerializeField] TMP_Text _label;

        [Header("Scales")]
        [SerializeField] float _enterScale = 1.05f;
        [SerializeField] float _downScale = 0.9f;

        [Header("AnimationTimes")]
        [SerializeField] float _enterAnimTime = 0.17f;
        [SerializeField] float _exitAnimTime = 0.3f;
        [SerializeField] float _downAnimTime = 0.2f;
        [SerializeField] float _upAnimTime = 0.3f;

        [Header("AnimationTypes")]
        [SerializeField] Ease _enterEasing = Ease.OutCubic;
        [SerializeField] Ease _exitEasing = Ease.OutCubic;
        [SerializeField] Ease _downEasing = Ease.OutCubic;
        [SerializeField] Ease _upEasing = Ease.OutCubic;

        [Header("InteractionOptions")]
        [SerializeField] float _colorMultiplier = 0.9f;
        [SerializeField] float _alphaMultiplier = 0.6f;

        [field: SerializeField] public bool IsInteractable { get; private set; } = true;
#if LITMOTION
        private MotionHandle _currentAnim;
#elif DOTWEEN
        private Sequence _currentAnim;
#endif
        private Color _originalColor;
        private Vector3 _defaultScale = Vector3.one;
        private bool _isPointerEnter = false;
        private bool _isInitialized = false;

        private void Awake()
        {
            TryInit();
        }

        private void OnDestroy()
        {
#if LITMOTION
            if (_currentAnim.IsActive()) _currentAnim.Cancel();
#elif DOTWEEN
            _currentAnim?.Kill();
#endif
        }

        public void TryInit()
        {
            if (_isInitialized == false)
            {
                _isInitialized = true;
                _defaultScale = transform.localScale;
                _originalColor = _image.color;

                SetInteractable(IsInteractable);
            }
        }

        public void SetText(string text) => _label.text = text;

        public void SetDefaultState()
        {
#if LITMOTION
            if (_currentAnim.IsActive()) _currentAnim.Cancel();
#elif DOTWEEN
            _currentAnim?.Kill();
#endif
            transform.localScale = _defaultScale;
        }

#if LITMOTION
        public void SetAnimation(MotionHandle animation)
        {
            if (_currentAnim.IsActive()) _currentAnim.Cancel();
            _currentAnim = animation;
        }
#elif DOTWEEN
        public void SetAnimation(Sequence animation)
        {
            _currentAnim?.Kill();
            _currentAnim = animation;
            _currentAnim.Play();
        }
#endif

        public void SetInteractable(bool isInteractable)
        {
            if (isInteractable)
            {
                IsInteractable = true;

                _image.color = _originalColor;
            }
            else
            {
                IsInteractable = false;

                var color = new Color(
                    _originalColor.r * _colorMultiplier,
                    _originalColor.g * _colorMultiplier,
                    _originalColor.b * _colorMultiplier,
                    _originalColor.a * _alphaMultiplier
                    );
                _image.color = color;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsInteractable == false) return;
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            //Debug.Log("Button pressed");


#if R3
            OnClick.OnNext(new());
#else
            OnClick?.Invoke();
#endif
    }

    #region Up/Down       
    public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            if (IsInteractable == false) return;
#if LITMOTION
            var handle = LMotion.Create(transform.localScale, _defaultScale * _downScale, _downAnimTime)
                .WithEase(_downEasing)
                .WithOnCancel(CheckActivity)
                .WithOnComplete(CheckActivity)
                .BindToLocalScale(transform);
                        SetAnimation(handle);
#elif DOTWEEN
            var tween = transform.DOScale(_defaultScale * _downScale, _downAnimTime).SetEase(_downEasing);
            SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
#endif

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            if (IsInteractable == false) return;
#if LITMOTION
            if (_isPointerEnter)
            {
                var handle = LMotion.Create(transform.localScale, _defaultScale * _enterScale, _upAnimTime)
                    .WithEase(_upEasing)
                    .WithOnCancel(CheckActivity)
                    .WithOnComplete(CheckActivity)
                    .BindToLocalScale(transform);
                SetAnimation(handle);
            }
            else
            {
                var handle = LMotion.Create(transform.localScale, _defaultScale, _upAnimTime)
                    .WithEase(_upEasing)
                    .WithOnCancel(CheckActivity)
                    .WithOnComplete(CheckActivity)
                    .BindToLocalScale(transform);
                SetAnimation(handle);
            }
#elif DOTWEEN
            if (_isPointerEnter)
            {
                var tween = transform.DOScale(_defaultScale * _enterScale, _upAnimTime).SetEase(_upEasing);
                SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
            }
            else
            {
                var tween = transform.DOScale(_defaultScale, _upAnimTime).SetEase(_upEasing);
                SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
            }
#endif

        }
        #endregion

        #region Enter/Exit
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsInteractable == false) return;
            _isPointerEnter = true;
#if LITMOTION            
            var handle = LMotion.Create(transform.localScale, _defaultScale * _enterScale, _enterAnimTime)
                .WithEase(_enterEasing)
                .WithOnCancel(CheckActivity)
                .WithOnComplete(CheckActivity)
                .BindToLocalScale(transform);

            SetAnimation(handle);
#elif DOTWEEN  
            var tween = transform.DOScale(_defaultScale * _enterScale, _enterAnimTime).SetEase(_enterEasing);
            SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
#endif
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsInteractable == false) return;
            _isPointerEnter = false;
#if LITMOTION
            var handle = LMotion.Create(transform.localScale, _defaultScale, _exitAnimTime)
                .WithEase(_exitEasing)
                .WithOnCancel(CheckActivity)
                .WithOnComplete(CheckActivity)
                .BindToLocalScale(transform);

            SetAnimation(handle);
#elif DOTWEEN
            var tween = transform.DOScale(_defaultScale, _exitAnimTime).SetEase(_exitEasing);
            SetAnimation(DOTween.Sequence().Append(tween).AppendCallback(CheckActivity));
#endif
        }
        #endregion

        private void CheckActivity()
        {
            if (isActiveAndEnabled == false)
            {
                transform.localScale = _defaultScale;
            }
        }
    }
}