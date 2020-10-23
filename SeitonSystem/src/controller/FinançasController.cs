﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeitonSystem.src.dao;
using SeitonSystem.src.dto;

namespace SeitonSystem.src.controller
{
    class FinançasController
    {
        FinançasDAO finançasDAO;

        public FinançasController()
        {
            try
            {
                finançasDAO = new FinançasDAO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InserirAtividade(Finanças finanças)
        {
            try
            {
                finançasDAO.InserirFluxo(finanças);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarFluxo(Finanças finanças)
        {
            try
            {
                finançasDAO.EditarFluxo(finanças);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ApagarFluxo(int id)
        {
            try
            {
                finançasDAO.ApagarFluxo(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Finanças> ListarFluxoTotal()
        {
            try
            {
                return finançasDAO.ListarFluxoTotal();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Finanças> ListarFluxoSaida()
        {
            try
            {
                return finançasDAO.ListarFluxoSaida();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Finanças> ListarFluxoEntrada()
        {
            try
            {
                return finançasDAO.ListarFluxoEntrada();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Finanças PesquisarPId(int id)
        {
            try
            {
                return finançasDAO.PesquisaFinançasId(id);
            }
            catch (Exception)
            {
                throw;
            }


        }

        public List<Finanças>BuscarPNome(string titulo)
        {
            try
            {
                return finançasDAO.BuscarPorNome(titulo);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
