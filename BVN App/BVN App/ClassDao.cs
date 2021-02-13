using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Bvn_App
{

    public class Campos
    {
        public int id;
        public string Nome_Cli;
        public string DDD;
        public string Telefone;
        public string Celular;
        public string Email;
        public string endereco;
        public string cep;
        public string obs;

        public string nome;
        public string Fabricante;
        public decimal Preco;
        public int Estoque;
        public int Min_estoque;
        public string nome2;
        public string id2;
    }
    public class ClassDao
    {
        public ClassDao()
        {

        }

        public Campos campos = new Campos();

        public MySqlConnection minhaConexao;
        public string usuarioBD = "freedbtech_TesteDb";
        public string senhaBD = "teste123";
        public string servidor = "freedb.tech";
        string bancoDados;
        string tabela;

        public void Conecte(string BancoDados, string Tabela)
        {
            bancoDados = BancoDados;
            tabela = Tabela;
            minhaConexao = new MySqlConnection("server=" + servidor + ";database=" + bancoDados +
                                               "; uid=" + usuarioBD + "; password=" + senhaBD);
        }

        public void Abrir()
        {
            minhaConexao.Open();
        }

        public void Fechar()
        {
            minhaConexao.Close();
        }

        public void PreencheTabela(System.Windows.Forms.DataGridView dataGridView)
        {
            Abrir();

            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select Nome_Cli, Cnpj, DDD, Estado from " + tabela, minhaConexao);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela;

            Fechar();
        }

        public void PreencheTabelaVenda(System.Windows.Forms.DataGridView dataGridView)
        {
            Abrir();

            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * from TabVenda", minhaConexao);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela;

            Fechar();
        }

        public void Insere(string Nome, string Email, string Cnpj, string Cep, string Telefone, string Abertura, string estado, string cidade, string endereco, string DDD)
        {
            Abrir();

            // Nome_Cli, DDD, Telefone, Email, Endereco, Cidade, Estado, CEP, Cnpj
            MySqlCommand comando = new MySqlCommand("insert into " + tabela +
                                                    "(Nome_Cli, DDD, Telefone, Email, Endereco, Cidade, Estado, CEP, Cnpj) values(@nome, @DDD, @telefone, @email, @endereco, @cidade, @estado, @cep, @cnpj)", minhaConexao);
            comando.Parameters.AddWithValue("@nome", Nome);
            comando.Parameters.AddWithValue("@email", Email);
            comando.Parameters.AddWithValue("@cnpj", Cnpj);
            comando.Parameters.AddWithValue("@cep", Cep);
            comando.Parameters.AddWithValue("@telefone", Telefone);
            comando.Parameters.AddWithValue("@abertura", Abertura);
            comando.Parameters.AddWithValue("@estado", estado);
            comando.Parameters.AddWithValue("@cidade", cidade);
            comando.Parameters.AddWithValue("@endereco", endereco);
            comando.Parameters.AddWithValue("@DDD", DDD);
            comando.ExecuteNonQuery();

            Fechar();
        }
        public void InsereVenda(int op4, int op5, int op6, string TipoDePagamento, string TipoDoEnvio, string StatusDoPedido, string Frete, int ValorNotaFiscal, string N_NotaFiscal, int Desconto)
        {
            Abrir();

            // Nome_Cli, DDD, Telefone, Email, Endereco, Cidade, Estado, CEP, Cnpj
            MySqlCommand comando = new MySqlCommand("insert into TabVenda (op4, op5, op6, TipoDePagamento, TipoDoEnvio, StatusDoPedido, Frete, ValorNotaFiscal, N_NotaFiscal, Desconto) values(@op4, @op5, @op6, @TipoDePagamento, @TipoDoEnvio, @StatusDoPedido, @Frete, @ValorNotaFiscal, @N_NotaFiscal, @Desconto)", minhaConexao);
            comando.Parameters.AddWithValue("@op4", op4);
            comando.Parameters.AddWithValue("@op5", op5);
            comando.Parameters.AddWithValue("@op6", op6);
            comando.Parameters.AddWithValue("@TipoDePagamento", TipoDePagamento);
            comando.Parameters.AddWithValue("@TipoDoEnvio", TipoDoEnvio);
            comando.Parameters.AddWithValue("@StatusDoPedido", StatusDoPedido);
            comando.Parameters.AddWithValue("@Frete", Frete);
            comando.Parameters.AddWithValue("@ValorNotaFiscal", ValorNotaFiscal);
            comando.Parameters.AddWithValue("@N_NotaFiscal", N_NotaFiscal);
            comando.Parameters.AddWithValue("@Desconto", Desconto);

            comando.ExecuteNonQuery();

            Fechar();
        }

        public void Atualiza(string campoNome_Cli, string campoDDD, string campoTelefone, int campoCelular, int ID, int Email)
        {
            Abrir();
            MySqlCommand comando = new MySqlCommand("update " + tabela + " set Nome_Cli=@Nome_Cli, DDD=@DDD , " + "Telefone=@Telefone, Celular=@Celular,  Email=@Email where ID=@id", minhaConexao);

            comando.Parameters.AddWithValue("@id", ID);
            comando.Parameters.AddWithValue("@Nome_Cli", campoNome_Cli);
            comando.Parameters.AddWithValue("@DDD", campoDDD);
            comando.Parameters.AddWithValue("@Telefone", campoTelefone);
            comando.Parameters.AddWithValue("@Celular", campoCelular);
            comando.Parameters.AddWithValue("@Email", Email);
            comando.ExecuteNonQuery();
            Fechar();
        }

        public void Consulta(string campoNome_Cli)
        {
            // consulta por Nome_Cli
            Abrir();

            MySqlCommand comando = new MySqlCommand("select * from " + tabela
                                                    + " where Nome_Cli = '" + campoNome_Cli + "'", minhaConexao);
            MySqlDataReader dtReader = comando.ExecuteReader();
            if (dtReader.Read())
            {
                campos.id = int.Parse(dtReader["ID"].ToString());
                campos.Nome_Cli = dtReader["Nome_Cli"].ToString();
                campos.DDD = dtReader["DDD"].ToString();
                campos.Telefone = dtReader["Telefone"].ToString();
                campos.Celular = dtReader["Celular"].ToString();
                campos.Email = dtReader["Email"].ToString();
            }

            Fechar();
        }

        public void Consulta(int id)
        {
            // sobrecarga do método de Consulta para permitir consulta por id também
            Abrir();
            MySqlCommand comando = new MySqlCommand("select * from " + tabela + " where id = '" + id + "'", minhaConexao);
            MySqlDataReader dtReader = comando.ExecuteReader();
            if (dtReader.Read())
            {
                campos.id = int.Parse(dtReader["ID"].ToString());
                campos.Nome_Cli = dtReader["Nome_Cli"].ToString();
                campos.DDD = dtReader["DDD"].ToString();
                campos.Telefone = dtReader["Telefone"].ToString();
                campos.Celular = dtReader["Celular"].ToString();
                campos.Email = dtReader["Email"] .ToString();
                campos.endereco = dtReader["Endereco"] + " - " + dtReader["Cidade"].ToString() + " - (" + dtReader["Estado"]+ ")" ;
                campos.cep = dtReader["Cep"].ToString();
                campos.obs = dtReader["obs"].ToString();
            }

            Fechar();
        }

        public void Deleta(int id)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("delete from "
                                                   + tabela + " where ID = @id", minhaConexao);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();

            Fechar();
        }

        public int NumRegistro()
        {
            Abrir();
            // MAX retorna o número do último valor de numCli
            MySqlCommand comando = new MySqlCommand("SELECT MAX(ID) FROM " + tabela, minhaConexao); // MAX retorna o número do último valor de numCli
            string n = comando.ExecuteScalar().ToString(); // ExecuteScalar retorna um dado do tipo object. É preciso converter para string.
            int num = int.Parse(n) + 1; // Agora convertemos o dado para int e somamos um para obter o número do próximo registro

            Fechar();

            return num; // retorna o número do próximo registro do autoincrement de numCli
        }


        public void ExibirDados()
        {
            string Nome2, DDD2, Telefone2, Celular2, Email2;
            Nome2 = campos.Nome_Cli;
            DDD2 = campos.DDD;
            Telefone2 = campos.Telefone.ToString();
            Celular2 = campos.Celular.ToString();
            Email2 = campos.Email.ToString();
        }

        public void linha(int id)
        {

        }
    }
}
