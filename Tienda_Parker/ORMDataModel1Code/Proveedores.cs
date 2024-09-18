using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Tienda_Parker.tienda
{

    public partial class Proveedores
    {
        public Proveedores(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
