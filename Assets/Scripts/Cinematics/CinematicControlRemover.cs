using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{

     [SerializeField] private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<PlayableDirector>().played += CinematicControlRemover_played;
        GetComponent<PlayableDirector>().stopped += CinematicControlRemover_stopped;
    }
    
    //timeline动画播放时，取消player控制和移动
    private void CinematicControlRemover_played(PlayableDirector obj)
    {
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.enabled = false;
    }
    //timeline播放完，恢复控制权
    private void CinematicControlRemover_stopped(PlayableDirector obj)
    {
        player.enabled = true;
    }

    

}
