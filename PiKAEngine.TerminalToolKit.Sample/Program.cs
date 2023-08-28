using PiKAEngine.TerminalToolKit;
using PiKAEngine.TerminalToolKit.Widgets.TestWidgets;

var sw = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false };
Console.Clear();
Console.CursorVisible = false;
Console.SetOut(sw);
var texture = new Texture(new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight));
var renderString = "";

while (true)
{
    texture.Clear();
    if (!texture.Size.Equals(new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight))) Console.Clear();
    texture.Resize(new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight));
    var testWidget = new TestWidget(renderString);
    testWidget.GetConstraint(Constraint.Loose(new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight)));
    var renderObject = testWidget.CreateRenderObjectTree();

    renderObject.Draw(new Position(0, 0), new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight), texture);

    Console.SetCursorPosition(0, 0);
    foreach (var pixel in texture.Pixels) Console.Write(pixel == (char)0 ? ' ' : pixel);
    Console.Out.Flush();

    if (Console.KeyAvailable)
    {
        var info = Console.ReadKey(true);

        renderString += info.KeyChar;
    }
}