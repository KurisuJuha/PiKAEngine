// See https://aka.ms/new-console-template for more information

using PiKAEngine.InventorySystem;

Console.WriteLine("Hello, World!");

var settings = new InventorySettings<int>((a, b) => a == b, _ => 20);
var grid = new InventoryGrid<int>(settings, 10);

grid.TryAddItems(new[]
{
    12, 12, 12, 12, 12, 12, 12
});
OutputItems();

grid.TrySubtractItem(out var _);
OutputItems();

grid.TrySubtractItems(3, out var _);
OutputItems();

void OutputItems()
{
    Console.Clear();
    foreach (var item in grid.Items) Console.WriteLine(item);
    Console.ReadKey(true);
}