using System.Collections.Generic;
using System.Linq;
using BoardDefenceGame.Enemy;
using BoardDefenceGame.Events;
using BoardDefenceGame.HardCodeds;
using BoardDefenceGame.ObjectPooler;
using UnityEngine;

namespace BoardDefenceGame.Core
{
    public class LevelManager : MonoBehaviour
    {
        #region Fields
        public LevelDataScriptableObject CurrentLevelData { get; private set; }
        private List<LevelDataScriptableObject> levelDataScriptableObjects;

        [field: Header("Scene Injections")]
        [field: SerializeField] public BoardManager BoardManager { get; private set; }
        [field: SerializeField] public EnemySpawner EnemySpawner { get; private set; }
        [field: SerializeField] public DefenceItemManager DefenceItemManager { get; private set; }
        [field: SerializeField] public CameraController CameraController { get; private set; }
        [field: SerializeField] public PlayerBaseHealthManager PlayerBaseHealthManager { get; private set; }
        [field: SerializeField] public ObjectPoolManager ObjectPoolManager { get; private set; }
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            GameEvents.OnAllEnemiesDied += PlayerWonGame;
        }
        private void OnDisable()
        {
            GameEvents.OnAllEnemiesDied -= PlayerWonGame;
        }
        private void Awake()
        {
            LoadLevelData();
        }
        private void Start()
        {
            BoardManager.CreateBoard(CurrentLevelData);
            ObjectPoolManager.Initialize(CurrentLevelData);
            EnemySpawner.Initialize(CurrentLevelData);
            DefenceItemManager.SetInitialDefenceItems(CurrentLevelData);
            CameraController.ArrangeCamera(CurrentLevelData);
            PlayerBaseHealthManager.Initialize(CurrentLevelData);
            GameEvents.OnCurrentLevelSet?.Invoke(CurrentLevelData);
        }
        #endregion

        #region Private Methods
        private void LoadLevelData()
        {
            levelDataScriptableObjects = Resources.LoadAll<LevelDataScriptableObject>(Paths.LevelDataPath).ToList();
            levelDataScriptableObjects.Sort((level1, level2) => level1.LevelNumber.CompareTo(level2.LevelNumber));
            var latestReachedLevelIndex = PlayerPrefs.GetInt(PlayerPref.LatestReachedLevelIndex, 0);
            latestReachedLevelIndex = Mathf.Min(latestReachedLevelIndex, levelDataScriptableObjects.Count - 1);
            CurrentLevelData = levelDataScriptableObjects[latestReachedLevelIndex];
        }

        private void PlayerWonGame()
        {
            var latestReachedLevelIndex = PlayerPrefs.GetInt(PlayerPref.LatestReachedLevelIndex, 0);
            latestReachedLevelIndex++;
            PlayerPrefs.SetInt(PlayerPref.LatestReachedLevelIndex, latestReachedLevelIndex);
            PlayerPrefs.Save();
        }

        #endregion

    }
}

