using System.Collections;
using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class SimpleTranslator : SimpleTransformer
    {
        public new Rigidbody rigidbody;
        public Vector3 start = -Vector3.forward;
        public Vector3 end = Vector3.forward;
        public GameObject targetGameObject;

        public override void PerformTransform(float position)
        {
            StartCoroutine(PerformTransformCoroutine(position));
        }

        private IEnumerator PerformTransformCoroutine(float position)
        {
            var curvePosition = accelCurve.Evaluate(position);
            var pos = transform.TransformPoint(Vector3.Lerp(start, end, curvePosition));
            Vector3 deltaPosition = pos - rigidbody.position;

            if (Application.isEditor && !Application.isPlaying)
                rigidbody.transform.position = pos;

            rigidbody.MovePosition(pos);

            if (m_Platform != null)
                m_Platform.MoveCharacterController(deltaPosition);

            yield return new WaitForFixedUpdate();

            yield return new WaitForSeconds(5f); // µÈ´ý3Ãë

            if (targetGameObject != null)
            {
                StartUI startUI = FindObjectOfType<StartUI>();
                if (startUI != null)
                {
                    startUI.over();
                }
            }
        }
    }
}
