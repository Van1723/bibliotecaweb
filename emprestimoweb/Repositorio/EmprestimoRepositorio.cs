using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emprestimoweb.Models;
using System.Data.Entity;

namespace emprestimoweb.Repositorio
{
    public class EmprestimoRepositorio
    {
        private bibliotecaEntities db = new bibliotecaEntities();
        public int GeraNovoEmprestimo()
        {
            Emprestimo objinserir = new Emprestimo();
            objinserir.Data_Emprestimo = DateTime.Now;
            db.Emprestimo.Add(objinserir);
            db.SaveChanges();
            return objinserir.Codigo;

        }

        public void FechaEmprestimo(Emprestimo objdados)
        {
            objdados.Data_Emprestimo = DateTime.Now;
            db.Entry(objdados).State = EntityState.Modified;
            db.SaveChanges();
            
            
        }
    }
}
    
