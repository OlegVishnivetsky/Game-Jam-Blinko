using R3;
using R3.Triggers;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Rope : MonoBehaviour
    {
        public Subject<Collision2D> OnCollisionDetected = new();

        [SerializeField] RopeElement _ropePrefab;
        [SerializeField] Transform _startElementAnchor;
        [SerializeField] Transform _endElementAnchor;
        [Range(10, 25)][SerializeField] int _elementsCount = 15;

        private List<RopeElement> _elements;

        public void Build()
        {
            _elements = new(_elementsCount);
            for (int i = 0; i < _elementsCount; i++)
            {
                var element = Instantiate(_ropePrefab, transform);
                var offsetDirection = (-_startElementAnchor.position + _endElementAnchor.position).normalized;
                var offset = (-element.StartPoint.position + element.EndPoint.position).magnitude;

                if (i == 0)
                {
                    element.Joint.connectedBody = _startElementAnchor.GetComponent<Rigidbody2D>();
                    element.Joint.connectedAnchor = _startElementAnchor.position;
                    element.transform.position = _startElementAnchor.position + 0.5f * offsetDirection * offset;
                }
                else if(i == _elementsCount - 1)
                {
                    element.Joint.connectedBody = _elements[i - 1].Rigidbody;
                    element.Joint.connectedAnchor = _endElementAnchor.position;
                    element.transform.position = _elements[i - 1].transform.position +  offsetDirection * offset;
                }
                else
                {
                    element.Joint.connectedBody = _elements[i - 1].Rigidbody;
                    element.transform.position = _elements[i - 1].transform.position + offsetDirection * offset;
                }

                element.Collider.OnCollisionEnter2DAsObservable().Subscribe(OnElementCollisionDetected).AddTo(this);

                _elements.Add(element);
            }
        }

        private Vector3 CalcPositionForElement(RopeElement element)
        {
            //var scalecompensator

            return Vector3.zero;
        }

        private void OnElementCollisionDetected(Collision2D collision)
        {
            OnCollisionDetected.OnNext(collision);
        }
    }
}
