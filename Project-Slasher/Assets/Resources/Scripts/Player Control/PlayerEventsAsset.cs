
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Player event asset")]
public class PlayerEventsAsset : ScriptableObject
{
    #region Movement events

    #endregion

    #region Combat events

    public Action<Collider> OnStrikeStart;
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

    public Action OnCollideWithProjectile;
    public Action OnCollideWithVoid;

    #endregion

    #region Utils
    public Action OnRestartLevel;
    #endregion
}
