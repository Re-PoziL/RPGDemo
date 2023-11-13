using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    //指示Unity编辑器在编辑器模式下或者游戏运行时都要执行该脚本
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string uniqueIdentifier = "";
        private Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();
        
        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public Dictionary<string, object> CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDic = (Dictionary<string, object>)state;
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();
                
                if(stateDic.ContainsKey(typeString))
                {
                    saveable.RestoreState(stateDic[typeString]);
                }
            }
        }

//只有在unity中才运行这一段代码
#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            //检查是否是预制体，预制体就不给uuid
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;
            //序列化对象
            SerializedObject serializedObject = new SerializedObject(this);
            //序列化属性对象,这儿是对象名，是一个字符串，不是字符串对象
            SerializedProperty serializedProperty = serializedObject.FindProperty("uniqueIdentifier");
            //判断序列化属性的值为空，或者这个值不是唯一的
            if(string.IsNullOrEmpty(serializedProperty.stringValue) || !IsUnique(serializedProperty.stringValue))
            {
                serializedProperty.stringValue = System.Guid.NewGuid().ToString();
                //应用序列化对象的修改
                serializedObject.ApplyModifiedProperties();
            }
            //把uuid和对象记作键值对放在globalLookup中
            globalLookup[serializedProperty.stringValue] = this;
        }
#endif

        private bool IsUnique(string candidate)
        {
            //字典中不存在
            if (!globalLookup.ContainsKey(candidate)) return true;
            //字典中这个id对应的当前对象
            if (globalLookup[candidate] == this) return true;
            //字典中这个对象对应null
            if(globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }
            //字典中这个对象的uuid不等于candidata
            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }    
            return false; 
        }
    }
}