using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCore : MonoBehaviour
{
    public Transform _playerStartPoint;
    [Space]
    [SerializeField] private GameObject _spikeTrapsParent;
    private SpikeTrap[] _spikeTraps;
    private float[] _spikeTrapTimes;
    [SerializeField] private GameObject _peaShootersParent;
    private PeaShooter[] _peaShooters;
    private float[] _peaShooterTimes;

    private void Awake()
    {
        GetSpikeTrapTimes();
        GetPeaShooterTimes();
    }

    public void RestartLevel()
    {
        if (_spikeTraps != null && _spikeTraps.Length > 0)
            SetSpikeTrapTimes();

        SetPeaShooterTimes();
    }

    private void GetSpikeTrapTimes()
    {
        if (_spikeTrapsParent)
            _spikeTraps = _spikeTrapsParent.GetComponentsInChildren<SpikeTrap>();
        if (_spikeTraps == null || _spikeTraps.Length == 0) return;
        _spikeTrapTimes = new float[_spikeTraps.Length];
        for (int i = 0; i < _spikeTraps.Length; i++)
        {
            _spikeTrapTimes[i] = _spikeTraps[i]._timer;
        }
    }

    private void SetSpikeTrapTimes()
    {
        if (_spikeTraps == null || _spikeTraps.Length == 0) return;
        for (int i = 0; i < _spikeTraps.Length; i++)
        {
            _spikeTraps[i]._timer = _spikeTrapTimes[i];
        }
    }

    private void GetPeaShooterTimes()
    {
        if (_peaShootersParent)
            _peaShooters = _peaShootersParent.GetComponentsInChildren<PeaShooter>();
        if (_peaShooters == null || _peaShooters.Length == 0) return;
        _peaShooterTimes = new float[_peaShooters.Length];
        for (int i = 0; i < _peaShooters.Length; i++)
        {
            _peaShooterTimes[i] = _peaShooters[i]._timer;
        }
    }

    private void SetPeaShooterTimes()
    {
        if (_peaShooters == null || _peaShooters.Length == 0) return;
        for (int i = 0; i < _peaShooters.Length; i++)
        {
            _peaShooters[i]._timer = _peaShooterTimes[i];
        }
    }
}
