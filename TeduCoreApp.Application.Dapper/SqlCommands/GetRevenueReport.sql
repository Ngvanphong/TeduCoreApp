alter PROC GetRevenueDaily
	@fromDate VARCHAR(10),
	@toDate VARCHAR(10)
AS
BEGIN
		  select
                CAST(b.DateCreated AS DATE) as Date,			
                sum(bd.Quantity * p.OriginalPrice) as MoneyIncome	
				INTO ImcomeTable
                from Bills b
                inner join dbo.BillDetails bd
                on b.Id = bd.BillId
                inner join Products p
                on bd.ProductId  = p.Id
                where b.DateCreated <= cast(@toDate as date) 
				AND b.DateCreated >= cast(@fromDate as date)
                group by CAST(b.DateCreated AS DATE);
		 select
                CAST(b2.DateCreated AS DATE) as Date,
				sum(b2.TotalMoneyPayment) as TotalPayMent
				INTO TotalPayMentTable
				from dbo.Bills b2
				where b2.DateCreated <= cast(@toDate as date) 
				AND b2.DateCreated >= cast(@fromDate as date)
                group by CAST(b2.DateCreated AS DATE);
		select 
			CAST(incom.Date AS DATE) as Date,
			 incom.TotalPayMent as Revenue,
             (incom.TotalPayMent-orgin.MoneyIncome) as Profit
              from TotalPayMentTable incom
			  inner join ImcomeTable orgin
			  on incom.Date=orgin.Date
			  
		DROP TABLE TotalPayMentTable;
		DROP TABLE ImcomeTable;
END

EXEC dbo.GetRevenueDaily @fromDate = '01/01/2017',
                         @toDate = '01/31/2019' 
