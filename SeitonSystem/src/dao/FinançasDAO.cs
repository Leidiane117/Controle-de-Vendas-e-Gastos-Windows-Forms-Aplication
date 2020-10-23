﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SeitonSystem.src.dto;
using SeitonSystem.src.dao;

namespace SeitonSystem.src.dao
{
    public class FinançasDAO
    {
        public const string inserirFluxo = "INSERT INTO fluxo_de_caixa(titulo, descricao, data_lancamento, tipo_fluxo)" +
                "VALUES(@titulo, @descricao, @data,@tipo)"; //0k

        public const string atualizarFluxo = "UPDATE fluxo_de_caixa SET titulo=@titulo, valor=@valor"+
            "descricao=@descricao, data_lancamento=@data," +
            " tipo_fluxo=@tipo  WHERE id=@id"; // ok

        public const string selectFluxoTotal = "SELECT id,titulo,valor,descricao,data_lancamento, tipo_fluxo" +
            " FROM fluxo_de_caixa"; // selecionar todas as colunas 0k

        public const string selectPorNome = "SELECT id,titulo, valor,descricao,data_lancamento,tipo_fluxo" +
            " FROM fluxo_de_caixa WHERE"; // selecionar os dados por nome/filtro 0k

        public const string selectFluxoId = "SELECT * FROM fluxo_de_caixa WHERE id=@id"; //ok

        public const string selectFluxoPorData = "SELECT* FROM fluxo_de_caixa WHERE";// falta esse

        public const string selectEntrada = "SELECT id,titulo,valor,descricao,data_lancamento, tipo_fluxo" +
           " FROM fluxo_de_caixa Where tipo_fluxo='Entrada'";// ok

        public const string selectSaida = "SELECT id,titulo, valor,descricao,data_lancamento, tipo_fluxo" +
           " FROM fluxo_de_caixa Where tipo_fluxo='Saida'"; // ok

        public const string deleteFluxo = "DELETE FROM fluxo_de_caixa WHERE id=@id";
       

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader MySqlDataReader;


        public FinançasDAO() // método para nova conexão
        {
            conn = ConnectDAO.GetConnection();
        }

        public FinançasDAO(MySqlConnection conexao)
        {
            conn = conexao;
        }

        public void InserirFluxo(Finanças finanças) // método Inserir OK
        {
            try
            {
                cmd = new MySqlCommand(inserirFluxo, conn);
                cmd.Parameters.Add(new MySqlParameter("@id", finanças.Id));
                cmd.Parameters.Add(new MySqlParameter("@titulo", finanças.Titulo));
                cmd.Parameters.Add(new MySqlParameter("@valor", finanças.Valor));
                cmd.Parameters.Add(new MySqlParameter("@descricao", finanças.Descricao));
                cmd.Parameters.Add(new MySqlParameter("@data", finanças.Data_lancamento));
                cmd.Parameters.Add(new MySqlParameter("@tipo", finanças.Tipo_fluxo));


                conn.Open();
                cmd.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Inserir Atividade" + e.Message);
            }
            finally
            {
                ConnectDAO.CloseConnection(conn);
            }
        }


        public void EditarFluxo(Finanças finanças)// método editar
        {
            try
            {
                cmd = new MySqlCommand(atualizarFluxo, conn);// abrindo a conexão e chamando a string de consulta
                cmd.Parameters.Add(new MySqlParameter("@id", finanças.Id));
                cmd.Parameters.Add(new MySqlParameter("@titulo", finanças.Titulo));
                cmd.Parameters.Add(new MySqlParameter("@valor", finanças.Valor));
                cmd.Parameters.Add(new MySqlParameter("@descricao", finanças.Descricao));
                cmd.Parameters.Add(new MySqlParameter("@data", finanças.Data_lancamento));
                cmd.Parameters.Add(new MySqlParameter("@tipo", finanças.Tipo_fluxo));

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                throw new Exception("Erro ao Editar Atividade");
            }
            finally
            {
                ConnectDAO.CloseConnection(conn);
            }

        }

        public void ApagarFluxo(int id) // método que apaga 
        {
            try
            {

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand(deleteFluxo, conn); // apaga registro da tabela finanças
                cmd.Parameters.Add(new MySqlParameter("@id", id));

                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ConnectDAO.CloseConnection(conn);
            }
        }


