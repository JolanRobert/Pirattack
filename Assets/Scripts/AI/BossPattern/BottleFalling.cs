using System;
using UnityEngine;
using Utils;

namespace AI.BossPattern
{
    public class BottleFalling : MonoBehaviour
    {

        [SerializeField] private Rigidbody rb;
    
        private GameObject fx;

        public void Init(float speed, GameObject _fx)
        {
            rb.velocity = Vector3.down * speed;
            fx = _fx;
        }

        private void Update()
        {
            if (transform.position.y < -10f) Pooler.Instance.Depop(Pooler.Key.Bottle, gameObject);
        }
    }
}