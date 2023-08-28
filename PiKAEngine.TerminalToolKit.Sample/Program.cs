// See https://aka.ms/new-console-template for more information

using PiKAEngine.TerminalToolKit;
using PiKAEngine.TerminalToolKit.Widgets.TestWidgets;

var sw = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false };
Console.Clear();
Console.SetOut(sw);
var texture = new Texture(new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight));

while (true)
{
    texture.Resize(new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight));
    var testWidget = new TestWidget();
    testWidget.GetConstraint(Constraint.Loose(new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight)));
    var renderObject = testWidget.CreateRenderObjectTree();

    renderObject.Draw(new Position(0, 0), new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight), texture);

    Console.SetCursorPosition(0, 0);
    foreach (var pixel in texture.Pixels) Console.Write(pixel);
    Console.Out.Flush();
    Console.ReadKey(true);
}