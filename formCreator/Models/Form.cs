﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace formCreator.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
    }

    public class Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class FormDBContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
    }
}