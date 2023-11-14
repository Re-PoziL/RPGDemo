using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace RPG.Control
{

    public class PlayerController : MonoBehaviour
    {

        private Health _health;
        

        [Serializable]
        private struct CursorMapping
        {
            public CursorType cursorType;
            public Texture2D cursorTexture2D;
            public Vector2 hotspot;
        }
        
        [SerializeField]
        private CursorMapping[] _cursorMapping = null;


        // Start is called before the first frame update
        void Awake()
        {
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithUI())
                return;
            if (_health.IsDie())
            {
                SetCursor(CursorType.None);
                return;
            }
            if (InteractWithComponent())
                return;
            if (InteractWithMove())
                return;
            SetCursor(CursorType.None);
        }


        private bool InteractWithComponent()
        {
            RaycastHit[] raycastHits = RaycastAllSorted();
            foreach (RaycastHit raycastHit in raycastHits)
            {
                //一条射线同时获得iraycastable
                IRaycastable[] raycastables = raycastHit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    
                    if(raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        //排序类
        private class DistanceComparer : IComparer<RaycastHit>
        {
            public int Compare(RaycastHit x, RaycastHit y)
            {
                if (x.distance < y.distance)
                {
                    return -1;
                }
                else if (x.distance > y.distance)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        
        //对射线上的所有对象进行一个排序
        private RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(GetMouseRay());
            Array.Sort(raycastHits, new DistanceComparer());
            return raycastHits;
        }

        //检查是否在ui上
        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        //点击移动
        private bool InteractWithMove()
        {
            RaycastHit raycastHit;
            if (RaycastNavmesh(out raycastHit))
            {
                if(Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(raycastHit.point);
                    SetCursor(CursorType.Movement);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavmesh(out RaycastHit raycastHit)
        {
            bool hasRaycastHit = Physics.Raycast(GetMouseRay(), out raycastHit);
            
            if (!hasRaycastHit)
                return false;
            
            NavMeshHit hit;
            float range = .5f;
            bool hasCastToNavmesh = NavMesh.SamplePosition(raycastHit.transform.position, out hit, range, NavMesh.AllAreas);
            
            if (!hasCastToNavmesh)
                return false;
            return true;
        }

        

        private void SetCursor(CursorType cursorType)
        {
            CursorMapping var = GetCursorMapping(cursorType);
            Cursor.SetCursor(var.cursorTexture2D, var.hotspot,CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType cursorType)
        {
            foreach(CursorMapping item in _cursorMapping)
            {
                if(item.cursorType == cursorType)
                {
                    return item;
                }
            }
            return _cursorMapping[0];
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
