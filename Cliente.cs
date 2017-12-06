using System;

namespace ExemploCRUD
{
    public class Cliente
    //Isso não é a tabela de clientes: ela está no Banco de Dados SQL.
    //Nós estamos usando essa classe para transitar os dados para tabela Cliente
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public string CpfCliente { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}