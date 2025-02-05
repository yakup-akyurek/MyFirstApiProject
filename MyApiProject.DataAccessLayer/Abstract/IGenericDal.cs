﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T :class
    {
        void Insert (T entity);
        void Delete (int id);
        void Update (T entity);
        T GetById(int id);
        List<T> GetAll();
    }
}
