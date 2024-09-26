using System.Collections;
using TMPro;
using UnityEngine;

namespace Root
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] TMP_Text _label;

        private Coroutine _coroutine;

        public void Show()
        {
            transform.Activate();

            _coroutine = StartCoroutine(Animate());
        }

        public void Hide()
        {
            transform.Disactivate();
            StopCoroutine(_coroutine);
        }

        private IEnumerator Animate()
        {
            while (true)
            {
                _label.text = "<b>L</b>oading...";
                yield return new WaitForSeconds(0.1f);
                _label.text = "L<b>o</b>ading...";
                yield return new WaitForSeconds(0.1f);
                _label.text = "Lo<b>a</b>ding...";
                yield return new WaitForSeconds(0.1f);
                _label.text = "Loa<b>d</b>ing...";
                yield return new WaitForSeconds(0.1f);
                _label.text = "Load<b>i</b>ng...";
                yield return new WaitForSeconds(0.1f);
                _label.text = "Loadi<b>n</b>g...";
                yield return new WaitForSeconds(0.1f);
                _label.text = "Loadin<b>g</b>...";
                yield return new WaitForSeconds(0.1f);
                _label.text = "Loading<b>.</b>..";
                yield return new WaitForSeconds(0.1f);
                _label.text = "Loading.<b>.</b>.";
                yield return new WaitForSeconds(0.1f);
                _label.text = "Loading..<b>.</b>";
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}