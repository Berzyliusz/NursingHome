using UnityEngine;
using NursingHome.UserInterface;
using NursingHome.Interactions;
using StarterAssets;
using Sirenix.OdinInspector;
using NursingHome.Lures;
using Audio;
using NursingHome.Audio;

namespace NursingHome
{
    public class Systems : MonoBehaviour
    {
        public static Systems Instance { get; private set; }

        [field:SerializeField]
        public UISystem UISystem { get; private set; }
        [field: SerializeField]
        public InteractionDetector InteractionDetector { get; private set; }

        //Todo: Make all those HARD dependencies be provided via externall bridge
        // converting hard dependancies into interfaces
        [SerializeField]
        FirstPersonController playerController;
        [SerializeField]
        AudioPlayer audioPlayer;
        [SerializeField]
        ItemPicker itemPicker;
        [SerializeField]
        ItemUser itemUser;

        public IPlayer Player { get; private set; }
        public IInputs Inputs { get; private set; }
        public ICursor Cursor { get; private set; }
        public IScoreCounter Score { get; private set; }
        public ITime Time { get; private set; }
        public IInteractionDetector interactionDetector { get; private set; }
        public ILureSpawner LureSpawner { get; private set; }
        public IAudioSystem AudioSystem { get; private set; }
        public IGameStateDispatcher GameStateDispatcher { get; private set; }

        public PlayerInventory Inventory { get; private set; }

        void Awake()
        {
            Instance = this;
            Inventory = new PlayerInventory(itemPicker);
            Inputs = new Inputs();
            Player = playerController;
            Cursor = new CursorHandler();
            Score = new ScoreCounter(itemUser);
            Time = new TimeHandler();
            LureSpawner = new LureSpawner(itemUser, InteractionDetector);
            interactionDetector = InteractionDetector;
            AudioSystem = new AudioSystem(audioPlayer, GameStateDispatcher);
            GameStateDispatcher = new GameStateDispatcher();
        }

        void Start()
        {
            Cursor.SetCursorLocked(CursorLockMode.Locked);
            Cursor.SetCursorVisible(false);
            Time.SetTimeScale(1.0f);
            UISystem.ShowScreen(UIType.AimDot);
            interactionDetector.SetCanDetectInteraction(true);
        }
    }
}