        public List<Finanças> ListarFluxoTotal() // método select listar todos os produtos
        {
            List<Finanças> lista = new List<Finanças>();// crio um objeto do tipo lista da minha classe
                                                        //produtopincipal,instancio.

            try
            {
                cmd = new MySqlCommand(selectFluxoTotal, conn); //string query para consultar produtos

                conn.Open();
                MySqlDataReader = cmd.ExecuteReader(); // adapatar o mysql para executar a leitura dos dados da minha tabela produtos
                if (MySqlDataReader.HasRows)
                {
                    while (MySqlDataReader.Read())
                    { //enquanto houver leitura
                        Finanças dado = new Finanças
                        {
                            Id = int.Parse(MySqlDataReader["id"].ToString()),
                            Titulo = MySqlDataReader["titulo"].ToString(),
                            Valor = double.Parse(MySqlDataReader["valor"].ToString()),
                            Descricao = MySqlDataReader["descricao"].ToString(),
                            Data_lancamento = DateTime.Parse(MySqlDataReader["data_lancamento"].ToString()),
                            Tipo_fluxo = MySqlDataReader["tipo_fluxo"].ToString(),
                        };
                        lista.Add(dado);
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao exibir Atividades" + e.Message);
            }
            finally
            {
                MySqlDataReader.Close();
                ConnectDAO.CloseConnection(conn);
            }

            return lista;
        }

        public List<Finanças> ListarFluxoEntrada() // método select listar todos os produtos
        {
            List<Finanças> lista = new List<Finanças>();// crio um objeto do tipo lista da minha classe
                                                        //produtopincipal,instancio.

            try
            {
                cmd = new MySqlCommand(selectEntrada, conn); //string query para consultar produtos

                conn.Open();
                MySqlDataReader = cmd.ExecuteReader(); // adapatar o mysql para executar a leitura dos dados da minha tabela produtos
                if (MySqlDataReader.HasRows)
                {
                    while (MySqlDataReader.Read())
                    { //enquanto houver leitura
                        Finanças dado = new Finanças
                        {
                            Id = int.Parse(MySqlDataReader["id"].ToString()),
                            Titulo = MySqlDataReader["titulo"].ToString(),
                            Valor = double.Parse(MySqlDataReader["valor"].ToString()),
                            Descricao = MySqlDataReader["descricao"].ToString(),
                            Data_lancamento = DateTime.Parse(MySqlDataReader["data_lancamento"].ToString()),
                            Tipo_fluxo = MySqlDataReader["tipo_fluxo"].ToString(),
                        };
                        lista.Add(dado);
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao exibir Entradas" + e.Message);
            }
            finally
            {
                MySqlDataReader.Close();
                ConnectDAO.CloseConnection(conn);
            }

            return lista;
        }

        public List<Finanças> ListarFluxoSaida() // método select listar todos os produtos
        {
            List<Finanças> lista = new List<Finanças>();// crio um objeto do tipo lista da minha classe
                                                        //produtopincipal,instancio.

            try
            {
                cmd = new MySqlCommand(selectSaida, conn); //string query para consultar produtos

                conn.Open();
                MySqlDataReader = cmd.ExecuteReader(); // adapatar o mysql para executar a leitura dos dados da minha tabela produtos
                if (MySqlDataReader.HasRows)
                {
                    while (MySqlDataReader.Read())
                    { //enquanto houver leitura
                        Finanças dado = new Finanças
                        {
                            Id = int.Parse(MySqlDataReader["id"].ToString()),
                            Titulo = MySqlDataReader["titulo"].ToString(),
                            Valor = double.Parse(MySqlDataReader["valor"].ToString()),
                            Descricao = MySqlDataReader["descricao"].ToString(),
                            Data_lancamento = DateTime.Parse(MySqlDataReader["data_lancamento"].ToString()),
                            Tipo_fluxo = MySqlDataReader["tipo_fluxo"].ToString(),
                        };
                        lista.Add(dado);
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao exibir Saídas" + e.Message);
            }
            finally
            {
                MySqlDataReader.Close();
                ConnectDAO.CloseConnection(conn);
            }

            return lista;
        }


        public List<Finanças> BuscarPorNome(string titulo) // select por nome do produto
        {
            List<Finanças> lista2 = new List<Finanças>();// crio um objeto do tipo lista da minha classe
            try
            {
                int num;
                string select = selectPorNome;

                if (!int.TryParse(titulo, out num))
                {
                    select += " titulo LIKE @titulo";
                    titulo += "%";
                }
                else
                {
                    select += " id = @id";
                }                                                                  //produtopincipal,instancio.

                cmd = new MySqlCommand(select, conn); //string query para consultar produtos
                cmd.Parameters.Add(new MySqlParameter("@titulo", titulo));

                conn.Open();
                cmd.ExecuteNonQuery();
                MySqlDataReader = cmd.ExecuteReader(); // adapatar o mysql para executar a leitura dos dados da minha tabela produtos
                if (MySqlDataReader.HasRows)
                {
                    while (MySqlDataReader.Read())
                    { //enquanto houver leitura
                        Finanças dado = new Finanças
                        {
                            Id = int.Parse(MySqlDataReader["id"].ToString()),
                            Titulo = MySqlDataReader["titulo"].ToString(),
                            Valor = double.Parse(MySqlDataReader["valor"].ToString()),
                            Descricao = MySqlDataReader["descricao"].ToString(),
                            Data_lancamento = DateTime.Parse(MySqlDataReader["data_lancamento"].ToString()),
                            Tipo_fluxo = MySqlDataReader["tipo_fluxo"].ToString(),
                        };
                        lista2.Add(dado);
                    }

                }
            }
            catch (Exception)
            {
                throw new Exception(" Não encontrado!");
            }
            finally
            {
                MySqlDataReader.Close();
                ConnectDAO.CloseConnection(conn);
            }

            return lista2;
        }



        public Finanças PesquisaFinançasId(int id)
        {
            Finanças finanças = new Finanças();

            try
            {
                cmd = new MySqlCommand(selectFluxoId, conn);

                cmd.Parameters.Add(new MySqlParameter("@id", id));

                conn.Open();
                MySqlDataReader = cmd.ExecuteReader();

                if (MySqlDataReader.HasRows)
                {
                    while (MySqlDataReader.Read())
                    { //enquanto houver leitura

                        Finanças finanças1 = new Finanças
                        {
                            Id = int.Parse(MySqlDataReader["id"].ToString()),
                            Titulo = MySqlDataReader["titulo"].ToString(),
                            Valor = double.Parse(MySqlDataReader["valor"].ToString()),
                            Descricao = MySqlDataReader["descricao"].ToString(),
                            Data_lancamento = DateTime.Parse(MySqlDataReader["data_lancamento"].ToString()),
                            Tipo_fluxo = MySqlDataReader["tipo_fluxo"].ToString(),
                        };
                        return finanças1;
                    }
                }
                else
                {
                    throw new Exception("Id não Cadastrado");
                }

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Carregar Dados" + e.Message);
            }
            finally
            {
                ConnectDAO.CloseConnection(conn);
            }
            return finanças;
        }






    }


}




         















   












































