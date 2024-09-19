using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Tienda_Parker.Database
{

    public partial class Historial_ventas
    {
        public Historial_ventas(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
