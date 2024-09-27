using UnityEngine;
using R3.Triggers;
using R3;
using Gameplay;
using MoreMountains.Feedbacks;
using Zenject;
using Lean.Pool;

public class Ground : MonoBehaviour
{
    [SerializeField] private MMF_Player _shakeFeedback;
    [SerializeField] private ParticleSystem _fx;

    [Inject]
    private PlayerView _player;

    private void Start()
    {
        this.OnCollisionEnter2DAsObservable()
            .Where(other => other.gameObject.GetComponent<BodyPart>())
            .Select(other => other.gameObject.GetComponent<BodyPart>())
            .Subscribe(OnCharacterChatched)
            .AddTo(this);
    }

    private void OnCharacterChatched(BodyPart body)
    {
        if (body.IsGrounded)
            return;


        var fx = LeanPool.Spawn(_fx);
        fx.transform.position = body.transform.position;
        fx.Play();

        body.IsGrounded = true;
        _player.TakeDamage();
        _shakeFeedback.PlayFeedbacks();

    }
}