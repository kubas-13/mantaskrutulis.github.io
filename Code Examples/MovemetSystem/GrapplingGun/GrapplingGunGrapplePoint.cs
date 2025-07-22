using UnityEngine;

public class GrapplingGunGrapplePoint : GrapplingGunBaseState
{
    private LineRenderer _lr;
    private GameObject _pointer;
    private Transform _player;
    private Transform _camera; 
    private LayerMask _grapplePoint;
    private Vector3 savedClosestPoint;
    private float _lenghtOfSquare;
    int _colliderCount;

    public override void SetupState(GrapplingGunStateManager GGSM)
    {
        _pointer = GGSM.Pointer;
        _player = GGSM.transform;
        _camera = GGSM.Camera;
        _lr = GGSM.Lr;
        _grapplePoint = GGSM.GrappleSettings.grapplePoint;
        _lenghtOfSquare = GGSM.GrappleSettings.LenghtOfSquare;
        _colliderCount = GGSM.GrappleSettings.ColliderCountInTheList;
    }
    public override void StartState(GrapplingGunStateManager GGSM){}
    public override void UpdateState(GrapplingGunStateManager GGSM)
    {

        Collider[] colliders = new Collider[_colliderCount];
        Vector3 pos = new(_player.position.x, _player.position.y + _lenghtOfSquare, _player.position.z);
        Vector3 halfExtents = new(_lenghtOfSquare, _lenghtOfSquare, _lenghtOfSquare);
        int results = Physics.OverlapBoxNonAlloc(pos, halfExtents, colliders, Quaternion.identity, _grapplePoint);
        _lr.SetPosition(1, _player.position);
        if (results <= 0 && _pointer != null)
        {
            _pointer.transform.position = Vector3.zero;
            return;
        }
        GetPoint(colliders, results, GGSM);
    }
    private void GetPoint(Collider[] colliders, int results, GrapplingGunStateManager GGSM)
    {
        float closestDistance = float.MaxValue;
        Collider closestPoint = null;
        for (int i = 0; i < results; i++)
        {

            Collider candidate = colliders[i];
            float distancePre = Vector3.Distance(_camera.transform.position, candidate.transform.position);

            Vector3 directionFromCamera = _camera.transform.position + _camera.transform.forward * distancePre;
            float distance = Vector3.Distance(directionFromCamera, candidate.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = candidate;
            }
        }

        Vector3 direction = _camera.transform.position + _camera.transform.forward * closestDistance;
        savedClosestPoint = closestPoint.bounds.ClosestPoint(direction);
        if (_pointer != null)
        {
            _pointer.transform.position = savedClosestPoint;
        }
        bool _inputQ = GGSM.Input.InputQ;
        if (_inputQ)
        {
            GGSM.SavedClosestPoint = savedClosestPoint;
            GGSM.SwitchState(GGSM._grappleToPoint);
        }
    }
}
