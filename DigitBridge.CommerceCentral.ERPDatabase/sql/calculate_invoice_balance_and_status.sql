select 
ins.InvoiceStatus,
ins.PaidAmount+trs.TotalAmount, 
ins.Balance-trs.TotalAmount,
(CASE 
    WHEN (ins.Balance-trs.TotalAmount) = 0 AND (ins.InvoiceStatus!=2) THEN 2
    WHEN (ins.Balance-trs.TotalAmount) > 0 AND (ins.InvoiceStatus!=1) THEN 1
    WHEN (ins.Balance-trs.TotalAmount) < 0 AND (ins.InvoiceStatus!=3) THEN 3
END)
FROM InvoiceHeader ins
INNER JOIN InvoiceTransaction trs ON (trs.InvoiceUuid = ins.InvoiceUuid AND trs.TransUuid = '46530b79-2906-4fff-84b7-96a8527db8fb')
WHERE ins.InvoiceStatus != 255 AND ins.InvoiceStatus != 200
