using Godot;
using System;

public class ColFallowTarget : CollisionShape
{

    [Export] private NodePath _targetToFallow;
    Vector3 referencePos = Vector3.Zero;
    private Vector3 offset = Vector3.Zero;
    
    private Spatial _spatial;
    private CylinderShape _cylinderShape;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _spatial = GetNode<Spatial>(_targetToFallow);
        _cylinderShape = (CylinderShape) Shape;

        CheckPos(_spatial.Translation);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        CheckPos(_spatial.Translation);
    }

    void CheckPos(Vector3 pos)
  {
      if (referencePos != pos)
      {
          referencePos = pos;
          _cylinderShape.Height = referencePos.y;
          offset.x = referencePos.x;
          offset.y = (_cylinderShape.Height / 2);
          offset.z = referencePos.z;
          Translation = offset;
      }
  }
}
