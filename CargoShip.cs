namespace apbd2;

public class CargoShip {
    public string ship_name { get; }
    public double max_speed { get; }
    public int max_containers { get; }
    public double max_weight { get; }

    private List<Container> containers = new();

    public CargoShip(string name, double max_speed, int max_containers, double max_weight) {
        ship_name = name;
        this.max_speed = max_speed;
        this.max_containers = max_containers;
        this.max_weight = max_weight;
    }

    public void LoadContainer(Container container) {
        if (containers.Count >= max_containers) 
            throw new OverflowException("Exceeded maximum number of containers.");
        
        if (GetTotalWeight() + container.total_weight > max_weight) 
            throw new OverflowException("Exceeded maximum allowed weight on the ship.");

        containers.Add(container);
    }

    public void LoadContainers(List<Container> new_containers) {
        foreach (var c in new_containers) {
            LoadContainer(c);
        }
    }

    public void UnloadContainer(Container container) {
        if (!containers.Remove(container)) {
            throw new InvalidOperationException("Container not found on the ship.");
        }
    }

    public void ReplaceContainer(int index, Container new_container) {
        if (index < 0 || index >= containers.Count) 
            throw new IndexOutOfRangeException("Invalid container index.");
        
        containers[index] = new_container;
    }

    public void TransferContainer(CargoShip target_ship, Container container) {
        UnloadContainer(container);
        target_ship.LoadContainer(container);
    }

    public void ListContainers() {
        Console.WriteLine($"Ship: {ship_name}" +
                          $"\nContainers ({containers.Count} of {max_containers}):");
        foreach (var c in containers) {
            Console.WriteLine($" *{c.serial_number} \n\tWeight: {c.total_weight} kg");
        }
        Console.WriteLine("\n");
    }

    public double GetTotalWeight() {
        double total = 0;
        foreach (var c in containers) {
            total += c.total_weight;
        }
        return total;
    }
    
    public override string ToString() {
        return $"Ship: {ship_name}" +
               $"\nMax Speed: {max_speed} knots" +
               $"\nContainers loaded: {containers.Count} of {max_containers}" +
               $"\nMax Weight: {max_weight} tons" +
               $"\nCurrent Weight: {GetTotalWeight()} kg" +
               $"\nLoaded Containers: {containers.Count}";
    }
}