
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Player event asset")]
public class PlayerEventsAsset : ScriptableObject
{
    #region Movement events

    #endregion

    #region Combat events
    /// <summary>
    /// Passes in the target's collider, null if no target found
    /// </summary>
    public Action<Collider> OnStrikeStart;
    /// <summary>
    /// Invoked when the strike ends, called by the combat state that handles the strike
    /// </summary>
    public Action OnStrikeEnd;

    public Action OnStrikeChargeReady;

    /// <summary>
    /// True if strike, false if overcharged or cancelled
    /// </summary>
    public Action<bool> OnStrikeChargeEnd;
    public Action OnStrikeOvercharged;
    public Action OnStrikeChargeStart;
    public Action OnStrikeCooldownFinished;
    public Action OnStrikeCooldownStarted;

    #endregion
}
