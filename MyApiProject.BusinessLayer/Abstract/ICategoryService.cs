﻿using MyApiProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.BusinessLayer.Abstract
{
    public interface ICategoryService:IGenericService<Category>
    {
        public int TCategoryCount();

    }
}
