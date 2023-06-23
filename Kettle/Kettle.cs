using System.Reactive.Subjects;

namespace PiKATools.DebugSystem;

public class Kettle
{
    private readonly Subject<string> _onLoggedSubject;

    public Kettle()
    {
        _onLoggedSubject = new Subject<string>();
    }

    public IObservable<string> OnLogged => _onLoggedSubject;

    public void Log(string debugString)
    {
        _onLoggedSubject.OnNext(debugString);
    }
}