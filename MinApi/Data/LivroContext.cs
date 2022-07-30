using System.Data;

namespace MinApi.Data
{
    public class LivroContext
    {
        public delegate Task<IDbConnection> GetConnection();
    }
}






//delegate - parecido com ponteiro 
//vai definir por um delegate as conexoes , 
//servir de referencia para o metodo