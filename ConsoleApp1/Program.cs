using System;
using System.Collections.Generic;
using System.Linq;

namespace ProdutoApp
{
    // Enumeração para os tipos de produto
    public enum TipoProduto
    {
        Final,
        Intermediario,
        Consumo,
        MateriaPrima
    }

    // Classe Produto com suas propriedades
    public class Produto
    {
        public string Descricao { get; set; }
        public double ValorVenda { get; set; }
        public double ValorCompra { get; set; }
        public TipoProduto Tipo { get; set; }
        public DateTime DataCriacao { get; set; }

        // Método para calcular a margem de lucro
        public double MargemLucro => ValorVenda - ValorCompra;

        // Validações para inserção
        public bool IsValid()
        {
            if (ValorVenda <= ValorCompra) return false;
            if (DataCriacao < new DateTime(2024, 01, 01)) return false;
            if (Descricao.Length < 5) return false;
            if (ValorCompra <= 0 || ValorVenda <= 0) return false;
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Lista de produtos
            List<Produto> produtos = new List<Produto>
            {
                new Produto {Descricao = "Produto1", ValorCompra = 100, ValorVenda = 150, Tipo = TipoProduto.Final, DataCriacao = new DateTime(2024, 04, 01)},
                new Produto {Descricao = "Produto2", ValorCompra = 200, ValorVenda = 300, Tipo = TipoProduto.Intermediario, DataCriacao = new DateTime(2024, 05, 10)},
                new Produto {Descricao = "Prod", ValorCompra = 50, ValorVenda = 100, Tipo = TipoProduto.Consumo, DataCriacao = new DateTime(2024, 06, 15)},  // Descrição inválida
                new Produto {Descricao = "Produto4", ValorCompra = 500, ValorVenda = 700, Tipo = TipoProduto.MateriaPrima, DataCriacao = new DateTime(2024, 07, 01)},
                new Produto {Descricao = "Produto5", ValorCompra = 150, ValorVenda = 170, Tipo = TipoProduto.Final, DataCriacao = new DateTime(2024, 02, 10)},
                new Produto {Descricao = "Produto6", ValorCompra = 80, ValorVenda = 120, Tipo = TipoProduto.Intermediario, DataCriacao = new DateTime(2024, 03, 25)},
            };

            // Validação dos produtos
            var produtosValidos = produtos.Where(p => p.IsValid()).ToList();

            // Filtrar produtos criados no segundo trimestre de 2024
            var produtosSegundoTrimestre = produtosValidos.Where(p =>
                p.DataCriacao >= new DateTime(2024, 04, 01) && p.DataCriacao <= new DateTime(2024, 06, 30));

            Console.WriteLine("Produtos criados no segundo trimestre de 2024:");
            foreach (var produto in produtosSegundoTrimestre)
            {
                Console.WriteLine($"Descrição: {produto.Descricao}, Tipo: {produto.Tipo}, Data Criação: {produto.DataCriacao.ToShortDateString()}");
            }

            // Ordenar os produtos por Tipo
            var produtosOrdenadosPorTipo = produtosValidos.OrderBy(p => p.Tipo);

            Console.WriteLine("\nProdutos ordenados por Tipo:");
            foreach (var produto in produtosOrdenadosPorTipo)
            {
                Console.WriteLine($"Descrição: {produto.Descricao}, Tipo: {produto.Tipo}");
            }

            // Exibir os 3 produtos com maior margem de lucro
            var produtosMaiorLucro = produtosValidos.OrderByDescending(p => p.MargemLucro).Take(3);

            Console.WriteLine("\nTop 3 produtos com maior margem de lucro:");
            foreach (var produto in produtosMaiorLucro)
            {
                Console.WriteLine($"Descrição: {produto.Descricao}, Margem de Lucro: {produto.MargemLucro:C}");
            }
        }
    }
}
