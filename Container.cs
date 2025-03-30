namespace apbd2;

using System;

public interface IHazardNotifier { void SendNotification(string message); }

public enum ContainerType { L, G, C }

public abstract class Container {
        protected double cargo_weight { get; set; }
        private double height { get; set; }
        private double container_weight;
        private double depth { get; set; }
        public double total_weight { get; set; }

        private static int number = 1;
        private ContainerType type;

        public string serial_number { get; }
        protected double capacity { get; set; }


        protected Container(ContainerType type, double height, double depth, double capacity, double container_weight) {
                this.type = type;
                this.height = height;
                this.container_weight = container_weight;
                this.depth = depth;
                this.capacity = capacity;
                serial_number = $"KON-{this.type}-{number++}";
        }
        
        public virtual void UnloadCargo() {
                LoadCargo(0);
        }
        
        public virtual void LoadCargo(double cargo_weight) {
                if (cargo_weight > capacity) {
                        throw new OverflowException($"Exceed the cargo capacity! Cannot fit {cargo_weight - capacity}.");
                }
                total_weight = cargo_weight + container_weight;
        }
}

public class Container_G : Container, IHazardNotifier {
        private double pressure;
        private static double leftover = 0.05;
        
        public Container_G(double height, double depth, double capacity, double container_weight, double pressure)
                : base(ContainerType.G, height, depth, capacity, container_weight) 
        { 
                this.pressure = pressure;
        }

        public override void LoadCargo(double cargo_weight) {
                if (cargo_weight > capacity) {
                        throw new OverflowException($"Exceed the cargo capacity! Cannot fit {cargo_weight - capacity}.");
                }
                base.LoadCargo(cargo_weight);
        }
        
        public override void UnloadCargo() {
                LoadCargo(leftover * cargo_weight);
        }

        public void SendNotification(string message) {
                Console.WriteLine($"Warning! Fire hazard. Container: {serial_number}");
        }
}

public class Container_L : Container, IHazardNotifier {
        public bool is_hazardous { get; }
        private static double hazardous { get; set; } = 0.5;
        private static double not_hazardous { get; set; } = 0.9;

        public Container_L(double height, double depth, double capacity, double container_weight, bool is_hazardous)
                : base(ContainerType.L, height, depth, capacity, container_weight) 
        {
                this.is_hazardous = is_hazardous;
        }

        public override void LoadCargo(double cargo_weight) {
                double final_capacity = capacity * double.Round(is_hazardous ? hazardous : not_hazardous);
                if (cargo_weight > final_capacity) {
                        throw new OverflowException($"Exceed the cargo capacity! Cannot fit {cargo_weight - final_capacity}.");
                }
                base.LoadCargo(cargo_weight);
        }

        public void SendNotification(string message) {
                Console.WriteLine($"Warning! Liquid hazard. Container: {serial_number}");
        }
}

public class Container_C : Container {
        private string cargo;
        private double required_temperature;
        public Container_C(double height, double depth, double capacity, double container_weight, string cargo, double required_temperature)
                : base(ContainerType.C, height, depth, capacity, container_weight)
        {
                this.cargo = cargo;
                this.required_temperature = required_temperature;
        }
        public void LoadCargo(double cargo_weight, double cargo_temperature) {
                if (Math.Abs(cargo_temperature - required_temperature) > 0.1) {
                        throw new ArgumentException($"Cannot load cargo. Container temperature ({cargo_temperature}°C) is different than required ({required_temperature}°C) for {cargo}.");
                }
                base.LoadCargo(cargo_weight);
        }
}
