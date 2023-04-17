using System;
using System.Linq;

namespace AbstractFactoryPattern
{
    internal class Program 
    {
        // Define the abstract pizza factory interface
        public interface PizzaFactory
        {
            Pizza CreatePizza(string type);
        }

        // Define the abstract pizza interface
        public abstract class Pizza
        {
            public string name;

            public Dough dough;
            public Sauce sauce;
            public Cheese cheese;
            public Pepperoni pepperoni;

            public abstract void Prepare();
            public void Bake()
            {
                Console.WriteLine("-Bake for 25 minutes at 350");
            }
            public void Cut()
            {
                Console.WriteLine("-Cutting the pizza into diagonal slices");
            }
            public void Box()
            {
                Console.WriteLine("-Place pizza in official PizzaStore box");
            }
            public void Message()
            { 
                Console.WriteLine("\t\n-- Customer ordered a " + name + "-- \n");
            }

            public void setName(string name)
            {
                this.name = name;
            }

            public string GetName()
            {
                return name;
            }
        }

        //Define ingredient used for the pizzas
        public interface Dough
        {
            void prepareDough();
        }

        public class ThinCrustDough : Dough
        {
            public ThinCrustDough()
            {
                prepareDough();
            }

            public void prepareDough()
            {
                Console.WriteLine("preparing dough...");
            }

        }

        public interface Sauce
        {
            void prepareSauce();

        }

        public class MarinaraSauce : Sauce
        {
            public MarinaraSauce()
            {
                prepareSauce();
            }
            public void prepareSauce()
            {
                Console.WriteLine("preparing sauce...");
            }
        }

        public interface Cheese
        {
            void prepareCheese();
        }

        public class ReggianoCheese : Cheese
        {
            public ReggianoCheese()
            {
                prepareCheese();
            }
            public void prepareCheese()
            {
                Console.WriteLine("preparing cheese...");
            }
        }

        public interface Pepperoni
        {
            void preparePepperoni();
        }

        public class SlicedPepperoni : Pepperoni
        {
            public SlicedPepperoni()
            {
                preparePepperoni();
            }
            public void preparePepperoni()
            {
                Console.WriteLine("preparing pepperoni...");
            }
        }

        // Define the abstract pizza factory interface
        public interface PizzaIngredientFactory
        {
            public Dough CreateDough();
            public Sauce CreateSauce();
            public Cheese CreateCheese();
            public Pepperoni CreatePepperoni();


        }

        public class NYPizzaIngredientFactory : PizzaIngredientFactory
        {
            public Dough CreateDough()
            {
                return new ThinCrustDough();
            }

            public Sauce CreateSauce()
            {
                return new MarinaraSauce();
            }

            public Cheese CreateCheese()
            {
                return new ReggianoCheese();
            }

            public Pepperoni CreatePepperoni()
            {
                return new SlicedPepperoni();
            }
        }

        public class PepperoniPizza : Pizza
        {
            readonly PizzaIngredientFactory ingredientFactory;

            public PepperoniPizza(PizzaIngredientFactory ingredientFactory)
            {
                this.ingredientFactory = ingredientFactory;
            }

            public override void Prepare()
            {
                Console.WriteLine("-- Preparing " + name + " --\n");
                dough = ingredientFactory.CreateDough();
                sauce = ingredientFactory.CreateSauce();
                cheese = ingredientFactory.CreateCheese();
                pepperoni = ingredientFactory.CreatePepperoni();
            }
        }
        public class CheesePizza : Pizza
        {
            readonly PizzaIngredientFactory ingredientFactory;

            public CheesePizza(PizzaIngredientFactory ingredientFactory)
            {
                this.ingredientFactory = ingredientFactory;
            }

            public override void Prepare()
            {
                Console.WriteLine("\n -- Preparing " + name + " --\n");
                dough = ingredientFactory.CreateDough();
                sauce = ingredientFactory.CreateSauce();
                cheese = ingredientFactory.CreateCheese();
            }
        }

        // Define the pizza store class that uses the abstract factory pattern
        public abstract class PizzaStore
        {
            public Pizza OrderPizza(string type)
            {
                Pizza pizza = CreatePizza(type);

                pizza.Prepare();
                pizza.Bake();
                pizza.Cut();
                pizza.Box();
                pizza.Message();

                return pizza;
            }

            public abstract Pizza CreatePizza(string type);


        }

        public class NYPizzaStore : PizzaStore
        {
            public override Pizza CreatePizza(string item)
            {
                Pizza pizza = null;
                PizzaIngredientFactory ingredientFactory = new NYPizzaIngredientFactory();

                if (item.Equals("cheese"))
                {
                    pizza = new CheesePizza(ingredientFactory);
                    pizza.setName("New York Style Cheese Pizza");
                }
                else if (item.Equals("pepperoni"))
                {
                    pizza = new PepperoniPizza(ingredientFactory);
                    pizza.setName("New York Style Pepperoni Pizza");
                }
                return pizza;
            }


        }

        
        static void Main(string[] args)
        {
            // Create a new pizza store that uses the NY style pizza factory
            PizzaStore nyPizzaStore = new NYPizzaStore();
            nyPizzaStore.OrderPizza("cheese");
            nyPizzaStore.OrderPizza("pepperoni");

            

            Console.ReadKey();
        }
    }
}