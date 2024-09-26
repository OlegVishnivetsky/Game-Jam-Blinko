using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] PlayerView _player;

        private void Update()
        {
            if(Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var halfScreen = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height) * 0.5f;
                var leftScreenHalf = new Rect(new(0, halfScreen.y), halfScreen);

                if (leftScreenHalf.Contains(touch.position))
                {
                    _player.MoveLeft();
                }
                else
                {
                    _player.MoveRight();
                }
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                var point = Input.mousePosition;
                var leftScreenHalf = new Rect(0, 0, Screen.currentResolution.width * 0.5f, Screen.currentResolution.height * 0.8f);
                var rightScreenHalf = new Rect(leftScreenHalf.xMax, 0, Screen.currentResolution.width * 0.5f, Screen.currentResolution.height * 0.8f);

                if (leftScreenHalf.Contains(point))
                {
                    _player.MoveLeft();
                }
                else if(rightScreenHalf.Contains(point))
                {
                    _player.MoveRight();
                }
            }
        }
    }
}