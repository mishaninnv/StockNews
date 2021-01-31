using System;
using UnityEngine;

public class LoadLots : MonoBehaviour
{
    public LotConstructor lotPref;
    public Transform lotParent;

    private Lots _lots;

    private void Start()
    {
        _lots = new Lots();
        LoadFromFile();
    }

    private void LoadFromFile()
    {
        try
        {
            var file = Resources.Load("data") as TextAsset;

            if (file == null)
                return;

            var json = file.text;

            _lots = JsonUtility.FromJson<Lots>(json);
            ProcessingDownloadedData();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void ProcessingDownloadedData()
    {
        if (_lots.lotsInformation.Length <= 0) return;

        foreach (var lotInformation in _lots.lotsInformation)
        {
            var lot = Instantiate(lotPref, lotParent);
            lot.Initialize(lotInformation);
        }
    }

    [System.Serializable]
    public struct Lots
    {
        public LotInformation[] lotsInformation;
    }

    [System.Serializable]
    public struct LotInformation
    {
        public string avatar, lotName, playerName, count, price, level;
    }
}