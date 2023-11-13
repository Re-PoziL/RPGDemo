using RPG.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{

    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject HitImpact;
        private Transform target;
        private GameObject attacker;
        private float damage;
        public static event UnityAction OnShoot;

        // Update is called once per frame
        void Update()
        {
            if (!target) return;
            transform.LookAt(AimTransform());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(GameObject attacker,Transform target,float damage)
        {
            this.target = target;
            this.damage = damage;
            this.attacker = attacker;
        }
         
        private void OnTriggerEnter(Collider other)
        {  
            if (other.GetComponent<Transform>() != target) 
                return;
            if (other.GetComponent<Health>().IsDie())
            {
                Destroy(gameObject);
                return;
            }
            target.GetComponent<Health>().TakeDamage(attacker, damage);
            if(HitImpact!=null)
            {
                Instantiate(HitImpact, AimTransform(),transform.rotation);
            }
            OnShoot?.Invoke();
            Debug.Log("…‰÷–¡À");
            Destroy(gameObject);
        }

        private Vector3 AimTransform()
        {
            CapsuleCollider capsuleCollider = target.GetComponent<CapsuleCollider>();
            if (capsuleCollider)
            {
                return target.position + Vector3.up * capsuleCollider.height / 2;
            }
            return target.position;
        }
    }
}