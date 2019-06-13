using FunBlackJack.BusinessObjects;
using System;

namespace FunBlackJack {
    class Program {
        static void Main(string[] args) {
            var table = new Table(5, 4);
            table.Play();

            Console.ReadKey();
        }
    }
}
