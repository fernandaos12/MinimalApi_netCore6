
using System.ComponentModel.DataAnnotations.Schema;

namespace MinApi.Data;

[Table("Livro")]

public record Livro(int Id, string titulo, string autor, int anoPublicacao);


//Record - tipo de estrutura de dados, 
//para usar como model modelo de dominio, 
//tem propriedades somente leitura e nao 
//pode sofrer alteracoes depois de sua criacao,
//cria um construtor padrao para o tipo 