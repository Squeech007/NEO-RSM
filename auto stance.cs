using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
    bool _autoStanceEnabled = true;
    bool _autoStanceManual = false;
    bool _autoStanceConfigLoaded = false;
    bool _autoStanceThreat = false;

    double _autoStanceDwell = 0;
    double _autoStanceClear = 0;

    string _autoStanceIdle = "Cruise";
    string _autoStanceThreatStance = "Hunting";
    string _autoStanceDocked = "Docked";

    double _autoStanceDwellSec = 4;
    double _autoStanceClearSec = 8;

    Dictionary<MyDetectedEntityInfo, float> _autoStanceThreats =
        new Dictionary<MyDetectedEntityInfo, float>();

    bool autoStanceDocked()
    {
        foreach (IMyShipConnector connector in _connectors)
        {
            if (connector != null &&
                !connector.Closed &&
                connector.Status == MyShipConnectorStatus.Connected)
                return true;
        }

        return false;
    }

    bool autoStanceHasThreat()
    {
        _autoStanceThreats.Clear();
        _wcPbApi.GetSortedThreats(Me, _autoStanceThreats);
        return _autoStanceThreats.Count > 0;
    }

    void updateAutoStance()
    {
        if (_autoStanceManual ||
            !_autoStanceEnabled ||
            !_autoStanceConfigLoaded)
            return;

        double dt = Runtime.TimeSinceLastRun.TotalSeconds;
        if (dt < 0 || dt > 10)
            dt = 0;

        bool docked = autoStanceDocked();
        bool threat = autoStanceHasThreat();
        string desired;

        if (docked)
        {
            _autoStanceThreat = false;
            _autoStanceClear = 0;
            desired = _autoStanceDocked;
        }
        else if (threat)
        {
            _autoStanceThreat = true;
            _autoStanceClear = 0;
            desired = _autoStanceThreatStance;
        }
        else
        {
            if (_autoStanceThreat)
            {
                _autoStanceThreat = false;
                _autoStanceClear = _autoStanceClearSec;
            }

            if (_autoStanceClear > 0)
            {
                _autoStanceClear -= dt;
                return;
            }

            desired = _autoStanceIdle;
        }

        if (desired == _currentStanceName)
        {
            _autoStanceDwell = 0;
            return;
        }

        if (_autoStanceDwell > 0)
        {
            _autoStanceDwell -= dt;
            return;
        }

        if (!_stances.ContainsKey(desired))
        {
            Echo("AutoStance: no such stance '" + desired + "'.");
            _autoStanceEnabled = false;
            return;
        }

        setStance(desired);
        _autoStanceDwell = _autoStanceDwellSec;
    }

    string resolveStanceName(string stance)
    {
        if (string.Equals(stance, "hunt", StringComparison.OrdinalIgnoreCase))
            return "Hunting";

        foreach (string name in _stances.Keys)
            if (string.Equals(name, stance, StringComparison.OrdinalIgnoreCase))
                return name;

        return stance;
    }

    void handleStanceCommand(string stance)
    {
        if (string.Equals(stance, "Auto", StringComparison.OrdinalIgnoreCase))
        {
            _autoStanceManual = false;
            _autoStanceDwell = 0;
            _autoStanceClear = 0;
            return;
        }

        if (string.Equals(stance, "Hold", StringComparison.OrdinalIgnoreCase))
        {
            _autoStanceManual = true;
            return;
        }

        stance = resolveStanceName(stance);

        if (_stances.ContainsKey(stance))
            _autoStanceManual = true;

        setStance(stance);
    }
}

}
