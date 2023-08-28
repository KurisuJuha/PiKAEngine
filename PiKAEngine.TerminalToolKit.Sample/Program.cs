// See https://aka.ms/new-console-template for more information

using PiKAEngine.TerminalToolKit;
using PiKAEngine.TerminalToolKit.Widgets.TestWidgets;

var texture = new Texture(new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight));

while (true)
{
    texture.Resize(new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight));
    var testWidget = new TestWidget();
    testWidget.GetConstraint(Constraint.Loose(new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight)));
    var renderObject = testWidget.CreateRenderObjectTree();

    renderObject.Draw(new Position(0, 0), new Size((ushort)Console.WindowWidth, (ushort)Console.WindowHeight), texture);

    Console.SetCursorPosition(0, 0);
    Console.Write(string.Join("", texture.Pixels));
    Console.ReadKey(true);
}