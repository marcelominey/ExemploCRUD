using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExemploCRUD
{
    public class BancoDados
    {
        SqlConnection cn;
        SqlCommand comandos;
        SqlDataReader rd;
        public bool Adicionar(Categoria cat){
            bool rs = false;
            try{
                cn = new SqlConnection(); //SqlConnection tem dois construtores. Vai fazer "por fora" msm
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa;password=senai@123"; 
                //o arroba é por causa da barra; se não quiser usar o arroba, coloca duas barras
                cn.Open();
                comandos = new SqlCommand();
                comandos.Connection = cn; //estou dizendo ONDE os comandos de SQL deverão ser executados. 
                //Estou estabelecendo uma relação entre os dois. Se der erro, ele vai dizer que 
                //"falta a referência de ligação".

                comandos.CommandType = CommandType.Text;
                //Esse CommandType não vem da classe SQL/SQL Client, pois esses 3 tipos de comandos 
                //(StoredProcedure, Text, TableDirect) podem ser de vários bancos de dados
                //Ele tá no using Data.SqlClient.
                comandos.CommandText = "INSERT INTO CATEGORIAS(titulo)values(@vt)";
                comandos.Parameters.AddWithValue("@vt", cat.Titulo);

                int r = comandos.ExecuteNonQuery();
                //vai retornar zero ou um - pois vou cadastrar um por vez.
                if (r > 1)
                    rs = true;

                comandos.Parameters.Clear();
            } catch (SqlException se) {
                throw new Exception ("Erro ao cadastrar. " + se.Message);
            } catch (Exception ex) {
                throw new Exception ("Erro inesperado. " + ex.Message);
            } finally {
                cn.Close ();
            }
            return rs;
        }
        public bool Atualizar (Categoria cat) {
            bool rs = false;
            try {
                cn = new SqlConnection ();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;User id=sa;password=senai@123"; //se n quiser usar arroba deve-se usar \\. //Caminho do banco. Devemos informar o servidor, nome e usuário
                cn.Open ();
                comandos = new SqlCommand ();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                comandos.CommandText = "update Categorias set titulo=@vt where idCategoria=@vi";
                comandos.Parameters.AddWithValue ("@vt", cat.Titulo);
                comandos.Parameters.AddWithValue ("@vi", cat.IdCategoria); // os nomes titulo e id categoria são os parametros incluidos na classe categoria.

                int r = comandos.ExecuteNonQuery ();

                if (r > 0)
                    rs = true;

                comandos.Parameters.Clear ();
            } catch (SqlException se) {
                throw new Exception ("Erro ao atualizar. " + se.Message);
            } catch (Exception ex) {
                throw new Exception ("Erro inesperado. " + ex.Message);
            } finally {
                cn.Close ();
            }
            return rs;
        }
        public bool Deletar (Categoria cat) {
            bool rs = false;
            try {
                cn = new SqlConnection ();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;User id=sa;password=senai@123"; //se n quiser usar arroba deve-se usar \\. //Caminho do banco. Devemos informar o servidor, nome e usuário
                cn.Open ();
                comandos = new SqlCommand ();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                comandos.CommandText = "delete from Categorias where idCategoria=@vi";
                comandos.Parameters.AddWithValue ("@vi", cat.IdCategoria);

                int r = comandos.ExecuteNonQuery ();

                if (r > 0)
                    rs = true;

                comandos.Parameters.Clear ();
            } catch (SqlException se) {
                throw new Exception ("Erro ao deletar. " + se.Message);
            } catch (Exception ex) {
                throw new Exception ("Erro inesperado. " + ex.Message);
            } finally {
                cn.Close ();
            }
            return rs;
        }
        public List<Categoria> ListarCategorias(int id){
            List<Categoria> lista = new List<Categoria>();
            try{
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;User id=sa;password=senai@123";
                cn.Open();
                comandos = new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                comandos.CommandText="Select * from Categorias where idCategoria=@vi";
                comandos.Parameters.AddWithValue("@vi",id);
                rd = comandos.ExecuteReader();

                while(rd.Read()){ //ENQUANTO TIVER LINHA/CONTEÚDO PARA LER EM RD
                    lista.Add(new Categoria{IdCategoria=rd.GetInt32(0), Titulo=rd.GetString(1)});
                    //CLASSE CATEGORIA: MEIO DE PASSAGEM DE DADOS
                    //ADICIONANDO NESSA LISTA UMA NOVA CATEGORIA, GERADA ANONIMAMENTE
                    //COLOCA O ID E O TITULO DENTRO DELA
                    //ADICIONA NA LISTA (LISTA QUE SÓ RECEBE CATEGORIAS).
                    
                }
                comandos.Parameters.Clear();
            }
            catch(SqlException se){
                throw new Exception("Erro ao tentar listar "+se.Message);
            }
            catch(Exception ex){
                throw new Exception("Erro inesperado "+ex.Message);
            }
            finally{
                cn.Close();
            }
            return lista;
        }
        public List<Categoria> ListarCategorias(string titulo){
            List<Categoria> lista = new List<Categoria>();
            try{
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;User id=sa;password=senai@123";
                cn.Open();
                comandos = new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                comandos.CommandText="Select * from Categorias where titulo like @vi";
                comandos.Parameters.AddWithValue("@vi",titulo);
                rd = comandos.ExecuteReader();

                while(rd.Read()){ //ENQUANTO TIVER LINHA/CONTEÚDO PARA LER EM RD
                    lista.Add(new Categoria{IdCategoria=rd.GetInt32(0), Titulo=rd.GetString(1)});
                    //CLASSE CATEGORIA: MEIO DE PASSAGEM DE DADOS
                    //ADICIONANDO NESSA LISTA UMA NOVA CATEGORIA, GERADA ANONIMAMENTE
                    //COLOCA O ID E O TITULO DENTRO DELA
                    //ADICIONA NA LISTA (LISTA QUE SÓ RECEBE CATEGORIAS).
                    
                }
                comandos.Parameters.Clear();
            }
            catch(SqlException se){
                throw new Exception("Erro ao tentar listar "+se.Message);
            }
            catch(Exception ex){
                throw new Exception("Erro inesperado "+ex.Message);
            }
            finally{
                cn.Close();
            }
            return lista;
        }

        public bool AdicionarCliente(Cliente cliente){
            bool rs = false;
            try{
                cn = new SqlConnection(); //apenas se comunicando com o servidor.
                //para se comunicar, precisa dizer o caminho, o BD que vou utilizar, o usuário e a senha:
                cn.ConnectionString = @"Data Source = .\sqlexpress; initial catalog = Papelaria; user id = sa; password=senai@123";
                cn.Open();
                comandos = new SqlCommand(); //vou dar os comandos de SQL dentro no banco referenciado cn.
                //em seguida, estabeleço uma relação entre o BD e :
                comandos.Connection = cn;
                
                comandos.CommandType = CommandType.StoredProcedure;
                comandos.CommandText = "sp_cadCliente"; //os parâmetros não estão aqui, estão lá dentro da procedure, no BD.
                //Veja que não tem @nome de nada, portanto, ele não é um parâmetro de C#, é um parâmetro de Banco!
                //Se quiser, compare com o método ListarCategorias, acima.

                SqlParameter pnome = new SqlParameter("@nome",SqlDbType.VarChar,50);//é um parâmetro de SQL mesmo.
                //Estou criando um parâmetro pome para que o C# tome conhecimento do parâmetro lá dentro da minha procedure!
                //AGORA, aquele parâmetro está sendo representado pelo pnome.
                pnome.Value = cliente.NomeCliente;
                comandos.Parameters.Add(pnome);
                //lá na procedure, tenho vários elementos aguardando para serem adicionados.
                //vou passar primeiro todos os elementos para o parâmetro; só depois, eu vou mandar inserir.
                
                SqlParameter pemail = new SqlParameter("@email",SqlDbType.VarChar,100);
                pemail.Value = cliente.EmailCliente;
                comandos.Parameters.Add(pemail);

                SqlParameter pcpf = new SqlParameter("@cpf",SqlDbType.VarChar,20);
                pcpf.Value = cliente.CpfCliente;
                comandos.Parameters.Add(pcpf);

                int r = comandos.ExecuteNonQuery();

                if(r > 1)
                    rs = true;
                
                comandos.Parameters.Clear();
                
            }
            catch(SqlException se){
                throw new Exception("Erro ao tentar inserir os dados "+se.Message);
            }
            catch(Exception ex){
                throw new Exception("erro inesperado "+ex.Message);
            }
            finally{
                cn.Close();
            }
            return rs;
        }
    }
}

