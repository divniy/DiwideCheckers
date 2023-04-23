using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Diwide.Checkers
{
    public class TilePointerHandler : BasePointerHandler, IPointerClickHandler
    {
        [Inject] private GameInstaller.Settings _settings;
        [Inject] private TileFacade _tileFacade;
        [Inject] private SignalBus _signalBus;
        public TileIndex TileIndex => _tileFacade.TileIndex;
        public bool IsValidMove { get; private set; } = false;
        
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

        public void OnSelectValidDestination(bool isSelected)
        {
            if (isSelected)
            {
                IsValidMove = true;
                AddMaterial(_settings.ValidMoveMaterial);
            }
            else
            {
                IsValidMove = false;
                RemoveMaterial(_settings.ValidMoveMaterial);
            }
        }
        
        public void OnRemoveValidDestination()
        {
            RemoveMaterial(_settings.ValidMoveMaterial);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _signalBus.Fire(new TileSelectedSignal(){ Tile = _tileFacade});
        }
    }
}