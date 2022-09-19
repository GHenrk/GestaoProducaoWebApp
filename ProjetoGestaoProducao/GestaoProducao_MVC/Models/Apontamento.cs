﻿using GestaoProducao_MVC.Models.Enums;
using System.Numerics;

namespace GestaoProducao_MVC.Models
{
    public class Apontamento
    {
        public int Id { get; set; }

        public DateTime DataInicial { get; set; } = DateTime.Now;

        public DateTime? DataFinal { get; set; }

        public TimeSpan? TempoTotal { get; set; }

        public string?  Descricao { get; set; }
    
        public AptStatus Status { get; set; }

        public Processo Processo { get; set; }

        public Maquina Maquina { get; set; }

        public Funcionario Funcionario { get; set; }

        public ICollection<RegistroParada> RegistroParadas { get; set; }

        public Apontamento()
        {

        }

        public Apontamento(DateTime dataInicial, DateTime? dataFinal, TimeSpan? tempoTotal, string descricao, AptStatus status, Processo processo, Maquina maquina, Funcionario funcionario)
        {
       
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            TempoTotal = tempoTotal;
            Descricao = descricao;
            Status = status;
            Processo = processo;
            Maquina = maquina;
            Funcionario = funcionario;
        }

        
        public void AddParada(RegistroParada parada)
        {
            RegistroParadas.Add(parada);

        }

        public void RemoveParada(RegistroParada parada)
        {
            RegistroParadas.Remove(parada);
        }
    }

}