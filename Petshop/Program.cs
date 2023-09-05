using System;
using System.Collections.Generic;
using System.Linq;

// Heyvan classi
class Animal
{
    public string Nickname { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public int Energy { get; set; }
    public double Price { get; set; }
    public int MealQuantity { get; set; }

    public Animal(string nickname, int age, string gender, double price)
    {
        Nickname = nickname;
        Age = age;
        Gender = gender;
        Energy = 100; // yeni heyvanlar default olaraq 100 enerjiye sahib olur
        Price = price;
        MealQuantity = 0;
    }

    public void Eat()
    {
        Energy += 20;
        MealQuantity++;
    }

    public void Sleep()
    {
        Energy += 30;
    }

    public void Play()
    {
        Energy -= 10;
    }
}

// PetShop 
class PetShop
{
    public List<Animal> Cats { get; set; }
    public List<Animal> Dogs { get; set; }
    public List<Animal> Birds { get; set; }
    public List<Animal> Fishes { get; set; }

    public PetShop()
    {
        Cats = new List<Animal>();
        Dogs = new List<Animal>();
        Birds = new List<Animal>();
        Fishes = new List<Animal>();
    }

    // istenilen heyvan tipine yeni bir heyvan elave etmek
    public void AddAnimal(string type, string nickname, int age, string gender, double price)
    {
        var animal = new Animal(nickname, age, gender, price);
        switch (type.ToLower())
        {
            case "cat":
                Cats.Add(animal);
                break;
            case "dog":
                Dogs.Add(animal);
                break;
            case "bird":
                Birds.Add(animal);
                break;
            case "fish":
                Fishes.Add(animal);
                break;
            default:
                Console.WriteLine("Bele bir heyvan novu yoxdur!");
                break;
        }
    }

    // verilen ada uyğun heyvani cixarmaq
    public void RemoveByNickname(string nickname)
    {
        var allAnimals = Cats.Concat(Dogs).Concat(Birds).Concat(Fishes).ToList();
        var animalToRemove = allAnimals.FirstOrDefault(a => a.Nickname.Equals(nickname, StringComparison.OrdinalIgnoreCase));
        if (animalToRemove != null)
        {
            if (animalToRemove.MealQuantity > 0)
            {
                Console.WriteLine($"{nickname} adlı heyvan ümumi {animalToRemove.MealQuantity} defe yemek yedi.");
            }
            Console.WriteLine($"{nickname} adlı heyvanın ümumi deyeri: {animalToRemove.Price}");
            allAnimals.Remove(animalToRemove);
            Console.WriteLine($"{nickname} adlı heyvan cixarildi.");
        }
        else
        {
            Console.WriteLine($"{nickname} adlı heyvan tapilmadi.");
        }
    }

    // butun heyvanları siralamaq
    public void ListAllAnimals()
    {
        Console.WriteLine("Pisikler:");
        ListAnimals(Cats);
        Console.WriteLine("\nİtler:");
        ListAnimals(Dogs);
        Console.WriteLine("\nQuşlar:");
        ListAnimals(Birds);
        Console.WriteLine("\nBalıqlar:");
        ListAnimals(Fishes);
    }

    // her hansi bir heyvan tipindeki heyvanları siralamaq
    private void ListAnimals(List<Animal> animals)
    {
        foreach (var animal in animals)
        {
            Console.WriteLine($"Adı: {animal.Nickname}, Yaşı: {animal.Age}, Cinsi: {animal.Gender}, Enerjisi: {animal.Energy}, Deyeri: {animal.Price}");
        }
    }
}

//Main
class Program
{
    static void Main(string[] args)
    {
        PetShop petShop = new PetShop();
        while (true)
        {
            Console.WriteLine("\nEsas Menyu:");
            Console.WriteLine("1. Yeni Heyvan Elave et");
            Console.WriteLine("2. Heyvanlara Bax");
            Console.WriteLine("3. Heyvanı Çıxar");
            Console.WriteLine("4. Heyvanin heyat balansini artir");
            Console.WriteLine("5. Exit");

            Console.Write("Seçim edin: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Heyvanin Tipini yazin (Cat/Dog/Bird/Fish): ");
                    string type = Console.ReadLine();
                    Console.Write("Heyvanın Adı: ");
                    string nickname = Console.ReadLine();
                    Console.Write("Heyvanın Yaşı: ");
                    int age = int.Parse(Console.ReadLine());
                    Console.Write("Heyvanın Cinsi: ");
                    string gender = Console.ReadLine();
                    Console.Write("Heyvanın Deyeri: ");
                    double price = double.Parse(Console.ReadLine());

                    petShop.AddAnimal(type, nickname, age, gender, price);
                    break;
                case "2":
                    petShop.ListAllAnimals();
                    break;
                case "3":
                    Console.Write("Çıxarılacaq Heyvanın Adı: ");
                    string removeName = Console.ReadLine();
                    petShop.RemoveByNickname(removeName);
                    break;
                case "4":
                    AnimalActions(petShop);
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Programdan çıxılır...");
                    return;
                default:
                    Console.WriteLine("Bele bir secim yoxdur.Yeniden cehd edin :/");
                    break;
            }
        }
    }

    // Heyvan uzerinde isler gorulmesi
    static void AnimalActions(PetShop petShop)
    {
        Console.Clear();
        Console.WriteLine("\nHeyvanin heyat balansini artirmaq ucun:");
        Console.WriteLine("1. Heyvana Yemek ver");
        Console.WriteLine("2. Heyvan yatsin");
        Console.WriteLine("3. Geri qayit");

        Console.Write("Seçim edin (1-3): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Yemek vermek istediyin heyvanın adını daxil et: ");
                string feedName = Console.ReadLine();
                var feedAnimal = FindAnimal(petShop, feedName);
                if (feedAnimal != null)
                {
                    feedAnimal.Eat();
                    Console.WriteLine($"{feedAnimal.Nickname} adlı heyvana yemek verildi. Enerjisi artıq {feedAnimal.Energy}.");
                }
                else
                {
                    Console.WriteLine($"{feedName} adlı heyvan tapilmadi.");
                }
                break;
            case "2":
                Console.Write("Yatmasini istediyin heyvanın adını daxil et: ");
                string sleepName = Console.ReadLine();
                var sleepAnimal = FindAnimal(petShop, sleepName);
                if (sleepAnimal != null)
                {
                    sleepAnimal.Sleep();
                    Console.WriteLine($"{sleepAnimal.Nickname} adlı heyvan yatdi. Enerjisi artıq {sleepAnimal.Energy}.");
                }
                else
                {
                    Console.WriteLine($"{sleepName} adlı heyvan tapilmadi.");
                }
                break;
            case "3":
                break;
            default:
                Console.WriteLine("Bele bir secim yoxdur. Yeniden cehd edin :/");
                break;
        }
    }

    // verilen ada uyğun heyvani tapmaq
    static Animal FindAnimal(PetShop petShop, string nickname)
    {
        var allAnimals = petShop.Cats.Concat(petShop.Dogs).Concat(petShop.Birds).Concat(petShop.Fishes).ToList();
        return allAnimals.FirstOrDefault(a => a.Nickname.Equals(nickname, StringComparison.OrdinalIgnoreCase));
    }
}

