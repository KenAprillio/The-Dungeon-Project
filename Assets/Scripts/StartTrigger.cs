using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class StartTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _collectibleBoon;
    [SerializeField] private GameObject _flowChart;

    [SerializeField] private PlayableDirector _timelineDirector;
    [SerializeField] private PlayableAsset _clip;

    private void PlayCutscene()
    {
        _timelineDirector.playableAsset = _clip;
        _timelineDirector.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //PlayCutscene();
            Fungus.Flowchart.BroadcastFungusMessage("UpgradeMessage");
            _collectibleBoon.SetActive(true);
            Destroy(gameObject);
        }
    }
}
