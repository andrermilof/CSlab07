using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab07
{
    public class Comment : Attribute
    {
        public string com { get; }

        public Comment(string com)
        {
            this.com = com;
        }
    }

    public enum eClassificationAnimal
    {
        Herbivores,
        Carnivores,
        Omnivores
    }

    public enum eFavouriteFood
    {
        Meat,
        Plants,
        Everything
    }

    [Comment("Animal")]
    public abstract class Animal
    {
        public string Name { get; set; }
        
        public string Country { get; set; }

        public string HideFromOtherAnimals { get; set; }

        public string WhatAnimal { get; set; }

        public eClassificationAnimal ClassificationAnimal;
        

        public Animal() { }

        public void Deconstruct() { }

        public eClassificationAnimal GetClassificationAnimal()
        {
            return ClassificationAnimal;
        }
        
        abstract public eFavouriteFood GetFavouriteFood();

        abstract public string SayHello();
    }

    [Comment("Cow")]
    public class Cow : Animal
    {
        public Cow() { }
        override public eFavouriteFood GetFavouriteFood()
        {
            return eFavouriteFood.Plants;
        }
        override public string SayHello()
        {
            return "mo";
        }
    }

    [Comment("Lion")]
    public class Lion : Animal
    {
        public Lion() { }
        override public eFavouriteFood GetFavouriteFood()
        {
            return eFavouriteFood.Meat;
        }
        override public string SayHello()
        {
            return "rr";
        }
    }

    [Comment("Pig")]
    public class Pig : Animal
    {
        public Pig() { }
        override public eFavouriteFood GetFavouriteFood()
        {
            return eFavouriteFood.Everything;
        }
        override public string SayHello()
        {
            return "hru";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
