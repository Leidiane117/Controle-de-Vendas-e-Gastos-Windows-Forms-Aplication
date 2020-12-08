﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SeitonSystem.src.dto;
using SeitonSystem.src.controller;
using SeitonSystem.src.view;

namespace SeitonSystem.src.view
{
    public partial class FinancasAtualizarView : Form
    {
        Finanças finanças;
        FinançasController finançasController;

        public FinancasAtualizarView(int idFluxo)
        {

            InitializeComponent();
         

            try
            {

                finançasController = new FinançasController();
                finanças = new Finanças();

                finanças = finançasController.PesquisarPId(idFluxo);
                PreencheTextBox();

            }
            catch (Exception e)
            {
                enviaMsg(e.Message, "erro");

            }
        }



        

        private void enviaMsg(String msg, String tipo)
        {
            MensagensView message = new MensagensView(msg, tipo);
            message.ShowDialog();
        }



        private void PreencheTextBox()
        {

            txt_id.Text = finanças.Id.ToString();
            txt_atualizarTitulo.Text = finanças.Titulo;
            dt_atualizar.Text = finanças.Data_lancamento.ToString();
            txt_atualizarValor.Text = finanças.Valor.ToString();
            txt_atualizarDescricao.Text = finanças.Descricao;
            cb_atualizar.Items.Add("Entrada");
            cb_atualizar.Items.Add("Saida");

        }




        private void LimparForm()
        {
            txt_id.Text = "";
            txt_atualizarTitulo.Clear();
            txt_atualizarValor.Clear();
            txt_atualizarDescricao.Clear();
            
            
        }


                    

        private void btn_limpar_Click(object sender, EventArgs e)
        {
            txt_atualizarTitulo.Clear();
            txt_atualizarValor.Clear();
            txt_atualizarDescricao.Clear();
        }

        private void btn_salvar_Click_1(object sender, EventArgs e)
        {

            try
            {
                Finanças finanças = new Finanças
                {
                    Id = int.Parse(txt_id.Text),
                    Titulo = txt_atualizarTitulo.Text,
                    Valor = double.Parse(txt_atualizarValor.Text),
                    Descricao = txt_atualizarDescricao.Text,
                    Data_lancamento = DateTime.Parse(dt_atualizar.Text),
                    Tipo_fluxo = cb_atualizar.Text
                };

                if (cb_atualizar.Text == "" || cb_atualizar.Text == null)
                {
                    enviaMsg("Informe o Tipo de Fluxo!", "aviso");
                }
               else if (!Regex.Match(txt_atualizarValor.Text, "^[0-9]{1,4}[,]{0,1}[0-9]{1,2}$").Success)
                {
                    enviaMsg(" Informe o valor corretamente!", "aviso");
                }
                else if (finanças.Valor <= 0.00)
                {
                    enviaMsg("Informe o valor!", "aviso");
                }


                else if (!Regex.Match(txt_atualizarTitulo.Text, "^[A-Za-zàáâãéèíóôúçÁÀÉÈÍÔÓÕÚÇ ]{1,80}$").Success)
                {
                    enviaMsg("Informe o Título corretamente!", "aviso");
                }


                else
                {

                    finançasController.AtualizarFluxo(finanças);
                    enviaMsg("Atividade Editada com Sucesso", "check");
                    LimparForm();
                    Close();
                }
            }
            catch (Exception)
            {
                throw;


            }



            }
      
        
        

        private void btn_voltar_Click(object sender, EventArgs e)
        {
           
                FinançasView finançasView = new FinançasView();
                finançasView.ShowDialog();
            }
        }
    }
    
















