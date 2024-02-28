using BoardDefenceGame.Core.Grid;
using BoardDefenceGame.DefenceItems;
using BoardDefenceGame.Events;
using BoardDefenceGame.ObjectPooler;
using BoardDefenceGame.UI;
using UnityEngine;

namespace BoardDefenceGame.Core
{
    public class InputManager : MonoBehaviour
    {
        #region Fields
        [SerializeField] private LayerMask gridLayer;
        private bool isDragging = false;
        private DefenceItem draggedObject;
        private SpawnDefenceItemButton pressedSpawnButton;
        private Camera mainCamera;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            GameEventsUI.OnDefenceItemStartedToDrag += OnDefenceItemStartedToDrag;
        }
        private void OnDisable()
        {
            GameEventsUI.OnDefenceItemStartedToDrag -= OnDefenceItemStartedToDrag;

        }
        private void Awake()
        {
            mainCamera = Camera.main;
        }
        private void Update()
        {
            if (!isDragging)
                return;
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 ray = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, layerMask: gridLayer);

                if (hit.collider)
                {
                    if (hit.collider.TryGetComponent<DefenseGrid>(out var defenseGrid))
                    {
                        if (!defenseGrid.HasDefenceItem)
                        {
                            defenseGrid.PutDefenceItemToGrid(draggedObject);
                            pressedSpawnButton.ReduceDefenseItemSize();
                            ResetDrag();
                            return;
                        }
                    }
                }
                draggedObject?.GoBackToButtonView(pressedSpawnButton.transform.position);
                ResetDrag();
            }
            if (draggedObject)
            {
                MoveDraggedObjectToMousePosition();
            }
        }
        #endregion

        #region Private Methods
        private void OnDefenceItemStartedToDrag(DefenceItemFeatureScriptableObject draggingDefenceItemFeature, SpawnDefenceItemButton spawnButton)
        {
            this.pressedSpawnButton = spawnButton;
            draggedObject = ObjectPoolManager.Instance.GetDefenceItemPool(draggingDefenceItemFeature).GetPooledObject(spawnButton.transform.position).GetComponent<DefenceItem>();
            draggedObject.InitializeItem(draggingDefenceItemFeature);
            isDragging = true;
        }

        private void MoveDraggedObjectToMousePosition()
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - draggedObject.transform.position;
            draggedObject.transform.Translate(mousePosition);
        }

        private void ResetDrag()
        {
            isDragging = false;
            draggedObject = null;
            pressedSpawnButton = null;
        }
        #endregion

    }
}
