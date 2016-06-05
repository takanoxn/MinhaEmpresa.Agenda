﻿using MinhaEmpresa.SharedKernel.Dominio.Agregacoes;
using MinhaEmpresa.SharedKernel.Dominio.Entidades;
using MinhaEmpresa.SharedKernel.Dominio.Repositorio;
using NHibernate;

namespace MinhaEmpresa.SharedKernel.Repositorio
{
    public class RepositorioSessao : IRepositorioSessao
    {
        private readonly ISession _sessao;
        private ITransaction _transacao;

        public RepositorioSessao(ISession sessao)
        {
            _sessao = sessao;
        }

        public IRepositorioConsulta<T> GetRepositorioConsulta<T>() where T : IEntidade
        {
            return new RepositorioConsulta<T>(_sessao);
        }

        public IRepositorio<T> GetRepositorio<T>() where T : IAggregateRoot
        {
            return new Repositorio<T>(_sessao);
        }

        public void IniciaTransacao()
        {
            _transacao = _sessao.BeginTransaction();
        }

        public void ComitaTransacao()
        {
            _transacao.Commit();
            _transacao.Dispose();
        }

        public void RollBackTransacao()
        {
            _transacao.Rollback();
            _transacao.Dispose();
        }

        public void Dispose()
        {
            _sessao.Close();
        }
    }
}
