using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        //����
        public void Save(string saveData)
        {
            Dictionary<string, object> state = LoadFile(saveData);
            CaptureState(state);
            SaveFile(saveData, state);
        }

        //����
        public void Load(string saveData)
        {
            RestoreState(LoadFile(saveData));

        }

        public IEnumerator LoadLastScene(string saveData)
        {
            Dictionary<string, object> state = LoadFile(saveData);
            int index = SceneManager.GetActiveScene().buildIndex;
            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                index = (int)state["lastSceneBuildIndex"];
            }
            //ÿ�ζ�����һ�Σ������ڲ��ڵ�ǰ�������������ᶪʧ���ݣ����¿�ָ������
            yield return SceneManager.LoadSceneAsync(index);
            RestoreState(state);
        }

        //�����ļ�
        private void SaveFile(string saveData, Dictionary<string, object> state)
        {
            string path = GetPathFromSaveData(saveData);
            Debug.Log("Save:" + path);
            using FileStream fileStream = File.Open(path, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, state);
        }
        
        //�����ļ�
        private Dictionary<string,object> LoadFile(string saveData)
        {
            string path = GetPathFromSaveData(saveData);
            Debug.Log("Load:" + path);
            if (!File.Exists(path))
                return new Dictionary<string, object>();
            using FileStream fileStream = File.Open(path, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            return (Dictionary<string, object>)binaryFormatter.Deserialize(fileStream);
        }

        //ץȡ״̬
        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        //����״̬
        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);

                }
            }
        }

        public string GetPathFromSaveData(string saveData)
        {
           return Path.Combine(Application.persistentDataPath,saveData+".sav");
        }
    }
}