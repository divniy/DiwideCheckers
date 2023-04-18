using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Diwide.Checkers
{
    public class TilePointerHandler : BasePointerHandler
    {
        [Inject] private GameInstaller.Settings _settings;
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            // Debug.Log("TilePointerHandler OnPointerEnter");
            AddMaterial(_settings.TargetingTileMaterial);
            InvokeOnTargetEvent(this, true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            // Debug.Log("TilePointerHandler OnPointerExit");
            RemoveMaterial(_settings.TargetingTileMaterial);
            InvokeOnTargetEvent(this, false);
        }
    }
}