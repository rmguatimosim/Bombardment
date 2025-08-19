
public abstract class State
{
    //Name
    public readonly string name;

    //Constructor
    protected State(string name) {
        this.name = name;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void LateUpdate() { }
    public virtual void FixedUpdate() { }
}