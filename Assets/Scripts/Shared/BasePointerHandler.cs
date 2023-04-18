using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Diwide.Checkers
{
    public abstract class BasePointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IInitializable
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        private List<Material> _materials = new();
        
        public event TargetingEvent OnTargetEventHandler;

        public void Initialize()
        {
            _meshRenderer.GetMaterials(_materials);
        }
        
        protected void AddMaterial(Material material)
        {
            _materials.Add(material);
            _meshRenderer.materials = _materials.ToArray();
        }
        
        protected void RemoveMaterial(Material material)
        {
            _materials.Remove(material);
            _meshRenderer.materials = _materials.ToArray();
        }
        
        public abstract void OnPointerEnter(PointerEventData eventData);
        public abstract void OnPointerExit(PointerEventData eventData);

        public void InvokeOnTargetEvent(TilePointerHandler target, bool isTargeting)
        {
            OnTargetEventHandler?.Invoke(target, isTargeting);
        }
    }

    public delegate void TargetingEvent(TilePointerHandler component, bool isTargeting);
}