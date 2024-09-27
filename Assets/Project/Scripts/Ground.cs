using UnityEngine;
using R3.Triggers;
using R3;
using Gameplay;
using MoreMountains.Feedbacks;
using Zenject;

public class Ground : MonoBehaviour
{
    [SerializeField] private MMF_Player _shakeFeedback;

    [Inject]
    private PlayerView _player;

    private void Start()
    {
        this.OnCollisionEnter2DAsObservable()
            .Where(other => other.gameObject.GetComponent<BodyPart>())
            .Select(other => other.gameObject.GetComponent<BodyPart>())
            .Subscribe(body =>
            {
                if (body.IsGrounded)
                    return;

                body.IsGrounded = true;
                _player.TakeDamage();
                _shakeFeedback.PlayFeedbacks();
            });
    }
}