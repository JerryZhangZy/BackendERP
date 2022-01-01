using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.Db.Model
{
    public class QboSalesOrderWrapper
    {
        public QboSalesOrder qboSalesOrder { get; set; }
        public List<QboOrderItemLine> qboItemLines { get; set; }
        public QboSalesOrderWrapper()
        {
            this.qboSalesOrder = new QboSalesOrder();
            this.qboItemLines = new List<QboOrderItemLine>();
        }
    }
}
