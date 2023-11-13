using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{

    public class FollowCamera : MonoBehaviour
    {

        [SerializeField] private Transform target;


        // Start is called before the first frame update
        void Start()
        {

        }

        private void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}