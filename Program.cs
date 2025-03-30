namespace apbd2;

class Program {
    static void Main() {
        CargoShip ship1 = new CargoShip("BiggusDickus", 25, 5, 50);
        CargoShip ship2 = new CargoShip("IncontinentiaButtocks", 30, 3, 30);
        CargoShip ship3 = new CargoShip("NaughtiusMaximus", 28, 4, 40);

        var c1 = new Container_L(2.5, 3, 10, 2, false);
        c1.LoadCargo(8);

        var c2 = new Container_G(2.5, 3, 8, 1.8, 5);
        c2.LoadCargo(6);

        var c3 = new Container_C(2.5, 3, 5, 1.5, "Bananas", -5);
        c3.LoadCargo(4, -5);

        Console.WriteLine($"--- Załadunek kontenerów na {ship1.ship_name} ---\n");
        ship1.LoadContainer(c1);
        ship1.LoadContainer(c2);
        ship1.ListContainers();

        Console.WriteLine($"--- Załadunek kontenera na {ship2.ship_name} ---\n");
        ship2.LoadContainer(c3);
        ship2.ListContainers();

        Console.WriteLine($"--- Transfer c2 z Titan na {ship3.ship_name} ---\n");
        ship1.TransferContainer(ship3, c2);

        ship1.ListContainers();
        ship3.ListContainers();

        Console.WriteLine($"--- Wymiana kontenera na {ship1.ship_name} ---\n");
        Container c4 = new Container_L(2.5, 3, 12, 2.2, true);
        c4.LoadCargo(5);
        ship1.ReplaceContainer(0, c4);

        ship1.ListContainers();
        Console.WriteLine(ship1);
        Console.WriteLine(ship2);
        Console.WriteLine(ship3);
    }
}
