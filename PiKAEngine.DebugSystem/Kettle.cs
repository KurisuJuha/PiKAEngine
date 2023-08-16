using System.Reactive.Subjects;

namespace PiKAEngine.DebugSystem;

public class Kettle
{
    private readonly Subject<object> _onLoggedSubject;

    public Kettle()
    {
        _onLoggedSubject = new Subject<object>();
    }

    public IObservable<object> OnLogged => _onLoggedSubject;

    public void Log(object debugString)
    {
        _onLoggedSubject.OnNext(debugString);
    }
}