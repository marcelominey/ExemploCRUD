using System;

namespace ExemploCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            BancoDados bd = new BancoDados();
            Categoria ca = new Categoria();

            Console.WriteLine("Insira o título: ");
            ca.Titulo = Console.ReadLine();

            Console.WriteLine("Insira o ID do título: ");
            ca.IdCategoria = int.Parse(Console.ReadLine());


            bool adicionar = bd.Adicionar(ca);

            

        }
    }
}
