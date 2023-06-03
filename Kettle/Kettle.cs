using System.Reactive.Subjects;

namespace PiKATools.DebugSystem;

public class Kettle
{
    private readonly Subject<string> onLoggedSubject;

    public Kettle()
    {
        onLoggedSubject = new Subject<string>();
    }

    public IObservable<string> onLogged => onLoggedSubject;

    public void Log(string debugString)
    {
        onLoggedSubject.OnNext(debugString);
    }
}