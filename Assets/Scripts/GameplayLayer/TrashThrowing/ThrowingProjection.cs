using Agate.MVC.Core;
using EcoTeam.EcoToss.PubSub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EcoTeam.EcoToss.TrashThrowing
{
    public class ThrowingProjection : MonoBehaviour
    {
        private Scene _simulationScene;
        private PhysicsScene _physicsScene;
        [SerializeField] private Transform[] _trashCanParent;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private int _maximumPhysicsFrameIterations;
        [SerializeField] private GameObject _simTrashPrefab;
        [SerializeField] private TrashThrowingController _trashThrowingController;

        private Queue<GameObject> _pool;
        [SerializeField] private int _simTrashAmountToPool;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSimulateThrowing>(SimulateTrajectory);
            PublishSubscribe.Instance.Subscribe<MessageDeleteTrajectory>(DeleteTrajectory);
        }

        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSimulateThrowing>(SimulateTrajectory);
            PublishSubscribe.Instance.Unsubscribe<MessageDeleteTrajectory>(DeleteTrajectory);
        }

        private void Start()
        {
            CreatePhysicsScene();
        }

        private void CreatePhysicsScene()
        {
            _pool = new Queue<GameObject>();

            _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
            _physicsScene = _simulationScene.GetPhysicsScene();

            //foreach (Transform trashCan in _trashCanParent)
            //{
            //    GameObject invisibleTrashCan = Instantiate(trashCan.gameObject, trashCan.position, trashCan.rotation);
            //    invisibleTrashCan.GetComponent<Renderer>().enabled = false;
            //    SceneManager.MoveGameObjectToScene(invisibleTrashCan, _simulationScene);
            //}
        }

        // Run in Update
        private void SimulateTrajectory(MessageSimulateThrowing message)
        {
            GameObject invisibleTrash;

            if (_pool.Count < _simTrashAmountToPool || _pool.Peek().gameObject == null)
            {
                invisibleTrash = Instantiate(_simTrashPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                invisibleTrash = _pool.Dequeue();
                invisibleTrash.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
                invisibleTrash.SetActive(true);
            }

            SceneManager.MoveGameObjectToScene(invisibleTrash, _simulationScene);

            _trashThrowingController.SimulateThrowTrash(message.SwipeDirection, invisibleTrash.GetComponent<Rigidbody>());

            _lineRenderer.positionCount = _maximumPhysicsFrameIterations;

            for (int i = 0; i < _maximumPhysicsFrameIterations; i++)
            {
                _physicsScene.Simulate(Time.fixedDeltaTime);
                _lineRenderer.SetPosition(i, invisibleTrash.transform.position);
            }

            StartCoroutine(SetNonactive());

            IEnumerator SetNonactive()
            {
                yield return new WaitForSecondsRealtime(3f);
                _pool.Enqueue(invisibleTrash);
                invisibleTrash.SetActive(false);
                Rigidbody tempRB = invisibleTrash.GetComponent<Rigidbody>();
                tempRB.velocity = Vector3.zero;
                tempRB.angularVelocity = Vector3.zero;
                tempRB.isKinematic = true;
            }
        }

        private void DeleteTrajectory(MessageDeleteTrajectory message)
        {
            _lineRenderer.positionCount = 0;
        }
    }
}