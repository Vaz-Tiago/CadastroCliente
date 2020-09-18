using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Incluir(cliente);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Alterar(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(CPF);
        }

        /// <summary>
        /// ValidaCPF
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarValidade(string CPF)
        {
            CPF = CPF.Trim().Replace(".", "").Replace("-", "");
            if (CPF.Length != 11)
                return false;

            List<string> sequenciasInvalidas = new List<string>()
            {
                "00000000000", "11111111111", "22222222222",
                "33333333333", "44444444444", "55555555555",
                "66666666666", "77777777777", "88888888888",
                "99999999999"
            };
            if (sequenciasInvalidas.Contains(CPF))
                return false;

            List<int> multiplicadorDigito1 = new List<int>() { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            List<int> multiplicadorDigito2 = new List<int>() { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string calculoCpf = CPF.Substring(0, 9);

            int soma = 0;
            for(int i = 0; i < 9; i++)
                soma += int.Parse(calculoCpf[i].ToString()) * multiplicadorDigito1[i];
            
            int resto = (soma * 10)  % 11;
            string digito = resto == 10 ? "0" : resto.ToString();

            calculoCpf = calculoCpf + digito;

            soma = 0;
            for(int i = 0; i < 10; i++)
                soma += int.Parse(calculoCpf[i].ToString()) * multiplicadorDigito2[i];

            resto = (soma * 10) % 11;
            digito = digito + (resto == 10 ? "0" : resto.ToString());

            return CPF.EndsWith(digito);
        }

    }
}
